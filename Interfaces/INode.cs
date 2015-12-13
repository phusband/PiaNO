using System;
using System.Collections.Generic;

namespace PiaNO.Interfaces
{
    public interface INode<TNode> : ICloneable, IDisposable
    {
        IEnumerable<TNode> Ancestors { get; }
        IEnumerable<TNode> Children { get; }
        IEnumerable<TNode> Descendants { get; }
        bool HasChildren { get; }
        bool IsOrphaned { get; }
        bool IsRootNode { get; }
        int Level { get; }
        TNode Parent { get; }
        TNode Root { get; }

        void Refresh();
    }
}