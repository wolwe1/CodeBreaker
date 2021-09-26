using System;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors
{
    public class AdfPopulationGenerator<T> : IPopulationGenerator<T> where T : IComparable
    {
        private readonly AdfGenerator<T> _generator;

        public AdfPopulationGenerator(int seed, AdfSettings settings)
        {
            _generator = new AdfGenerator<T>(seed,settings);
        }

        public IPopulationMember<T> GenerateNewMember()
        {
            var newAdf = _generator.Generate();
            return new AdfPopulationMember<T>(newAdf);
        }

        public IPopulationMember<T> GenerateFromId(string chromosome)
        {
            var newAdf = _generator.GenerateFromId(chromosome);

            return new AdfPopulationMember<T>(newAdf);
        }

        public INode<T> GenerateSubTree(int maxDepth)
        {
            return _generator.GenerateSubTree(maxDepth);
        }

        public AdfGenerator<T> GetGenerator()
        {
            return _generator;
        }

        public FunctionGenerator GetFunctionGenerator()
        {
            return _generator.GetFunctionGenerator();
        }
    }
}