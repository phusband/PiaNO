using PiaNO.Serialization;
using System;
using System.Drawing;
using System.IO;

namespace PiaNO.Plot
{
    public class PlotStyle: IEquatable<PlotStyle>
    {
        private string _rawData;

        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public string Description { get; set; }
        public Color? Color { get; set; }
        public Color? ModeColor { get; set; }
        public short ColorPolicy { get; set; }
        public bool EnableDithering
        {
            get {return ColorPolicy % 2 == 1; }
            //set;
        }
        public bool ConvertoToGrayscale
        {
            get { return ColorPolicy == 2 || ColorPolicy == 3; }
        }
        public short PhysicalPenNumber { get; set; }
        public short VirtualPenNumber { get; set; }
        public short Screen { get; set; }
        public double LinePatternSize { get; set; }
        public short Linetype { get; set; }
        public bool AdaptiveLinetype { get; set; }
        public short LineWeight { get; set; }
        public FillStyle FillStyle { get; set; }
        public EndStyle EndStyle { get; set; }
        public JoinStyle JoinStyle { get; set; }

        public PlotStyle()
        {
            Name = "Style 1";
            LocalizedName = string.Empty;
            Description = string.Empty;
            Color = null;
            ModeColor = null;
            ColorPolicy = 2;
            PhysicalPenNumber = 0;
            VirtualPenNumber = 0;
            Screen = 100;
            LinePatternSize = 0.5;
            Linetype = 31;
            AdaptiveLinetype = true;
            LineWeight = 0;
            FillStyle = FillStyle.FromObject;
            EndStyle = EndStyle.FromObject;
            JoinStyle = JoinStyle.FromObject;
        }
        internal PlotStyle(string rawData)
        {
            _rawData = rawData;
            Deserialize();
        }

        protected virtual Stream Serialize()
        {
            throw new NotImplementedException();
        }
        protected virtual void Deserialize()
        {
            PiaSerializer.Deserialize(this, _rawData);
        }

        public bool Equals(PlotStyle other)
        {
            if (this == null && other == null)
                return true;

            if (this == null || other == null)
                return false;

            return this.Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
