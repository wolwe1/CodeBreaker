using System;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.source
{
    public class MainProgram<T> where T : IComparable
    {
        private readonly NodeTree<T> _nodeTree;

        public MainProgram(NodeTree<T> nodeTree)
        {
            _nodeTree = nodeTree;
        }

        public T GetValue()
        {
            return _nodeTree.GetValue();
        }

        public bool IsValid()
        {
            return _nodeTree != null && _nodeTree.IsValid();
        }
    }
}