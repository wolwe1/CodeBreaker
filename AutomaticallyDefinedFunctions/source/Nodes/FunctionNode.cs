using System.Collections.Generic;
using AutomaticallyDefinedFunctions.Nodes;

namespace AutomaticallyDefinedFunctions.source.Nodes
{
    public abstract class FunctionNode<T> : INode<T>
    {
        protected readonly List<INode<T>> Children;

        protected FunctionNode(List<INode<T>> children)
        {
            Children = children;
        }

        public abstract T GetValue();
    }
}