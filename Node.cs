//
//  Copyright © 2015 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using PiaNO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PiaNO
{
    public abstract class Node<T> : INode<T>
        where T : class
    {
        #region Properties

        private IEnumerable<T> _ancestors;
        private IEnumerable<T> _children;
        private IEnumerable<T> _descendants;
        private bool _isDisposed;
        private T _parent;
        private T _root;

        public IEnumerable<T> Ancestors
        {
            get { return _ancestors ?? (_ancestors = GetAncestors().Select(a => a)); }
        }

        public IEnumerable<T> Children
        {
            get { return _children ?? (_children = GetChildren().Select(c => c)); }
        }

        public IEnumerable<T> Descendants
        {
            get { return _descendants ?? (_descendants = GetDescendants().Skip(1).Select(d => d)); }
        }

        public bool HasChildren
        {
            get { return Children.Any(); }
        }

        public bool IsOrphaned
        {
            get { return Parent == null; }
        }

        public bool IsRootNode
        {
            get { return Level == 0; }
        }

        public int Level
        {
            get { return Ancestors.Count(); }
        }

        public T Parent
        {
            get { return _parent ?? (_parent = GetParent()); }
            protected set { _parent = value; }
        }

        public T Root
        {
            get { return _root ?? (_root = GetRoot()); }
            protected set { _root = value; }
        }

        #endregion Properties

        #region Methods

        protected virtual IEnumerable<T> GetAncestors()
        {
            if (Parent == null)
                yield break;

            var relative = Parent as INode<T>;
            if (relative == null)
                throw new InvalidCastException("Invalid Parent Type.");

            while (relative != null)
            {
                yield return (T)relative;
                relative = relative.Parent as INode<T>;
            }
        }

        protected abstract IEnumerable<T> GetChildren();

        protected virtual IEnumerable<T> GetDescendants()
        {
            var nodes = new Stack<INode<T>>();
            nodes.Push(this);

            while (nodes.Any())
            {
                var current = nodes.Pop();
                yield return (T)current;

                foreach (var childNode in current.Children)
                    nodes.Push((INode<T>)childNode);
            }
        }

        protected abstract T GetParent();

        protected virtual T GetRoot()
        {
            return Ancestors.LastOrDefault();
        }

        public virtual void Refresh()
        {
            _ancestors = null;
            _children = null;
            _descendants = null;
            _parent = null;
            _root = null;
        }

        #endregion Methods

        #region ICloneable

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }

        protected virtual T Clone()
        {
            return (T)this.MemberwiseClone();
        }

        #endregion ICloneable

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                if (_descendants != null)
                {
                    foreach (INode<T> d in _descendants)
                        d.Dispose();
                }

                _isDisposed = true;
            }
        }

        #endregion IDisposable
    }
}