using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions
{
    public abstract class ChildManager : INode
    {
        protected readonly List<INode> Children;

        protected ChildManager()
        {
            Children = new List<INode>();
        }

        protected ChildManager(IEnumerable<INode> nodes): this()
        {
            Children.AddRange(nodes);
        }

        public int GetChildCount()
        {
            return Children.Count;
        }
        
        public INode GetChild(int index)
        {
            if (index < 0 || index >= GetChildCount())
                throw new IndexOutOfRangeException($"Get child({index}) is out of bounds for children size {GetChildCount()}");

            return Children[index];
        }

        public void AddChild(INode newNode)
        {
            Children.Add(newNode);
        }

        public int GetNullNodeCount()
        {
            return Children.Sum(x => x.GetNullNodeCount());
        }

        public int GetNodeCount()
        {
            return Children.Sum(child => child.GetNodeCount());
        }

        public bool IsValid()
        {
            return Children.All(child => child.IsValid());
        }

        protected INode<T> GetChildAs<T>(int index) where T : IComparable
        {
            return Children[index] as INode<T>;
        }
        
        protected INode<T> GetChildCopyAs<T>(int index) where T : IComparable
        {
            var childAs = GetChildAs<T>(index);

            return childAs.GetCopy();
        }

        protected void RegisterChildren(IEnumerable<INode> nodes)
        {
            Children.AddRange(nodes);
        }
    }
}