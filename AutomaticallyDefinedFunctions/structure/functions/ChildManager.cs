using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.visitors;

namespace AutomaticallyDefinedFunctions.structure.functions
{
    public abstract class ChildManager : INode
    {
        protected readonly List<INode> Children;
        private readonly int _expectedChildrenAmount;

        private ChildManager()
        {
            Children = new List<INode>();
        }

        protected ChildManager(IEnumerable<INode> nodes): this()
        {
            Children.AddRange(nodes);
            _expectedChildrenAmount = Children.Count;
        }

        protected ChildManager(int expectedChildrenAmount) : this()
        {
            _expectedChildrenAmount = expectedChildrenAmount;
        }

        public int GetChildCount()
        {
            return Children.Count;
        }
        
        public INode GetChild(int index)
        {
            return index >= GetChildCount() ? null : Children[index];
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
            return Children.Count == _expectedChildrenAmount && Children.All(child => child != null && child.IsValid());
        }

        public INode<T> GetChildAs<T>(int index) where T : IComparable
        {
            //Enables property based getters without throwing
            if (index > Children.Count)
                return null;
            
            return Children[index] as INode<T>;
        }
        
        protected INode<T> GetChildCopyAs<T>(int index) where T : IComparable
        {
            var childAs = GetChildAs<T>(index);

            return childAs?.GetCopy();
        }

        protected void RegisterChildren(IEnumerable<INode> nodes)
        {
            Children.AddRange(nodes);
        }
        
        public void Visit(INodeVisitor visitor)
        {
            visitor.Accept(this);

            Children.ForEach(child => child.Visit(visitor));
        }
        
        public abstract bool IsNullNode();
        public abstract string GetId();
        
    }
}