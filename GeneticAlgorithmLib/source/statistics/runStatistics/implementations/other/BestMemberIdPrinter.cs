using System.Collections.Generic;
using GeneticAlgorithmLib.source.statistics.output;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations.other
{
    public class BestMemberIdPrinter : IRunStatistic
    {
        private readonly BestPerformerMeasure _bestMemberMeasure;

        public BestMemberIdPrinter()
        {
            _bestMemberMeasure = new BestPerformerMeasure();
        }

        public StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> generationRecords)
        {
            var bestMemberInRun = _bestMemberMeasure.GetBestPerformer(generationRecords);
          
            return new StatisticOutput()
                .SetHeading("Best member ID")
                .SetRunValue(bestMemberInRun.GetMemberId());
        }
    }
}