using System;
using System.Drawing;

namespace PiaNO.Plot
{
    public class PlotStyle: PiaNode
    {
        #region Properties

        public string Name
        {
            get { return Values["name_str"]; }
            set { Values["name_str"] = value; }
        }
        public string LocalizedName
        {
            get { return Values["localized_name_str"]; }
            set { Values["localized_name_str"] = value; }
        }
        public string Description
        {
            get { return Values["description_str"]; }
            set { Values["description_str"] = value; }
        }
        public Color? Color
        {
            get
            {
                if (!Values.ContainsKey("color"))
                    return null;
                return _getColor(Values["color"]);
            }
            //set
            //{
            //    if (!Values.ContainsKey("color"))
            //        Values.Add("color", value.ToString());
            //    else
            //        Values["color"] = value.ToString();
            //}
        }
        public Color? ModeColor
        {
            get
            {
                if (!Values.ContainsKey("mode_color"))
                    return null;
                return _getColor(Values["mode_color"]);
            }
            set
            {
                if (Values.ContainsKey("mode_color"))
                    Values["mode_color"] = value.ToString();
            }
        }
        public short ColorPolicy
        {
            get { return short.Parse(Values["color_policy"]); }
            set { Values["color_policy"] = value.ToString(); }
        }
        public bool EnableDithering
        {
            get { return ColorPolicy % 2 == 1; }
            //set;
        }
        public bool ConvertoToGrayscale
        {
            get { return ColorPolicy == 2 || ColorPolicy == 3; }
        }
        public short PhysicalPenNumber
        {
            get { return short.Parse(Values["physical_pen_number"]); }
            set { Values["physical_pen_number"] = value.ToString(); }
        }
        public short VirtualPenNumber
        {
            get { return short.Parse(Values["virtual_pen_number"]); }
            set { Values["virtual_pen_number"] = value.ToString(); }
        }
        public short Screen
        {
            get { return short.Parse(Values["screen"]); }
            set { Values["screen"] = value.ToString(); }
        }
        public double LinePatternSize
        {
            get { return double.Parse(Values["linepattern_size"]); }
            set { Values["linepattern_size"] = value.ToString(); }
        }
        public Linetype Linetype
        {
            get { return (Linetype)Enum.Parse(typeof(Linetype), Values["linetype"]); }
            set { Values["linetype"] = ((int)value).ToString(); }
        }
        public bool AdaptiveLinetype
        {
            get { return bool.Parse(Values["adaptive_linetype"]); }
            set { Values["adaptive_linetype"] = value.ToString().ToUpper(); }
        }
        public short LineWeight
        {
            get { return short.Parse(Values["lineweight"]); }
            set { Values["lineweight"] = value.ToString(); }
        }
        public FillStyle FillStyle
        {
            get { return (FillStyle)Enum.Parse(typeof(FillStyle), Values["fill_style"]); }
            set { Values["fill_style"] = ((int)value).ToString(); }
        }
        public EndStyle EndStyle
        {
            get { return (EndStyle)Enum.Parse(typeof(EndStyle), Values["end_style"]); }
            set { Values["end_style"] = ((int)value).ToString(); }
        }
        public JoinStyle JoinStyle
        {
            get { return (JoinStyle)Enum.Parse(typeof(JoinStyle), Values["join_style"]); }
            set { Values["join_style"] = ((int)value).ToString(); }
        }

        #endregion

        #region Constructors

        public PlotStyle() : base()
        {
            NodeName = "New style";
            LocalizedName = string.Empty;
            Description = string.Empty;
            //Color = null;
            ModeColor = null;
            ColorPolicy = 2;
            PhysicalPenNumber = 0;
            VirtualPenNumber = 0;
            Screen = 100;
            LinePatternSize = 0.5;
            Linetype = Plot.Linetype.FromObject;
            AdaptiveLinetype = true;
            LineWeight = 0;
            FillStyle = FillStyle.FromObject;
            EndStyle = EndStyle.FromObject;
            JoinStyle = JoinStyle.FromObject;
        }
        protected internal PlotStyle(string innerData) : base(innerData) { }
        internal PlotStyle(PiaNode baseNode)
        {
            NodeName = baseNode.NodeName;
            Parent = baseNode.Parent;
            Owner = baseNode.Owner;
            Values = baseNode.Values;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
