using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions
{
    public abstract class FunctionNode<T> : ChildManager, INode<T> where T : IComparable
    {
        protected FunctionNode(){}
        protected FunctionNode(IEnumerable<INode<T>> nodes) : base(nodes) {}

        public bool IsNullNode() => false;
 
        protected INode<TX> ReplaceNullNodesForComponent<TX>(INode<TX> component,int maxDepth, FunctionGenerator generator) where TX : IComparable
        {
            if (component.IsNullNode())
            {
                return generator.Choose<TX>(maxDepth - 1);
            }
            
            return component.GetNullNodeCount() > 0 ? component.ReplaceNullNodes(maxDepth - 1,generator) : component.GetCopy();
        }
        
        public new FunctionNode<T> AddChild(INode newNode)
        {
            base.AddChild(newNode);
            return this;
        }
        
        public INode<T> GetChild(int index)
        {
            return (INode<T>) base.GetChild(index);
        }
        
        public abstract string GetId();
        public abstract INode<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth);
        public abstract INode<T> GetSubTree(int nodeIndexToGet);

        public abstract T GetValue();

        public abstract INode<T> GetCopy();

        public abstract INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator);
    }
}