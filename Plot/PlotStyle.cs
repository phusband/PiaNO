using System;
using System.Drawing;

namespace PiaNO.Plot
{
    public class PlotStyle: PiaNode
    {
        #region Properties

        private const string USE_OBJECT_COLOR = "-1006632961";

        public string Name
        {
            get { return GetValue("name_str"); }
            set { SetValue("name_str", value); }
        }
        public string LocalizedName
        {
            get { return GetValue("localized_name_str"); }
            set { SetValue("localized_name_str", value); }
        }
        public string Description
        {
            get { return GetValue("description_str"); }
            set { SetValue("description_str", value); }
        }
        public Color? Color
        {
            get
            {
                var color = GetValue("color");
                return color != null ? GetColor(color) : null;
            }
            set { SetValue("color", value == null ? USE_OBJECT_COLOR : value.ToString()); }
        }
        public Color? ModeColor
        {
            get
            {
                var color = GetValue("mode_color");
                return color != null ? GetColor(color) : null;
            }
            set { SetValue("mode_color", value == null ? USE_OBJECT_COLOR : value.ToString()); }
        }
        public short ColorPolicy
        {
            get { return short.Parse(GetValue("color_policy")); }
            set { SetValue("color_policy", value.ToString()); }
        }
        public bool EnableDithering
        {
            get { return ColorPolicy % 2 == 1; } // Pretty sure this is bitwise
            
        }
        public bool ConvertoToGrayscale
        {
            get { return ColorPolicy == 2 || ColorPolicy == 3; } // Pretty sure this is bitwise
        }
        public short PhysicalPenNumber
        {
            get { return short.Parse(GetValue("physical_pen_number")); }
            set { SetValue("physical_pen_number", value.ToString()); }
        }
        public short VirtualPenNumber
        {
            get { return short.Parse(GetValue("virtual_pen_number")); }
            set { SetValue("virtual_pen_number", value.ToString()); }
        }
        public short Screen
        {
            get { return short.Parse(GetValue("screen")); }
            set { SetValue("screen", value.ToString()); }
        }
        public double LinePatternSize
        {
            get { return double.Parse(GetValue("linepattern_size")); }
            set { SetValue("linepattern_size", value.ToString()); }
        }
        public Linetype Linetype
        {
            get { return (Linetype)Enum.Parse(typeof(Linetype), GetValue("linetype")); }
            set { SetValue("linetype", ((int)value).ToString()); }
        }
        public bool AdaptiveLinetype
        {
            get { return bool.Parse(GetValue("adaptive_linetype")); }
            set { SetValue("adaptive_linetype", value.ToString().ToUpper()); }
        }
        public short LineWeight
        {
            get { return short.Parse(GetValue("lineweight")); }
            set { SetValue("lineweight", value.ToString()); }
        }
        public FillStyle FillStyle
        {
            get { return (FillStyle)Enum.Parse(typeof(FillStyle), GetValue("fill_style")); }
            set { SetValue("fill_style", ((int)value).ToString()); }
        }
        public EndStyle EndStyle
        {
            get { return (EndStyle)Enum.Parse(typeof(EndStyle), GetValue("end_style")); }
            set { SetValue("end_style", ((int)value).ToString()); }
        }
        public JoinStyle JoinStyle
        {
            get { return (JoinStyle)Enum.Parse(typeof(JoinStyle), GetValue("join_style")); }
            set { SetValue("join_style", ((int)value).ToString()); }
        }

        #endregion

        #region Constructors

        public PlotStyle()
            : base()
        {
            Name = "New style"; // needs to be iterative based on existing nodes
            LocalizedName = string.Empty;
            Description = string.Empty;
            Color = null;
            ModeColor = null;
            ColorPolicy = 1;
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
        protected internal PlotStyle(string innerData)
            : base(innerData) { }

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
