using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.runStatistics;
using GeneticAlgorithmLib.test.statisticsTests;

namespace GeneticAlgorithmLib.source.statistics.history
{
    public class RunRecord<T>
    {
        private readonly int _runCount;
        private readonly List<GenerationRecord<T>> _generationResults;

        public RunRecord(int runCount)
        {
            _runCount = runCount;
            _generationResults = new List<GenerationRecord<T>>();
        }

        public void AddGeneration(GenerationRecord<T> generationRecord)
        {
            _generationResults.Add(generationRecord);
        }

        public GenerationRecord<T> GetGeneration(int index)
        {
            if (index >= _generationResults.Count || index < 0)
                throw new IndexOutOfRangeException($"Cannot get history of generation {index} with max of {_generationResults.Count} in run {_runCount}");
            
            return _generationResults.ElementAt(index);
        }

        public void Summarise(List<IRunStatistic> runStatistics)
        {
            Console.WriteLine("****************");
            Console.WriteLine($"Run {_runCount}");

            foreach (var statistic in runStatistics)
            {
                var output = statistic.GetStatistic(_generationResults);
                Console.Write(output);
            }
            
            Console.WriteLine("****************");
        }
    }
}