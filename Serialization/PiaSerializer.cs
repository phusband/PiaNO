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

            var dataLines = node.InnerData.Split(new char[]{ '\n'},
                                            StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataLines.Length; i++)
            {
                var curLine = dataLines[i];
                if (curLine.Contains('='))
                {
                    var value = _getValue(curLine);
                    node.Values.Add(value.Key, value.Value);
                }
                else if (curLine.Contains('{'))
                {
                    var bracketCount = 1;
                    var nodeBuilder = new StringBuilder();
                    var n = i + 1;
                    var subLine = string.Empty;

                    while (bracketCount != 0) // Iterate until the node is closed
                    {
                        subLine = dataLines[n++];
                        bracketCount += subLine.Contains('{')
                            ? 1 : subLine.Contains('}')
                            ? -1 : 0;

                        if (bracketCount != 0) // Skip the closing bracket
                            nodeBuilder.AppendLine(subLine);
                    }

                    var childNode = new PiaNode(nodeBuilder.ToString())
                    { 
                        Name = curLine.TrimEnd('{'),
                        Parent = node
                    };
                    node.ChildNodes.Add(childNode);
                    i = n - 1;
                }
            }
        }
        public static Stream Serialize(PiaNode node)
        {
            throw new NotImplementedException();
        }

        public static KeyValuePair<string, object> _getValue(string valueString)
        {
            var prop = valueString.TrimEnd(new char[] {'\n', '\r'}).Split('=');
            return new KeyValuePair<string,object>(prop[0].Trim(), prop[1].TrimStart('\"'));
        }
    }
}
