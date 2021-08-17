using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.controlModel.terminationCriteria
{
    public interface ITerminationCriteria
    {
        bool Met<T>(int generationCount, GenerationRecord<T> generationRecord);
    }
}