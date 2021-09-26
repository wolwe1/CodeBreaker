using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes;

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
        public override string GetId()
        {
            return $"{NodeCategory.Add}<{typeof(T)},{typeof(T)}>[{Children.ElementAt(0).GetId()}{Children.ElementAt(1).GetId()}]";
        }

        public override int GetNodeCount()
        {
            return Children.ElementAt(0).GetNodeCount() +
                   Children.ElementAt(1).GetNodeCount();
        }

        public override INode<T> GetSubTree(int nodeIndexToGet)
        {
            var index = nodeIndexToGet;
            if (index-- == 0)
                return Children[0];

            if (index - Children[0].GetNodeCount() <= 0)
                return Children[0].GetSubTree(--index);

            index -= Children[0].GetNodeCount();
            
            if (index-- == 0)
                return Children[1];

            if (index - Children[1].GetNodeCount() <= 0)
                return Children[1].GetSubTree(--index);

            throw new Exception("Sub tree could not be found");
        }
    }
}