using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure
{
    public class AverageMeasure : StatisticMeasure
    {
        public override CalculationResultSet GetGenerationStatistics(List<CalculationResultSet> generationResultSets)
        {
            var generationAverages = generationResultSets.Select(GetAverageOfSet);

            return CreateResultSetForGenerations(generationAverages);
        }

        public override double GetRunStatistic(CalculationResultSet generationStats)
        {
            return GetAverageOfSet(generationStats);
        }

        public override string GetHeading()
        {
            return "Avg";
        }

        public double GetAverageOfSet(CalculationResultSet calculationResultSet)
        {
            var total = calculationResultSet.Total();

            return total / calculationResultSet.Size();
        }

        public double Get<T>(GenerationRecord<T> generationRecord)
        {
            var fitnessValues = generationRecord.GetFitnessValues();

            return GetAverageOfSet(fitnessValues);
        }
    }
}