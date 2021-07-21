using System;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.source
{
    public class FunctionDefinition<T> where T : IComparable
    {
        private readonly NodeTree<T> _nodeTree;

        public FunctionDefinition(NodeTree<T> nodeTree)
        {
            _nodeTree = nodeTree;
        }
    }
}