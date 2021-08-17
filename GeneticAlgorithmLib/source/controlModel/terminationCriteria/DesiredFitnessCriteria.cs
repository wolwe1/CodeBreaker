using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.controlModel.terminationCriteria
{
    public class DesiredFitnessCriteria : ITerminationCriteria
    {
        private double _desiredFitness;

        public DesiredFitnessCriteria(double desiredFitness)
        {
            _desiredFitness = desiredFitness;
        }

        public bool Met<T>(int generationCount, GenerationRecord<T> generationRecord)
        {
            var fitnessValues = generationRecord.GetFitnessValues();
            var bestFitness = fitnessValues.Max();

            return bestFitness >= _desiredFitness;

        }
    }
}