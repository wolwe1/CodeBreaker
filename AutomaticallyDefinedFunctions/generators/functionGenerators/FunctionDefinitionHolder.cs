using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.generators.functionGenerators
{
    /// <summary>
    /// Pretends to be a function generator, but instead dispatches already generated definitions
    /// </summary>
    /// <typeparam name="T">The return type of the definition</typeparam>
    public class FunctionDefinitionHolder<T> : FunctionGenerator<T> where T : IComparable
    {
        private List<FunctionDefinition<T>> _definitions;
        
        public FunctionDefinitionHolder(Random generator) : base(generator, 0)
        {
            _definitions = new List<FunctionDefinition<T>>();
        }

        public override FunctionNode<T> Generate()
        {
            var definitionToChoose = NumberGenerator.Next(_definitions.Count);

            return _definitions.ElementAt(definitionToChoose);
        }

        public FunctionDefinitionHolder<T> AddDefinition(FunctionDefinition<T> definition)
        {
            _definitions.Add(definition);

            return this;
        }
    }
}