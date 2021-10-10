using System;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;

namespace AutomaticallyDefinedFunctions.structure.nodes
{
    public interface INode
    {
        int GetNullNodeCount();
        int GetNodeCount();
        bool IsValid();
        
    }
    
    public interface INode<out T> : INode where T : IComparable
    {
        public T GetValue();
        INode<T> GetCopy();
        INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator);
        bool IsNullNode();
        string GetId();
        INode<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth);
        INode<T> GetSubTree(int nodeIndexToGet);
    }
}