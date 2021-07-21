using System;
using System.Collections.Generic;

namespace AutomaticallyDefinedFunctions.source.nodes
{
    public abstract class FunctionNode<T> : INode<T> where T : IComparable
    {
        protected readonly List<INode<T>> Children;

        public FunctionNode()
        {
            Children = new List<INode<T>>();
        }
        
        public FunctionNode(IEnumerable<INode<T>> nodes): this()
        {
            Children.AddRange(nodes);
        }

        public abstract T GetValue();

        public abstract bool IsValid();
    }
}