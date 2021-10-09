using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.operators.implementation
{
    public class MutationOperator<T> : Operator<T> where T : IComparable
    {
        private readonly int _maxModificationDepth;

        public MutationOperator(int applicationPercentage, int maxModificationDepth) : base(applicationPercentage)
        {
            _maxModificationDepth = maxModificationDepth;
        }

        protected override IEnumerable<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
        {
            var adf = GetAdfFromParents(parents,generator);

            var mainToMutate = RandomNumberFactory.Next(adf.GetNumberOfMainPrograms());
            
            var numberOfNodes = adf.GetMainNodeCount(mainToMutate);
            //Prevent root node replacement
            var nodeToReplace = RandomNumberFactory.Next(numberOfNodes - 1) + 1;

            var populationGenerator = (AdfPopulationGenerator<T>) generator;
            
            adf.ReplaceNodeInMain(mainToMutate,nodeToReplace,populationGenerator.GetFunctionGenerator(),_maxModificationDepth);

            return new List<string>(){adf.GetId()};
        }
    }
}