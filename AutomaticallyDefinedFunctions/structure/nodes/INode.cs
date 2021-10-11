using System;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.visitors;

namespace AutomaticallyDefinedFunctions.structure.nodes
{
    public interface INode
    {
        int GetNullNodeCount();
        int GetNodeCount();
        bool IsValid();
        bool IsNullNode();
        string GetId();
        void Visit(INodeVisitor visitor);
    }
    
    public interface INode<out T> : INode where T : IComparable
    {
        public T GetValue();
        INode<T> GetCopy();
        INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator);
    }
}