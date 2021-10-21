using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.history;
using GeneticAlgorithmLib.source.statistics.output;
using GeneticAlgorithmLib.source.statistics.runStatistics;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace GeneticAlgorithmLib.source.statistics
{
    public abstract class ExecutionHistory<T> : IExecutionHistory<T>
    {
        private readonly Stopwatch _generationStopwatch;
        private readonly List<RunRecord<T>> _runHistory;
        private readonly List<IRunStatistic> _runStatistics;
        private int _currentRunCount;
        protected IOutputPrinter OutputPrinter;

        public string AdditionalExecutionInfo;
        
        protected ExecutionHistory(IOutputPrinter printer)
        {
            _runStatistics = new List<IRunStatistic>();
            _runHistory = new List<RunRecord<T>>();
            _currentRunCount = -1;

            _generationStopwatch = new Stopwatch();
            OutputPrinter = printer;
            AdditionalExecutionInfo = "";
        }

        public void NewRun()
        {
            var newRun = new RunRecord<T>(++_currentRunCount);

            _runHistory.Add(newRun);
        }

        public void NewGeneration()
        {
            _generationStopwatch.Start();
        }

        public void CloseGeneration(GenerationRecord<T> generationRecord)
        {
            generationRecord.RunTime = StopGenerationTimer();

            var targetRun = _runHistory.ElementAt(_currentRunCount);

            targetRun.AddGeneration(generationRecord);
        }

        public void Summarise()
        {
            foreach (var run in _runHistory)
            {
                var runOutput = run.Summarise(_runStatistics);
                Console.WriteLine("*****************");
                Console.WriteLine($"Run {run.GetRunNumber()}");
                run.AdditionalRunInfo = AdditionalExecutionInfo;
                OutputPrinter.Print(runOutput,run);
                Console.WriteLine("*****************");
            }
        }

        public List<string> GetBestPerformerIds()
        {
            var members = _runHistory
                .SelectMany(run => run.GetGenerationRecords())
                .SelectMany(generation => generation.GetMembers());

            var orderedMembers = members
                .OrderByDescending(m => m.GetFitnessValue()).Take(10);

            return orderedMembers.Select(m => m.GetMemberId()).ToList();
        }

        public ExecutionHistory<T> UseStatistic(IRunStatistic statistic)
        {
            _runStatistics.Add(statistic);
            return this;
        }

        public ExecutionHistory<T> UseFitnessMeasure(IStatisticMeasure measure)
        {
            _runStatistics.Add(new FitnessStatistic(measure));
            return this;
        }

        private TimeSpan StopGenerationTimer()
        {
            _generationStopwatch.Stop();
            var elapsed = _generationStopwatch.Elapsed;
            _generationStopwatch.Reset();

            return elapsed;
        }

        public List<IRunRecord> GetRunRecords()
        {
            return _runHistory.Cast<IRunRecord>().ToList();
        }
    }
}