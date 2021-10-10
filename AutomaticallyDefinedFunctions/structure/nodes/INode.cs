using System;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.state;

namespace AutomaticallyDefinedFunctions.structure.nodes
{
    public interface INode<T> where T : IComparable
    {
        public T GetValue();

        public bool IsValid();

        public int GetNullNodeCount();
        INode<T> GetCopy();
        INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator);
        bool IsNullNode();
        string GetId();
        int GetNodeCount();
        INode<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth);
        INode<T> GetSubTree(int nodeIndexToGet);
        void Visit(INodeVisitor visitor);
    }
}