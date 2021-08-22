using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace AutomaticallyDefinedFunctions.source.nodes
{
    public abstract class FunctionNode<T> : INode<T> where T : IComparable
    {
        protected readonly List<INode<T>> Children;

        protected FunctionNode()
        {
            Children = new List<INode<T>>();
        }

        protected FunctionNode(IEnumerable<INode<T>> nodes): this()
        {
            Children.AddRange(nodes);
        }

        /// <summary>
        /// Register the nodes considered as children of the function. To be used by inheritors
        /// </summary>
        /// <param name="children">The children to register</param>
        protected FunctionNode<T> RegisterChildren(List<INode<T>> children)
        {
            var validChildren = Validate(children);
            Children.Clear();
            Children.AddRange(validChildren);

            return this;
        }

        private List<INode<T>> Validate(List<INode<T>> children)
        {
            return children.Where(child => child != null).ToList();
        }

        /// <summary>
        /// Register a node as a child of the function
        /// </summary>
        /// <param name="child">The child to add</param>
        /// <returns>The function</returns>
        protected FunctionNode<T> RegisterChild(INode<T> child)
        {
            if(child != null)
                Children.Add(child);
            return this;
        }

        public int GetChildCount()
        {
            return Children.Count;
        }
        
        public INode<T> GetChild(int index)
        {
            if (index < 0 || index >= GetChildCount())
                throw new IndexOutOfRangeException($"Get child({index}) is out of bounds for children size {GetChildCount()}");

            return Children[index];
        }

        public FunctionNode<T> AddChild(INode<T> newNode)
        {
            Children.Add(newNode);
            return this;
        }
        public abstract T GetValue();

        public abstract bool IsValid();

        public abstract int GetNullNodeCount();
        // public int GetNullNodeCount()
        // {
        //     return Children.Sum(child => child.GetNullNodeCount());
        // }
    }
}