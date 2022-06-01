using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure
{
    public class StandardDeviationMeasure  : StatisticMeasure
    {
        private AverageMeasure _averageMeasure;

        public StandardDeviationMeasure()
        {
            _averageMeasure = new AverageMeasure();
        }

        public override CalculationResultSet GetGenerationStatistics(List<CalculationResultSet> generationResultSets)
        {
            var generationAverages = generationResultSets.Select(GetStandardDeviationOfSet);

            return CreateResultSetForGenerations(generationAverages);
        }

        public override double GetRunStatistic(CalculationResultSet generationStats)
        {
            return GetStandardDeviationOfSet(generationStats);
        }

        public override string GetHeading()
        {
            return "Std dev";
        }

        private double GetStandardDeviationOfSet(CalculationResultSet calculationResultSet)
        {
            var numberOfMembers = calculationResultSet.Size();
            var avg = _averageMeasure.GetAverageOfSet(calculationResultSet);

            var squaredDeviations = calculationResultSet.Map(result => Math.Pow((result - avg), 2));
            var numerator = squaredDeviations.Accumulate((x, y) => x + y);

            return Math.Sqrt(numerator / numberOfMembers);

        }

        public double Get<T>(GenerationRecord<T> generationRecord)
        {
            var fitnessValues = generationRecord.GetFitnessValues();

            return GetStandardDeviationOfSet(fitnessValues);
        }

    }
}