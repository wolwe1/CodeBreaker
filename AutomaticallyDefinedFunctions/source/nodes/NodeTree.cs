using System;

namespace AutomaticallyDefinedFunctions.source.nodes
{
    public class NodeTree<T> where T : IComparable
    {
        private readonly INode<T> _root;

        public NodeTree(INode<T> root)
        {
            _root = root;
        }

        public T GetValue()
        {
            return _root.GetValue();
        }
    }
}