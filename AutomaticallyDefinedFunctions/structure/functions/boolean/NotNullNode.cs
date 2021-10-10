using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.state;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.boolean
{
    public class NotNullNode<TU> : FunctionNode<bool> where TU : IComparable
    {
        private INode<TU> _argument;

        public NotNullNode()
        {
        }
        
        public NotNullNode(INode<TU> argument)
        {
            _argument = argument;
        }

        public override string GetId()
        {
            return $"{NodeCategory.NotNull}<{typeof(bool)},{typeof(TU)}>[{_argument.GetId()}]";

        }

        public override int GetNodeCount()
        {
            return _argument.GetNodeCount();
        }

        public override INode<bool> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            if (--nodeIndexToReplace <= 0)
                return SetArgument(generator.CreateFunction<TU>(maxDepth));

            return SetArgument(_argument.ReplaceNode(nodeIndexToReplace, generator, maxDepth));
        }

        public override INode<bool> GetSubTree(int nodeIndexToGet)
        {
            throw new NotImplementedException();
        }

        public override void Visit(INodeVisitor visitor)
        {
            visitor.Accept(this);
            _argument.Visit(visitor);
        }

        public override bool GetValue()
        {
            return _argument.GetValue() is not null;
        }

        public override bool IsValid()
        {
            return _argument is not null;
        }

        public override int GetNullNodeCount()
        {
            return _argument.GetNullNodeCount();
        }

        public override INode<bool> GetCopy()
        {
            return new NotNullNode<TU>(_argument.GetCopy());
        }

        public override INode<bool> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            if (_argument.IsNullNode())
                return SetArgument(generator.CreateFunction<TU>(maxDepth));
            
            return SetArgument(_argument.ReplaceNullNodes(maxDepth, generator));
        }

        public NotNullNode<TU> SetArgument(INode<TU> node)
        {
            _argument = node;
            return this;
        }
    }
}