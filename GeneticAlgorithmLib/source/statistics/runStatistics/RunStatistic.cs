using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.statistics.runStatistics
{
    public abstract class RunStatistic : IRunStatistic
    {
        protected readonly string Heading;
        protected readonly bool IncludeGenerationOutput;
        protected string Scale;

        protected RunStatistic(string heading)
        {
            Heading = heading;
            IncludeGenerationOutput = true;
            Scale = "";
        }
        public abstract string GetStatistic<T>(List<GenerationRecord<T>> generationResults);

        protected static List<CalculationResultSet> CreateGenerationResultSets<T>(List<GenerationRecord<T>> generationResults)
        {
            var convertedGenerationResults = new List<CalculationResultSet>();

            foreach (var result in generationResults)
            {
                var set = result.GetFitnessValues();
                convertedGenerationResults.Add(set);
            }

            return convertedGenerationResults;
        }
        
        protected string GetGenerationOutput(CalculationResultSet generationStats)
        {
            var builder = new StringBuilder();
            var generationValues = generationStats.ToList();

            for (var generation = 0; generation < generationValues.Count(); generation++)
            {
                var value = generationValues[generation].GetResult();
                builder.AppendLine($"\tGeneration {generation} - {Heading}: {value} {Scale}");
            }
            
            return builder.ToString();
        }

        protected string GetOutput(CalculationResultSet generationStats, double runStatistic)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{Heading} : \t{runStatistic} {Scale}");
            
            if (IncludeGenerationOutput)
            {
                builder.AppendLine(GetGenerationOutput(generationStats));
            }

            return builder.ToString();
        }
    }
}