using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiaNO
{
    public sealed class PiaData
    {
        string _dataString;

        public PiaData(string dataString)
        {
            this._dataString = dataString;
        }

        public IEnumerable<PiaNode> GetChildren(PiaNode node)
        {
            return null;
        }

        public string GetNodeString(PiaNode node)
        {
            return null;
        }
    }
}
