using System.Collections.Generic;
using System.Linq;

namespace PiaNO.Plot
{
    public class PlotStyleTable : PiaFile
    {
        #region Properties

        public IList<PlotStyle> PlotStyles
        {
            get { return _getPlotStyles(); }
            set { _setStyles(value); }
        }
        public IDictionary<int, double> Lineweights
        {
            get { return _getLineWeights(); }
        }

        public string Description
        {
            get { return Values["description_str"]; }
            set { Values["description_str"] = value; }
        }

        public bool AciTableAvailable
        {
            get { return bool.Parse(Values["aci_table_available"]); }
            set
            {
                Values["aci_table_available"] = value
                ? "TRUE"
                : "FALSE";
            }
        }

        public double ScaleFactor
        {
            get { return double.Parse(Values["scale_factor"]); }
            set { Values["scale_factor"] = value.ToString(); }
        }

        public bool ApplyFactor
        {
            get { return bool.Parse(Values["apply_factor"]); }
            set
            {
                Values["apply_factor"] = value
                ? "TRUE"
                : "FALSE";
            }
        }

        public double CustomLineweightDisplayUnits
        {
            get { return double.Parse(Values["custom_lineweight_display_units"]); }
            set { Values["custom_lineweight_display_units"] = value.ToString(); }
        }

        #endregion

        #region Constructors

        public PlotStyleTable() : base()
        {
            AciTableAvailable = false;
            ScaleFactor = 1.0;
            ApplyFactor = false;
            CustomLineweightDisplayUnits = 1;
        }
        public PlotStyleTable(string fileName)
            : base(fileName) { }

        #endregion

        #region Methods

        protected virtual List<PlotStyle> _getPlotStyles()
        {
            if (!HasChildNodes)
                return null;

            var styleNode = this["plot_style"];
            if (styleNode == null || !styleNode.HasChildNodes)
                return null;

            var styles = styleNode.ChildNodes;
            return styles.Select(s => new PlotStyle(s)).ToList();
        }
        protected Dictionary<int, double> _getLineWeights()
        {
            if (!HasChildNodes)
                return null;

            var weightNode = this["custom_lineweight_table"];
            if (weightNode == null)
                return null;

            var weights = weightNode.Values;
            return weights.ToDictionary(w => int.Parse(w.Key), w => double.Parse(w.Value));
        }

        private void _setStyles(IList<PlotStyle> value)
        {
            var styleNode = this["plot_style"];
            if (styleNode == null)
                return;

            styleNode.ChildNodes = (List<PiaNode>)value;
        }

        #endregion
    }
}
