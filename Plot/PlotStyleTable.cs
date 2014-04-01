using System;
using System.Collections.Generic;
using System.Linq;

namespace PiaNO.Plot
{
    public class PlotStyleTable : PiaFile
    {
        #region Properties

        private int _nextStyleNode
        {
            get { return _getNextStyleNode(); }
        }

        public IEnumerable<PlotStyle> PlotStyles
        {
            get { return _getPlotStyles().ToList().AsReadOnly(); }
        }

        public IDictionary<int, double> Lineweights
        {
            get { return _getLineWeights(); }
        }

        public string Description
        {
            get { return Values["description_str"]; }
            set { SetValue("description_str", value); }
        }

        public bool AciTableAvailable
        {
            get { return bool.Parse(Values["aci_table_available"]); }
            set { SetValue("aci_table_available", value.ToString().ToUpper()); }
        }

        public double ScaleFactor
        {
            get { return double.Parse(Values["scale_factor"]); }
            set { SetValue("scale_factor", value.ToString()); }
        }

        public bool ApplyFactor
        {
            get { return bool.Parse(Values["apply_factor"]); }
            set { SetValue("apply_factor", value.ToString().ToUpper()); }
        }

        public double CustomLineweightDisplayUnits
        {
            get { return double.Parse(Values["custom_lineweight_display_units"]); }
            set { SetValue("custom_lineweight_display_units", value.ToString()); }
        }

        #endregion

        #region Constructors

        public PlotStyleTable() : base()
        {
            ScaleFactor = 1.0;
            ApplyFactor = false;
            CustomLineweightDisplayUnits = 1;
        }
        public PlotStyleTable(string fileName)
            : base(fileName) { }

        #endregion

        #region Methods

        private int _getNextStyleNode()
        {
            var styleNode = this["plot_style"];
            if (styleNode == null)
                throw new NotImplementedException("Create style node if it doesn't exist!");

            return !styleNode.HasChildNodes
                    ? 0
                    : styleNode.ChildNodes.Count;
        }

        protected virtual IEnumerable<PlotStyle> _getPlotStyles()
        {
            if (!HasChildNodes)
                return null;

            var styleNode = this["plot_style"];
            if (styleNode == null || !styleNode.HasChildNodes)
                return null;

            var styles = styleNode.ChildNodes;
            return styles.Select(s => new PlotStyle(s));
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

        public virtual void AddStyle(PlotStyle style)
        {
            if (style == null)
                throw new ArgumentNullException("style");

            if (PlotStyles.Contains(style))
                throw new ArgumentException(string.Format("Style '{0}' already exists.", style.Name));

            var styleNode = this["plot_style"];
            if (styleNode == null)
                throw new NotImplementedException("Create style node if it doesn't exist!");

            style.NodeName = _nextStyleNode.ToString();
            styleNode.ChildNodes.Add(style);
        }

        #endregion
    }
}
