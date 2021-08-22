using System;

namespace AutomaticallyDefinedFunctions.source.nodes
{
    /// <summary>
    /// Node trees behave the same as individual nodes, but can provide additional functionality
    /// </summary>
    /// <typeparam name="T"> The return type of the node tree</typeparam>
    public class NodeTree<T> : INode<T> where T : IComparable
    {
        private readonly INode<T> _root;

        public NodeTree(INode<T> root)
        {
            _root = root;
        }

        public NodeTree()
        {
            _root = null;
        }

        public T GetValue()
        {
            return _root.GetValue();
        }

        public bool IsValid()
        {
            return _root != null && _root.IsValid();
        }

        public int GetNullNodeCount()
        {
            return _root?.GetNullNodeCount() ?? 0;
        }
    }
}