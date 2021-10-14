using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.generators.adf;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.ga
{
    public class AdfPopulationGenerator<T> : IPopulationGenerator<T> where T : IComparable
    {
        protected AdfGenerator<T> Generator;

        public AdfPopulationGenerator(int seed, AdfSettings settings)
        {
            Generator = new AdfGenerator<T>(seed,settings);
        }
        
        protected AdfPopulationGenerator() { }

        public IPopulationMember<T> GenerateNewMember()
        {
            var newAdf = Generator.Generate();
            return new AdfPopulationMember<T>(newAdf);
        }

        public IPopulationMember<T> GenerateFromId(string chromosome)
        {
            var newAdf = Generator.GenerateFromId(chromosome);

            return new AdfPopulationMember<T>(newAdf);
        }
        
        public AdfGenerator<T> GetGenerator()
        {
            return Generator;
        }

        public FunctionCreator GetFunctionCreator()
        {
            return Generator.GetFunctionCreator();
        }
    }
}