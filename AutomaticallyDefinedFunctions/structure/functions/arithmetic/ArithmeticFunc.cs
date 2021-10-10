using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.state;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic
{
    public abstract class ArithmeticFunc<T> : FunctionNode<T> where T : IComparable
    {
        private readonly string _category;

        protected ArithmeticFunc(IEnumerable<INode<T>> nodes, string category) : base(nodes)
        {
            _category = category;
        }

        protected ArithmeticFunc(string category)
        {
            _category = category;
        }
        
        protected ArithmeticFunc(INode<T> firstNode,INode<T> secondNode,string category) : this(category)
        {
            Children.Add(firstNode);
            Children.Add(secondNode);
        }
        
        public override bool IsValid()
        {
            return Children.All(child => child.IsValid());
        }

        public override int GetNullNodeCount()
        {
            return Children.Sum(x => x.GetNullNodeCount());
        }
        public override int GetNodeCount()
        {
            //TODO: Investigate
            return Children.ElementAt(0).GetNodeCount() +
                   Children.ElementAt(1).GetNodeCount();
        }

        public override string GetId()
        {
            return $"{_category}<{typeof(T)},{typeof(T)}>[{Children.ElementAt(0).GetId()}{Children.ElementAt(1).GetId()}]";
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

        protected (INode<T>,INode<T>) GetReplaceNodes(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            var index = nodeIndexToReplace;
            if (--index == 0)
                return (generator.CreateFunction<T>(maxDepth), Children[1].GetCopy());

            if ((index -= Children[0].GetNodeCount()) <= 0)
                return (Children[0].ReplaceNode(index, generator, maxDepth),Children[1].GetCopy());

            if (index-- == 0)
                return (Children[0].GetCopy(),generator.CreateFunction<T>(maxDepth));
            
            if ((index -= Children[1].GetNodeCount()) <= 0)
                return (Children[1].GetCopy(),Children[1].ReplaceNode(index, generator, maxDepth));
            
            throw new Exception("Could not find desired node to replace");
        }

        protected (INode<T>, INode<T>) GetChildrenWithoutNullNodes(int maxDepth, FunctionGenerator generator)
        {
            var newLeftChild = ReplaceNullNodesForComponent(GetChild(0),maxDepth - 1,generator);
            var newRightChild = ReplaceNullNodesForComponent(GetChild(1),maxDepth - 1,generator);

            return (newLeftChild, newRightChild);
        }

        protected IEnumerable<INode<T>> GetChildCopies()
        {
            return new List<INode<T>>() {GetChild(0).GetCopy(), GetChild(1).GetCopy()};
        }
        public override void Visit(INodeVisitor visitor)
        {
            visitor.Accept(this);
        }

    }
}