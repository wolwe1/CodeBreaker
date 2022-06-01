using System.Collections.Generic;
using GeneticAlgorithmLib.source.statistics.history;

namespace GeneticAlgorithmLib.source.statistics.output
{
    public interface IOutputPrinter
    {
        void Print<T>(List<StatisticOutput> runStatistics, RunRecord<T> runRecord);
    }
}