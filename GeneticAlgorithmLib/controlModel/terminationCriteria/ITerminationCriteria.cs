using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel.terminationCriteria
{
    public interface ITerminationCriteria
    {
        bool Met<T>(int generationCount, EvaluationResults<T> evaluationResults);
    }
}