//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PiaNO.Serialization;

namespace PiaNO
{
    public class PiaNode : ICloneable, IEquatable<PiaNode>, IEnumerable<PiaNode>
    {
        #region Properties

        private IList<PiaNode> _childNodes;

        protected internal IList<PiaNode> ChildNodes
        {
            get { return _childNodes ?? (_childNodes = new List<PiaNode>()); }
            set { _childNodes = value; }
        }
        protected internal bool HasChildNodes
        {
            get { return ChildNodes != null && ChildNodes.Count > 0; }
        }
        protected internal string InnerData
        {
            get { return PiaSerializer._serializeNode(this); }
        }
        protected internal string NodeName { get; set; }
        protected internal PiaFile Owner { get; set; }
        protected internal PiaNode Parent { get; set; }

        protected internal Dictionary<string, string> Values
        {
            get { return _values ?? (_values = new Dictionary<string, string>()); }
            set { _values = value; }
        }
        private Dictionary<string, string> _values;

        #endregion

        #region Constructors

        protected internal PiaNode()
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

        protected string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (!Values.ContainsKey(key))
                Values.Add(key, string.Empty);
            
            return Values[key];
        }
        protected void SetValue(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (!Values.ContainsKey(key))
                Values.Add(key, value);
            else
                Values[key] = value;
        }
        protected internal static Color? GetColor(string input)
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

        #region IEnumerable

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
