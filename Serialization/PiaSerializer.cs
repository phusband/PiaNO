using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PiaNO.Serialization
{
    public static class PiaSerializer
    {
        public static void Deserialize(PiaNode node)
        {
            if (node == null)
                throw new ArgumentNullException("Node");

            if (String.IsNullOrEmpty(node.InnerData))
                throw new ArgumentNullException("InnerData");

            var dataLines = node.InnerData.Split(new char[]{'\r', '\n'},
                                            StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataLines.Length; i++)
            {
                var curLine = dataLines[i];
                if (curLine.Contains('='))
                {
                    var value = _getValue(curLine);

                    if (!node.Values.ContainsKey(value.Key))
                        node.Values.Add(value.Key, value.Value);
                    else
                        node.Values[value.Key] = value.Value;
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
                        Parent = node,
                    };
                    if (node is PiaFile)
                        childNode.Owner = (PiaFile)node;
                    else
                        childNode.Owner = node.Owner;

                    node.ChildNodes.Add(childNode);
                    i = n - 1;
                }
            }
        }
        public static Stream Serialize(PiaNode node)
        {
            throw new NotImplementedException();
        }

        public static KeyValuePair<string, string> _getValue(string valueString)
        {
            var prop = valueString.TrimEnd(new char[] {'\r', '\n'}).Split('=');
            return new KeyValuePair<string,string>(prop[0].Trim(), prop[1].TrimStart('\"'));
        }
    }
}
