using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.history;
using GeneticAlgorithmLib.source.statistics.runStatistics;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace GeneticAlgorithmLib.source.statistics
{
    public abstract class ExecutionHistory<T> : IExecutionHistory<T>
    {
        protected readonly List<RunRecord<T>> RunHistory;
        protected int CurrentRunCount;
        protected readonly List<IRunStatistic> RunStatistics;
        
        private Stopwatch _generationStopwatch;

        protected ExecutionHistory()
        {
            RunStatistics = new List<IRunStatistic>();
            RunHistory = new List<RunRecord<T>>();
            CurrentRunCount = -1;
            
            _generationStopwatch = new Stopwatch();
        }

        public void NewRun()
        {
            var newRun = new RunRecord<T>(++CurrentRunCount);
            
            RunHistory.Add(newRun);
            
        }

        public void NewGeneration()
        {
            _generationStopwatch.Start();
        }

        public void CloseGeneration(GenerationRecord<T> generationRecord)
        {
            generationRecord.RunTime = StopGenerationTimer();
            
            var targetRun = RunHistory.ElementAt(CurrentRunCount);

            targetRun.AddGeneration(generationRecord);
        }

        public void Summarise()
        {
            foreach (var run in RunHistory)
            {
                run.Summarise(RunStatistics);
            }
        }

        public ExecutionHistory<T> UseStatistic(IRunStatistic statistic)
        {
            RunStatistics.Add(statistic);
            return this;
        }
        
        public ExecutionHistory<T> UseFitnessMeasure(IStatisticMeasure measure)
        {
            RunStatistics.Add(new FitnessStatistic(measure));
            return this;
        }

        private TimeSpan StopGenerationTimer()
        {
            _generationStopwatch.Stop();
            var elapsed = _generationStopwatch.Elapsed;
            _generationStopwatch.Reset();

            return elapsed;
        }
    }
}