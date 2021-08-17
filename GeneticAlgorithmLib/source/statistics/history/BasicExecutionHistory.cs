using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.runStatistics;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;
using GeneticAlgorithmLib.test.statisticsTests;

namespace GeneticAlgorithmLib.source.statistics.history
{
    public class BasicExecutionHistory<T> : ExecutionHistory<T>
    {
        public BasicExecutionHistory()
        {
            UseFitnessMeasure(new AverageMeasure());
            UseStatistic(new RunTimeStatistic(new AverageMeasure()));
        }
    }
}