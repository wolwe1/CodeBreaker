using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.Extensions
{
    public abstract class AddFunc<T> : FunctionNode<T> where T : IComparable
    {
        protected AddFunc(IEnumerable<INode<T>> nodes) : base(nodes){}

        protected AddFunc() : base() { }
        
        protected AddFunc(INode<T> firstNode,INode<T> secondNode): this()
        {
            Children.Add(firstNode);
            Children.Add(secondNode);
        }

        public void Refresh(INode<T> firstNode, INode<T> secondNode)
        {
            Children.Clear();
            Children.Add(firstNode);
            Children.Add(secondNode);
        }

        public override bool IsValid()
        {
            return Children.Count == 2;
        }
    }
}