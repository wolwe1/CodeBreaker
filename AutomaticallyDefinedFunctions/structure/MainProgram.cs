using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.visitors;

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

        public MainProgram<T> ReplaceNode(int nodeIndexToReplace, FunctionCreator creator, int maxDepth)
        {
            var visitor = new BranchReplacementVisitor(creator,maxDepth);
            var treeCopy = _nodeTree.GetCopy();
            //Alters tree in place
            treeCopy.Visit(visitor);
            
            return new MainProgram<T>(treeCopy);
        }

        public void Visit(INodeVisitor visitor)
        {
            _nodeTree.Visit(visitor);
        }
    }
}