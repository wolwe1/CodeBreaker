using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel.terminationCriteria
{
    public class DesiredFitnessCriteria : ITerminationCriteria
    {
        private double _desiredFitness;

        public DesiredFitnessCriteria(double desiredFitness)
        {
            _desiredFitness = desiredFitness;
        }

        public bool Met(int generationCount, EvaluationResults evaluationResults)
        {
            var fitnessValues = evaluationResults.GetFitnessValues();
            var bestFitness = fitnessValues.Max();

            return bestFitness >= _desiredFitness;

        }
    }
}