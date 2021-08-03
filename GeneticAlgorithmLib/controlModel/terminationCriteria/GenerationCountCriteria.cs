using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel.terminationCriteria
{
    public class GenerationCountCriteria : ITerminationCriteria
    {
        private readonly int _maxGenerations;

        public GenerationCountCriteria(int maxGenerations)
        {
            _maxGenerations = maxGenerations;
        }

        public bool Met(int generationCount, EvaluationResults evaluationResults)
        {
            return generationCount >= _maxGenerations;
        }
    }
}