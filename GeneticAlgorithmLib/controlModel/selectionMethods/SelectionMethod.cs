using System.Collections.Generic;
using GeneticAlgorithmLib.fitnessFunctions;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel.selectionMethods
{
    public abstract class SelectionMethod : ISelectionMethod
    {
        protected IFitnessFunction FitnessFunction;

        protected SelectionMethod(IFitnessFunction function)
        {
            FitnessFunction = function;
        }
        
        public abstract List<string> Select<T>(EvaluationResults<T> results);
    }
}