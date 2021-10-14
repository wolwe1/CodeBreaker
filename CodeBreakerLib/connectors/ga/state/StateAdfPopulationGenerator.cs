using System;
using AutomaticallyDefinedFunctions.generators.adf;

namespace CodeBreakerLib.connectors.ga.state
{
    public class StateAdfPopulationGenerator<TAdfOutput,TProgResponse> : AdfPopulationGenerator<TAdfOutput> where TAdfOutput : IComparable where TProgResponse : IComparable
    {
        public StateAdfPopulationGenerator(int seed, StateAdfSettings<TAdfOutput, TProgResponse> settings) : base(seed,
            settings)
        {
            Generator = new StateAdfGenerator<TAdfOutput, TProgResponse>(seed, settings);
        }
    }
}