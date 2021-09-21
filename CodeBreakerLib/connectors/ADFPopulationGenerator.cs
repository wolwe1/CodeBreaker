using System;
using AutomaticallyDefinedFunctions.generators;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors
{
    public class ADFPopulationGenerator<T> : IPopulationGenerator<T> where T : IComparable
    {
        private readonly ADFGenerator<T> _generator;

        public ADFPopulationGenerator(int seed, int terminalChance, int maxFunctionDepth, int maxMainDepth )
        {
            _generator = new ADFGenerator<T>(seed,terminalChance,maxFunctionDepth,maxMainDepth);
        }

        public IPopulationMember<T> GenerateNewMember()
        {
            var newAdf = _generator.Generate();
            return new ADFPopulationMember<T>(newAdf);
        }

        public IPopulationMember<T> GenerateFromId(string chromosome)
        {
            var newAdf = _generator.GenerateFromId(chromosome);

            return new ADFPopulationMember<T>(newAdf);
        }
    }
}