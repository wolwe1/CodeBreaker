using System.Collections.Generic;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure
{
    public interface IStatisticMeasure
    {
        CalculationResultSet GetGenerationStatistics(List<CalculationResultSet> generationResultSets);
        double GetRunStatistic(CalculationResultSet generationStats);
        string GetHeading();
    }
}