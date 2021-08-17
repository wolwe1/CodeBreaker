using System.Collections.Generic;

namespace GeneticAlgorithmLib.source.statistics.runStatistics
{
    public interface IRunStatistic
    {
        string GetStatistic<T>(List<GenerationRecord<T>> evaluationResults);
    }
}