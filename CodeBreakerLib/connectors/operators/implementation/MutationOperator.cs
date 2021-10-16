using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.structure;
using CodeBreakerLib.connectors.ga;
using CodeBreakerLib.visitors;
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

            var mutator = CreateMutator(adf,mainToMutate,generator);

            adf.VisitMain(mainToMutate, mutator);

            return new List<string>(){adf.GetId()};
        }

        private NodeMutatorVisitor<T> CreateMutator(Adf<T> adf,int mainToMutate, IPopulationGenerator<T> generator)
        {
            var numberOfNodes = adf.GetMainNodeCount(mainToMutate);
            //Prevent root node replacement
            var nodeToReplace = RandomNumberFactory.Next(numberOfNodes - 1) + 1;
            
            return new NodeMutatorVisitor<T>(nodeToReplace, generator,_maxModificationDepth);
        }
    }
}