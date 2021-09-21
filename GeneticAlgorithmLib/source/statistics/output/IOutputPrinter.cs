using System.Collections.Generic;

namespace GeneticAlgorithmLib.source.statistics.output
{
    public interface IOutputPrinter
    {
        void Print(List<StatisticOutput> runStatistics);
    }
}