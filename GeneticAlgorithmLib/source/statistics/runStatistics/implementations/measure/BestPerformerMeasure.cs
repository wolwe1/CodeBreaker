using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure
{
    public class BestPerformerMeasure : StatisticMeasure
    {
        public override CalculationResultSet GetGenerationStatistics(List<CalculationResultSet> generationResultSets)
        {
            var generationBests = generationResultSets.Select(GetBestPerformer);

            return new CalculationResultSet(generationBests);
        }

        public override double GetRunStatistic(CalculationResultSet generationStats)
        {
            return generationStats.Max();
        }

        public override string GetHeading()
        {
            return "Best";
        }

        public CalculationResult GetBestPerformer(CalculationResultSet set)
        {
            var best = set.Max();

            return set.ToList().FirstOrDefault(p => p.GetResult() == best);
        }
    }
}