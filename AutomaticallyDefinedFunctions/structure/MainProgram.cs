using System;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure
{
    public class MainProgram<T> where T : IComparable
    {
        private readonly INode<T> _nodeTree;

        public MainProgram(INode<T> nodeTree)
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
        
        public string GetId()
        {
            return $"Main{_nodeTree.GetId()}";
        }

        public MainProgram<T> GetCopy()
        {
            return new MainProgram<T>(_nodeTree.GetCopy());
        }

        public int GetNodeCount()
        {
            return _nodeTree.GetNodeCount();
        }

        public MainProgram<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            return new MainProgram<T>(_nodeTree.ReplaceNode(nodeIndexToReplace, generator,maxDepth));
        }

        public INode<T> GetSubTree(int nodeIndexToGet)
        {
            return _nodeTree.GetSubTree(nodeIndexToGet);
        }
    }
}