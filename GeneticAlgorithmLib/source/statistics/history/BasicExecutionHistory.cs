using GeneticAlgorithmLib.source.statistics.output.implementations;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.other;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.runtime;

namespace GeneticAlgorithmLib.source.statistics.history
{
    public class BasicExecutionHistory<T> : ExecutionHistory<T>
    {
        public BasicExecutionHistory() : base(new DefaultOutputPrinter())
        {
            UseFitnessMeasure(new AverageMeasure());
            UseFitnessMeasure(new BestPerformerMeasure());
            UseStatistic(new RunTimeStatistic(new AverageMeasure()));
            UseStatistic(new BestMemberOutputPrinter());
        }

        public BasicExecutionHistory<T> OutputToFile()
        {
            OutputPrinter = new FileOutputPrinter();
            return this;
        }
    }
}