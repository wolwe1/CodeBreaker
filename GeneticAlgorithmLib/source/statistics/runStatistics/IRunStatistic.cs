using System.Collections.Generic;
using GeneticAlgorithmLib.source.statistics.output;

namespace GeneticAlgorithmLib.source.statistics.runStatistics
{
    public interface IRunStatistic
    {
        StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> evaluationResults);
    }
}