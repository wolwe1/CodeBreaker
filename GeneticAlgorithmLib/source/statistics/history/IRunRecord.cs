using System.Collections.Generic;
using GeneticAlgorithmLib.source.statistics.output;
using GeneticAlgorithmLib.source.statistics.runStatistics;

namespace GeneticAlgorithmLib.source.statistics.history
{
    public interface IRunRecord
    {
        public List<StatisticOutput> Summarise(List<IRunStatistic> runStatistics);

        public int GetRunNumber();

        public List<IGenerationRecord> GetGenerationRecords();
    }
}