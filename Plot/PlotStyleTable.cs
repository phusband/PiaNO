using System;
using System.Collections.Generic;
using System.Linq;

namespace PiaNO.Plot
{
    public class PlotStyleTable : PiaFile, IList<PlotStyle>
    {
        protected IList<PlotStyle> InnerStyles;
        public IList<Double> Lineweights { get; set; }

        public string Description { get; set; }
        public bool AciTableAvailable { get; set; }
        public double ScaleFactor { get; set; }
        public bool ApplyFactor { get; set; }
        public double CustomLineweightDisplayUnits { get; set; }

        public PlotStyleTable()
        {
            InnerStyles = new List<PlotStyle>();
            Lineweights = new List<Double>();

            AciTableAvailable = false;
            ScaleFactor = 1.0;
            ApplyFactor = false;
            CustomLineweightDisplayUnits = 1;
        }
        protected PlotStyleTable(string rawData)
        {
            _rawData = rawData;
        }

        #region IList

        protected override void Deserialize()
        {
            base.Deserialize();
            foreach (var node in ChildNodes)
                InnerStyles.Add((PlotStyle)node);
        }


        public virtual void Add(PlotStyle item)
        {
            if (item == null)
                throw new NullReferenceException();

            if (this.Contains(item))
                throw new ArgumentException(string.Format("Plot style '{0}' already exists.", item.Name));

            InnerStyles.Add(item);
        }

        public virtual void Clear()
        {
            InnerStyles.Clear();
        }

        public bool Contains(PlotStyle item)
        {
            return InnerStyles.Contains(item);
        }

        public void CopyTo(PlotStyle[] array, int arrayIndex)
        {
            InnerStyles.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return InnerStyles.Count; }
        }

        public IEnumerator<PlotStyle> GetEnumerator()
        {
            return InnerStyles.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(PlotStyle item)
        {
            return InnerStyles.IndexOf(item);
        }

        public virtual void Insert(int index, PlotStyle item)
        {
            InnerStyles.Insert(index, item);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(PlotStyle item)
        {
            if (!this.Contains(item))
                throw new ArgumentException(string.Format("Plot style '{0}' not found."), item.Name);

            return InnerStyles.Remove(item);
        }

        public virtual void RemoveAt(int index)
        {
            InnerStyles.RemoveAt(index);
        }

        public PlotStyle this[int index]
        {
            get
            {
                return InnerStyles[index];
            }
            set
            {
                if (value != null)
                    InnerStyles[index] = value;
            }
        }

        #endregion

    }
}
