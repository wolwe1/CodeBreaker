using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.source
{
    public class FunctionDefinition<T> : FunctionNode<T> where T : IComparable
    {
        private readonly string _name;
        private readonly INode<T> _function;
        private readonly ImmutableList<ValueNode<T>> _arguments;
        private readonly int _numberOfArguments;

        public FunctionDefinition(string name,INode<T> function,ImmutableList<ValueNode<T>> arguments)
        {
            _name = name;
            _arguments = arguments;
            _function = function;
            _numberOfArguments = function?.GetNullNodeCount() ?? 0;
        }

        public static FunctionDefinition<T> Create(string name)
        {
            return new FunctionDefinition<T>(name,null,ImmutableList<ValueNode<T>>.Empty);    
        }

        public FunctionDefinition<T> AddArgument()
        {
            var newArgumentSet = _arguments.Add(new NullNode<T>());
            return new FunctionDefinition<T>(_name, _function, newArgumentSet);
        }
        
        public FunctionDefinition<T> UseFunction(INode<T> function)
        {
            return new FunctionDefinition<T>(_name, function, _arguments);
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

        public int GetArity()
        {
            return _numberOfArguments;
        }
    }
}