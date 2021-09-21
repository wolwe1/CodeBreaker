using GeneticAlgorithmLib.source.statistics.runStatistics.implementations;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.other;

namespace GeneticAlgorithmLib.source.statistics.history
{
    public class BasicExecutionHistory<T> : ExecutionHistory<T>
    {
        public BasicExecutionHistory()
        {
            UseFitnessMeasure(new AverageMeasure());
            UseFitnessMeasure(new BestPerformerMeasure());
            UseStatistic(new RunTimeStatistic(new AverageMeasure()));
            UseStatistic(new BestMemberOutputPrinter());
        }
    }
}