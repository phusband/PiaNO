//
//  Copyright © 2015 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PiaNO
{
    public class PiaNode : Node<PiaNode>, IEquatable<PiaNode>
    {
        #region Properties

        private readonly PiaData _data;
        private string _innerData;
        private Dictionary<string, string> _values;

        public PiaData Data
        {
            get { return _data; }
        }
        public string InnerData
        {
            get { return _innerData; }
            set { _innerData = value; }
        }
        public string Name{ get; set; }
        public Dictionary<string, string> Values
        {
            get { return _values ?? (_values = new Dictionary<string, string>()); }
            set { _values = value; }
        }
        
        #endregion

        #region Constructors

        protected PiaNode()
        {
        }
        
        internal PiaNode(string name, string innerData)
            : this(name, innerData, null) { }

        internal PiaNode(string name, string innerData, PiaNode parent)
        {
            //this._data = data;
            this.Name = name;

            if (parent != null)
            {
                this.Parent = parent;
                this.Root = parent.Root;
            }
        }

        #endregion

        #region Methods

        protected internal string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (!Values.ContainsKey(key))
                Values.Add(key, string.Empty);
            
            return Values[key];
        }
        protected internal void SetValue(string key, string value)
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
                if (!HasChildren)
                    return null;
                    //throw new ArgumentOutOfRangeException();

                return Children.FirstOrDefault(n => n.Name.Equals(name, 
                       StringComparison.OrdinalIgnoreCase));

            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #region IEquatable

        public bool Equals(PiaNode other)
        {
            if (this == null && other == null)
                return true;

            if (this == null || other == null)
                return false;

            return this.Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        protected override IEnumerable<PiaNode> GetChildren()
        {
            throw new NotImplementedException();
        }

        protected override PiaNode GetParent()
        {
            throw new NotImplementedException();
        }
    }
}
