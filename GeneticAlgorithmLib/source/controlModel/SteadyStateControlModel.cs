using System;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.operators;

namespace GeneticAlgorithmLib.source.controlModel
{
    public class SteadyStateControlModel<T> : ControlModel<T> where T : IComparable
    {
        public SteadyStateControlModel(IPopulationMutator<T> mutator,IFitnessFunction fitnessFunc) : base(mutator,fitnessFunc)
        {
        }
        
    }
}