using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.calculatedResults;
using GeneticAlgorithmLib.source.statistics.output;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations.other
{
    public class BestMemberOutputPrinter : IRunStatistic
    {
        private readonly BestPerformerMeasure _bestMemberMeasure;

        public BestMemberOutputPrinter()
        {
            _bestMemberMeasure = new BestPerformerMeasure();
        }

        public StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> generationRecords)
        {
            var resultSet = new CalculationResultSet();

            var bestMemberPerGeneration = generationRecords
                .Select(g => g.GetMemberWithMaxFitness());

            var bestFitnessValue = bestMemberPerGeneration.Max(m => m.GetFitness());

            var bestMemberInRun = bestMemberPerGeneration.FirstOrDefault(x => x.GetFitness() == bestFitnessValue);

            return new StatisticOutput()
                .SetHeading("Best member ID")
                .SetRunValue(bestMemberInRun.GetMemberId());
        }
    }
}