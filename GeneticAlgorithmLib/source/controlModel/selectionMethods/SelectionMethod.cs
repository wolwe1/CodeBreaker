using System.Collections.Generic;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.controlModel.selectionMethods
{
    public abstract class SelectionMethod : ISelectionMethod
    {
        protected readonly IFitnessFunction FitnessFunction;

        protected SelectionMethod(IFitnessFunction function)
        {
            FitnessFunction = function;
        }

        public abstract List<string> Select<T>(GenerationRecord<T> results,int maxParentsToProduce);
    }
}