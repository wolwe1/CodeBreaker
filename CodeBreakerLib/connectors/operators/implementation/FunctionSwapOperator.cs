using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.operators.implementation
{
    public class FunctionSwapOperator<T> : Operator<T> where T : IComparable
    {
        public FunctionSwapOperator(int applicationPercentage) : base(applicationPercentage)
        {
        }

        protected override IEnumerable<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
        {
            var firstAdf = GetAdfFromParents(parents,generator);
            var secondAdf = GetAdfFromParents(parents,generator);

            var firstDefinitionIndex = RandomNumberFactory.Next(firstAdf.GetNumberOfDefinitions());
            var secondDefinitionToSwap = RandomNumberFactory.Next(secondAdf.GetNumberOfDefinitions());

            var firstFunction = firstAdf.GetFunctionDefinition(firstDefinitionIndex);
            var secondFunction = secondAdf.GetFunctionDefinition(secondDefinitionToSwap);

            firstAdf = firstAdf.SetFunctionDefinition(firstDefinitionIndex, secondFunction);
            secondAdf = secondAdf.SetFunctionDefinition(secondDefinitionToSwap, firstFunction);
            
            return new List<string>(){firstAdf.GetId(),secondAdf.GetId()};
        }
    }
}