using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;

namespace AutomaticallyDefinedFunctions.structure.nodes
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

        public bool IsNullNode()
        {
            return false;
        }

        protected int GetNullNodeCountForComponent<X>(INode<X> component) where X : IComparable => component?.GetNullNodeCount() ?? 0;
        
        protected INode<TX> ReplaceNullNodesForComponent<TX>(INode<TX> component,int maxDepth, FunctionGenerator generator) where TX : IComparable
        {
            if (component.IsNullNode())
            {
                return generator.Choose<TX>(maxDepth - 1);
            }
            
            return component.GetNullNodeCount() > 0 ? component.ReplaceNullNodes(maxDepth - 1,generator) : component.GetCopy();
        }
        
        public abstract string GetId();
        public abstract int GetNodeCount();
        public abstract INode<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth);
        public abstract INode<T> GetSubTree(int nodeIndexToGet);

        public abstract T GetValue();

        public abstract bool IsValid();

        public abstract int GetNullNodeCount();

        public abstract INode<T> GetCopy();

        public abstract INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator);
    }
}