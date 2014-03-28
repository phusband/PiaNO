using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiaNO.Plot
{
    public class ColorDependentPlotStyleTable : PlotStyleTable
    {
        public IList<string> AciTable { get; private set; }

        public ColorDependentPlotStyleTable()
            : base()
        {
            AciTable = new string[256];
            for (int i = 0; i < AciTable.Count;)
                AciTable[i] = "Color " + ++i;

            _rebuildStyles();
        }

        protected ColorDependentPlotStyleTable(string rawData)
            :base(rawData)
        {

        }

        private void _rebuildStyles()
        {
            foreach (var name in AciTable)
            {
                var newStyle = new PlotStyle { Name = name };
                InnerStyles.Add(newStyle);
            }
        }

        public override void Add(PlotStyle item) { }
        public override void Clear()
        {
            InnerStyles.Clear();
            _rebuildStyles();
        }
        public override void Insert(int index, PlotStyle item) { }
        public override bool Remove(PlotStyle item)
        {
            return false;
        }
        public override void RemoveAt(int index) { }
        
    }
}
