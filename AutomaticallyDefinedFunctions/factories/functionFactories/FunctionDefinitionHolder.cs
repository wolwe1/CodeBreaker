using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public class FunctionDefinitionHolder<T> : FunctionFactory where T : IComparable
    {
        private readonly List<FunctionDefinition<T>> _functionDefinitions;

        public FunctionDefinitionHolder() : base("Not known")
        {
            _functionDefinitions = new List<FunctionDefinition<T>>();
        }

        public override FunctionNode<TX> CreateFunction<TX>(int maxDepth, FunctionCreator parent)
        {
            //Type safety check
            if (typeof(TX) != typeof(T))
                return null;
            
            var choice = RandomNumberFactory.Next(_functionDefinitions.Count);

            var definition = _functionDefinitions.ElementAt(choice);
            
            return (FunctionNode<TX>) definition.ReplaceNullNodes(maxDepth,parent);
  
        }

        public void AddDefinition(FunctionDefinition<T> function)
        {
            _functionDefinitions.Add(function);
        }

        public void Clear()
        {
            _functionDefinitions.Clear();
        }
        
        public override bool CanDispatch<TX>()
        {
            return typeof(TX) == typeof(T);
        }

        protected override INode<T1> GenerateFunctionFromId<T1, TU>(string id, FunctionCreator functionCreator)
        {
            var nodeTree = functionCreator.GenerateChildFromId<T1>(ref id);
            return new FunctionDefinition<T1>("Unknown",nodeTree);
        }
    }
}