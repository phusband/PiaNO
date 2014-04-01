using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PiaNO.Zip.Checksums;
using PiaNO.Zip.Compression;
using PiaNO.Zip.Streams;

namespace PiaNO.Serialization
{
    public static class PiaSerializer
    {
        public static void Deserialize(Stream stream, PiaFile piaFile)
        {
            if (stream == null)
                throw new ArgumentNullException("Stream");

            if (piaFile == null)
                throw new ArgumentNullException("PiaFile");

            try
            {
                // Header
                var headerBytes = new Byte[48]; // Ignore 12 byte checksum
                stream.Read(headerBytes, 0, headerBytes.Length);
                var headerString = Encoding.Default.GetString(headerBytes);
                piaFile.Header = new PiaHeader(headerString);

                // Inflation
                string inflatedString;
                stream.Seek(60, SeekOrigin.Begin);
                using (var zStream = new InflaterInputStream(stream))
                {
                    var sr = new StreamReader(zStream, Encoding.Default);
                    inflatedString = sr.ReadToEnd();
                }

                // Nodes
                piaFile.Owner = piaFile;
                _deserializeNode(piaFile, inflatedString);
            }
            catch (Exception)
            {
                throw;
            }

        }
        internal static void _deserializeNode(PiaNode parent, string nodeString)
        {
            if (parent == null && !(parent is PiaFile))
                throw new ArgumentNullException("parent");

            if (nodeString == null)
                throw new ArgumentNullException("nodeString");

            var dataLines = nodeString.Split(new char[] { '\r', '\n' },
                                            StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataLines.Length; i++)
            {
                var curLine = dataLines[i];
                if (curLine.Contains('='))
                {
                    var value = _deserializeValue(curLine);

                    if (!parent.Values.ContainsKey(value.Key))
                        parent.Values.Add(value.Key, value.Value);
                    else
                        parent.Values[value.Key] = value.Value;
                }
                else if (curLine.Contains('{'))
                {
                    var bracketCount = 1;
                    var nodeBuilder = new StringBuilder();
                    var n = i + 1;
                    var subLine = string.Empty;

                    while (bracketCount != 0)
                    {
                        subLine = dataLines[n++];
                        bracketCount += subLine.Contains('{')
                            ? 1 : subLine.Contains('}')
                            ? -1 : 0;

                        if (bracketCount != 0)
                            nodeBuilder.AppendLine(subLine);
                    }

                    var childNode = new PiaNode(nodeBuilder.ToString())
                    {
                        NodeName = curLine.Trim().TrimEnd('{'),
                        Parent = parent,
                        Owner = parent.Owner
                    };

                    parent.ChildNodes.Add(childNode);

                    i = n - 1;
                }
            }
        }
        private static KeyValuePair<string, string> _deserializeValue(string valueString)
        {
            var prop = valueString.TrimEnd(new char[] { '\r', '\n' }).Split('=');
            if (prop[1].StartsWith("\""))
            {
                prop[0] += "_str";
                prop[1] = prop[1].TrimStart('\"');
            }

            return new KeyValuePair<string, string>(prop[0].Trim(), prop[1].Trim());
        }

        public static void Serialize(Stream stream, PiaFile piaFile)
        {
            if (stream == null)
                throw new ArgumentNullException("Stream");

            if (piaFile == null)
                throw new ArgumentNullException("PiaFile");

            try
            {
                // Header
                var headerString = piaFile.Header.ToString();
                var headerBytes = Encoding.Default.GetBytes(headerString);
                stream.Write(headerBytes, 0, headerBytes.Length);

                // Nodes
                var nodeString = _serializeNode(piaFile);
                var nodeBytes = Encoding.Default.GetBytes(nodeString);

                // Deflation
                byte[] deflatedBytes;
                var deflater = new Deflater(Deflater.DEFAULT_COMPRESSION);
                using (var ms = new MemoryStream())
                {
                    var deflateStream = new DeflaterOutputStream(ms, deflater);
                    deflateStream.Write(nodeBytes, 0, nodeBytes.Length);
                    deflateStream.Finish();

                    deflatedBytes = ms.ToArray();
                }

                // Checksum
                var checkSum = new byte[12];
                BitConverter.GetBytes(deflater.Adler).CopyTo(checkSum, 0); // Adler
                BitConverter.GetBytes(nodeBytes.Length).CopyTo(checkSum, 4); // InflatedSize
                BitConverter.GetBytes(deflatedBytes.Length).CopyTo(checkSum, 8); // DeflatedSize
                stream.Write(checkSum, 0, checkSum.Length);

                // Final write
                stream.Write(deflatedBytes, 0, deflatedBytes.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal static string _serializeNode(PiaNode node, int level = 0)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            var nodeBuilder = new StringBuilder();
            var whiteSpace = new string(' ', level);
            var newLine = Environment.NewLine;

            foreach (var value in node.Values)
                nodeBuilder.AppendFormat("{0}{1}\n", whiteSpace, _serializeValue(value));

            foreach (var child in node.ChildNodes)
            {
                nodeBuilder.AppendFormat("{0}{1}{2}\n", whiteSpace, child.NodeName, "{");
                nodeBuilder.Append(_serializeNode(child, level + 1));
                nodeBuilder.AppendFormat("{0}{1}\n", whiteSpace, "}");
            }

            return nodeBuilder.ToString();
        }
        private static string _serializeValue(KeyValuePair<string, string> value)
        {
            var valueString = string.Format("{0}={1}", value.Key, value.Value);
            valueString = valueString.Replace("_str=", "=\"");

            return valueString;
        }
    }
}
