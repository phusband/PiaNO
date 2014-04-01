//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
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
            AciTableAvailable = true;
        }
        public ColorDependentPlotStyleTable(string innerData)
            : base(innerData) { }

        #endregion

        #region Methods

        private IDictionary<string, string> _getAciTable()
        {
            if (!HasChildNodes)
                return null;

            var aciNode = this["aci_table"];
            if (aciNode == null)
                return null;

            return aciNode.Values;
        }

        public override void AddStyle(PlotStyle style)
        {
            throw new NotSupportedException("Cannot add styles to .ctb files.");
        }

        #endregion
    }
}
