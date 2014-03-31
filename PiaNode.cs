using PiaNO.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PiaNO
{
    public class PiaNode : ICloneable, IEquatable<PiaNode>, IList<PiaNode>
    {
        #region Properties

        protected internal IList<PiaNode> ChildNodes
        { 
            get {return _childNodes ?? (_childNodes = new List<PiaNode>()); }
            set { _childNodes = value; }
        }
        private IList<PiaNode> _childNodes;

        protected internal Dictionary<string, string> Values
        {
            get { return _values ?? (_values = new Dictionary<string, string>()); }
            set { _values = value; }
        }
        private Dictionary<string, string> _values;

        public string NodeName { get; set; }
        public PiaFile Owner { get; protected internal set; }
        public PiaNode Parent { get; protected internal set; }
        public bool HasChildNodes
        {
            get { return ChildNodes != null && ChildNodes.Count > 0; }
        }
        public string InnerData
        {
            get { return PiaSerializer._serializeNode(this); }
        }

        #endregion

        #region Constructors

        protected PiaNode()
        {
            ChildNodes = new List<PiaNode>();
            Values = new Dictionary<string, string>();
        }
        protected internal PiaNode(string innerData)
        {
            ChildNodes = new List<PiaNode>();
            Values = new Dictionary<string, string>();

            PiaSerializer._deserializeNode(this, innerData);
        }

        #endregion

        #region Methods

        protected internal static Color? _getColor(string input)
        {
            var colorVal = int.Parse(input);
            if (colorVal == -1)
                return null;

            return Color.FromArgb(colorVal);
        }

        public virtual PiaNode this[string name]
        {
            get
            {
                if (ChildNodes.Count == 0)
                    return null;
                    //throw new ArgumentOutOfRangeException();

                return ChildNodes.FirstOrDefault(n => n.NodeName.Equals(name, 
                       StringComparison.InvariantCultureIgnoreCase));

            }
        }

        public override string ToString()
        {
            return this.NodeName;
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region IEquatable

        public bool Equals(PiaNode other)
        {
            if (this == null && other == null)
                return true;

            if (this == null || other == null)
                return false;

            return this.NodeName.Equals(other.NodeName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region IList

        public int IndexOf(PiaNode item)
        {
            return ChildNodes.IndexOf(item);
        }

        public virtual void Insert(int index, PiaNode item)
        {
            ChildNodes.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            ChildNodes.RemoveAt(index);
        }

        public PiaNode this[int index]
        {
            get
            {
                return ChildNodes[index];
            }
            set
            {
                ChildNodes[index] = value;
            }
        }

        public virtual void Add(PiaNode item)
        {
            ChildNodes.Add(item);
        }

        public virtual void Clear()
        {
            ChildNodes.Clear();
        }

        public bool Contains(PiaNode item)
        {
            return ChildNodes.Contains(item);
        }

        public void CopyTo(PiaNode[] array, int arrayIndex)
        {
            ChildNodes.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ChildNodes.Count; }
        }

        public bool IsReadOnly
        {
            get { return ChildNodes.IsReadOnly; }
        }

        public virtual bool Remove(PiaNode item)
        {
            return ChildNodes.Remove(item);
        }

        public IEnumerator<PiaNode> GetEnumerator()
        {
            return ChildNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
