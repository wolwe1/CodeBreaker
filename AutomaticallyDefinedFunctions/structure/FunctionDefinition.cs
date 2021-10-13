using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure
{
    public class FunctionDefinition<T> : FunctionNode<T> where T : IComparable
    {
        private readonly string _name;
        private readonly INode<T> _function;

        public FunctionDefinition(string name,INode<T> function): base(1)
        {
            _name = name;
            _function = function;
            RegisterChildren(new List<INode>(){_function});
        }

        public static FunctionDefinition<T> Create(string name)
        {
            return new FunctionDefinition<T>(name,null);    
        }

        public FunctionDefinition<T> UseFunction(INode<T> function)
        {
            return new FunctionDefinition<T>(_name, function);
        }

        public override string GetId()
        {
            return CreateId<T>(NodeCategory.FunctionDefinition);
        }
        
        public override T GetValue()
        {
            return _function.GetValue();
        }
        
        public override INode<T> GetCopy()
        {
            return new FunctionDefinition<T>(_name, _function.GetCopy());
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            return new FunctionDefinition<T>(_name, _function.ReplaceNullNodes(maxDepth,creator));
        }
        
    }
}