using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.operators;
using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.controlModel
{
    public class SteadyStateControlModel<T> : ControlModel<T> where T : IComparable
    {
        public SteadyStateControlModel(IPopulationMutator<T> mutator,IFitnessFunction fitnessFunc) : base(mutator,fitnessFunc)
        {
        }

        public override List<string> SelectParents(GenerationRecord<T> results)
        {
            var populationSizeToMaintain = results.Size();
            return SelectionMethod.Select(results, populationSizeToMaintain);
        }
        
    }
}