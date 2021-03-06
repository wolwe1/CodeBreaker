using System;
using AutomaticallyDefinedFunctions.generators.adf;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.ga.state
{
    public class StateAdfPopulationGenerator<TAdfOutput,TProgResponse> : AdfPopulationGenerator<TAdfOutput> where TAdfOutput : IComparable where TProgResponse : IComparable
    {
        public StateAdfPopulationGenerator(int seed, StateAdfSettings<TAdfOutput, TProgResponse> settings) : base(seed,
            settings)
        {
            Generator = new StateAdfGenerator<TAdfOutput, TProgResponse>(seed, settings);
        }
        
        public override IPopulationMember<TAdfOutput> GenerateNewMember()
        {
            var newAdf = Generator.Generate();
            return new StateAdfPopulationMember<TAdfOutput,TProgResponse>(newAdf);
        }

        public override IPopulationMember<TAdfOutput> GenerateFromId(string chromosome)
        {
            var newAdf = Generator.GenerateFromId(chromosome);

            return new StateAdfPopulationMember<TAdfOutput,TProgResponse>(newAdf);
        }
    }
}