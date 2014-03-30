using System.Collections.Generic;

namespace PiaNO.Plot
{
    public class ColorDependentPlotStyleTable : PlotStyleTable
    {
        #region Properties

        public IDictionary<string, string> AciTable
        {
            get { return _getAciTable(); }
        }

        #endregion

        #region Constructors

        public ColorDependentPlotStyleTable() : base()
        {
        }
        protected ColorDependentPlotStyleTable(string rawData) :base(rawData)
        {

        }

        #endregion

        #region Methods

        protected override List<PlotStyle> _getPlotStyles()
        {
            var styles = new List<PlotStyle>();
            foreach (var kvp in AciTable)
                styles.Add(new PlotStyle
                { 
                    NodeName = kvp.Key,
                    Name = kvp.Value
                });
            return styles;
        }
        private IDictionary<string, string> _getAciTable()
        {
            if (!HasChildNodes)
                return null;

            var aciNode = this["aci_table"];
            if (aciNode == null)
                return null;

            return aciNode.Values;
        }

        public override void Add(PiaNode item) { }
        public override void Clear()
        {
            PlotStyles.Clear();
        }
        public override void Insert(int index, PiaNode item) { }
        public override bool Remove(PiaNode item)
        {
            return false;
        }
        public override void RemoveAt(int index) { }

        #endregion

    }
}
