using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel.terminationCriteria
{
    public interface ITerminationCriteria
    {
        bool Met(int generationCount, EvaluationResults evaluationResults);
    }
}