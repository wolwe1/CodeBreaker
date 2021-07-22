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

        public void AddChild(INode<T> newNode)
        {
            Children.Add(newNode);
        }
        public abstract T GetValue();

        public abstract bool IsValid();
    }
}