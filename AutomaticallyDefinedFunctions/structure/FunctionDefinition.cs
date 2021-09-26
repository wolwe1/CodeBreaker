using System;
using System.Collections.Immutable;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.structure
{
    public class FunctionDefinition<T> : FunctionNode<T> where T : IComparable
    {
        private readonly string _name;
        private readonly INode<T> _function;
        private readonly ImmutableList<ValueNode<T>> _arguments;

        private FunctionDefinition(string name,INode<T> function,ImmutableList<ValueNode<T>> arguments)
        {
            _name = name;
            _arguments = arguments;
            _function = function;
        }

        public static FunctionDefinition<T> Create(string name)
        {
            return new FunctionDefinition<T>(name,null,ImmutableList<ValueNode<T>>.Empty);    
        }

        public FunctionDefinition<T> UseFunction(INode<T> function)
        {
            return new FunctionDefinition<T>(_name, function, _arguments);
        }

        public override string GetId()
        {
            return _function.GetId();
        }

        public override int GetNodeCount()
        {
            return _function.GetNodeCount();
        }

        public override INode<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            return _function.ReplaceNode(nodeIndexToReplace,generator,maxDepth);
        }

        public override INode<T> GetSubTree(int nodeIndexToGet)
        {
            return _function.GetSubTree(nodeIndexToGet);
        }

        public override T GetValue()
        {
            return _function.GetValue();
        }

        public override bool IsValid()
        {
            return _function != null && _function.IsValid();
        }

        public override int GetNullNodeCount()
        {
            return _function.GetNullNodeCount();
        }

        public override INode<T> GetCopy()
        {
            return new FunctionDefinition<T>(_name, _function.GetCopy(), _arguments);
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            return new FunctionDefinition<T>(_name, _function.ReplaceNullNodes(maxDepth,generator), _arguments);
        }
        
    }
}