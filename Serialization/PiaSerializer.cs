using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using PiaNO.Plot;
using System.Runtime.Serialization;

namespace PiaNO.Serialization
{
    public static class PiaSerializer
    {
        public static Stream Serialize(object o)
        {
            return null;
        }

        public static void Test(object sender, string rawData)
        {
            var siri = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //siri.Deserialize()
        }

        public static void Deserialize(object sender, string rawData)
        {
            if (String.IsNullOrEmpty(rawData))
                throw new ArgumentNullException();

            var lines = rawData.Split(new char [] {'{', '}'}, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
                throw new ArgumentOutOfRangeException();
            
            if (sender is PlotStyleTable)
            {
                var currentLine = lines[0];
                var dict = _getDataStrings(currentLine);
                var psTable = (PlotStyleTable)sender;

                psTable.Description = dict["description"];
                psTable.ScaleFactor = double.Parse(dict["scale_factor"]);
                psTable.ApplyFactor = bool.Parse(dict["apply_factor"]);
                psTable.CustomLineweightDisplayUnits = int.Parse(dict["custom_lineweight_display_units"]);

                int i = 0, n = 0;
                var styles = psTable.GetEnumerator();
                while(!currentLine.Contains("custom_lineweight_table"))
                {
                    currentLine = lines[i++];
                    if (!currentLine.Contains("name="))
                        continue;

                    if (sender is ColorDependentPlotStyleTable)
                        psTable[n++] = new PlotStyle(currentLine);
                    else
                        psTable.Add(new PlotStyle(currentLine));
                }

                currentLine = lines[i++];
                psTable.Lineweights = _getLineWeights(currentLine);
            }
            else if (sender is PlotStyle)
            {
                var currentLine = lines[0];
                var dict = _getDataStrings(currentLine);
                var pStyle = (PlotStyle)sender;

                pStyle.Name = dict["name"];
                pStyle.LocalizedName = dict["localized_name"];
                pStyle.Description = dict["description"];
                pStyle.Color = _getColor(dict["color"]);
                pStyle.ModeColor = dict.ContainsKey("mode_color")
                    ? _getColor(dict["mode_color"])
                    : null;
                pStyle.ColorPolicy = short.Parse(dict["color_policy"]);
                pStyle.PhysicalPenNumber = short.Parse(dict["physical_pen_number"]);
                pStyle.VirtualPenNumber = short.Parse(dict["virtual_pen_number"]);
                pStyle.Screen = short.Parse(dict["screen"]);
                pStyle.LinePatternSize = double.Parse(dict["linepattern_size"]);
                pStyle.Linetype = short.Parse(dict["linetype"]);
                pStyle.AdaptiveLinetype = bool.Parse(dict["adaptive_linetype"]);
                pStyle.LineWeight = short.Parse(dict["lineweight"]);
                pStyle.FillStyle = (FillStyle)Enum.Parse(typeof(FillStyle), dict["fill_style"]);
                pStyle.EndStyle = (EndStyle)Enum.Parse(typeof(EndStyle), dict["end_style"]);
                pStyle.JoinStyle = (JoinStyle)Enum.Parse(typeof(JoinStyle), dict["join_style"]);
            }
        }

        private static string _getValue(string input)
        {
            var args = input.Trim().Split('=');
            if (args.Length != 2)
                throw new ArgumentOutOfRangeException();

            return args[1].TrimStart('\"');
        }

        private static Dictionary<string, string> _getDataStrings(string input)
        {
            return input.Trim(' ', '\n').Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(prop => prop.Split('='))
                                       .ToDictionary(val => val[0].Trim(), val => val.Length == 2
                                           ? val[1].TrimStart('\"')
                                           : null);
        }

        private static List<double> _getLineWeights(string input)
        {
            return input.Trim(' ', '\n').Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(prop => double.Parse(prop.Split('=')[1]))
                                        .ToList();
        }

        private static Color? _getColor(string input)
        {
            var colorVal = int.Parse(input);
            if (colorVal == -1)
                return null;

            return Color.FromArgb(colorVal);
        }

    }
}
