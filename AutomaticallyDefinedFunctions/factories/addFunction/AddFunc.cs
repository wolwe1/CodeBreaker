using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.factories.addFunction
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

        public AddFunc<T> Refresh(INode<T> firstNode, INode<T> secondNode)
        {
            Children.Clear();
            Children.Add(firstNode);
            Children.Add(secondNode);

            return this;
        }

        public override bool IsValid()
        {
            return Children.All(child => child.IsValid());
        }

        public override int GetNullNodeCount()
        {
            return Children.Sum(x => x.GetNullNodeCount());
        }
    }
}