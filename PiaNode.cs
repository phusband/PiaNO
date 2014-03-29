using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using PiaNO.Serialization;

namespace PiaNO
{
    public class PiaNode : ICloneable, IEnumerable
    {
        protected internal IList<PiaNode> ChildNodes { get; set; }
        protected internal Dictionary<string, object> Values { get; set; }

        public string Name { get; set; }
        public PiaFile Owner { get; protected internal set; }
        public PiaNode Parent { get; protected internal set; }
        public bool HasChildNodes
        {
            get { return ChildNodes.Count > 0; }
        }
        public string InnerData { get; protected set; }

        protected PiaNode()
        {
            ChildNodes = new List<PiaNode>();
            Values = new Dictionary<string, object>();
        }

        protected internal PiaNode(string innerData)
        {
            ChildNodes = new List<PiaNode>();
            Values = new Dictionary<string, object>();

            InnerData = innerData;
            Deserialize();
        }

        public virtual PiaNode this[string name]
        {
            get
            {
                if (ChildNodes.Count == 0)
                    throw new ArgumentOutOfRangeException();

                return ChildNodes.FirstOrDefault(n => n.Name.Equals(name, 
                       StringComparison.InvariantCultureIgnoreCase));

            }
        }

        protected virtual void Deserialize()
        {
            PiaSerializer.Deserialize(this);               
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region ICloneable

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region IEnumerable

        public IEnumerator GetEnumerator()
        {
            return ChildNodes.GetEnumerator();
        }

        #endregion
    }
}
