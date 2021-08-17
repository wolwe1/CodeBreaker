using System.Collections.Generic;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations
{
    public class FitnessStatistic : RunStatistic
    {
        private readonly IStatisticMeasure _measure;

        public FitnessStatistic(IStatisticMeasure measure) : base($"{measure.GetHeading()} fitness")
        {
            _measure = measure;
        }
        
        public override string GetStatistic<T>(List<GenerationRecord<T>> generationResults)
        {
            var generationResultSets = CreateGenerationResultSets(generationResults);

            var generationStats = _measure.GetGenerationStatistics(generationResultSets);
            var runStatistic = _measure.GetRunStatistic(generationStats);

            return GetOutput(generationStats, runStatistic);
        }
    }
}