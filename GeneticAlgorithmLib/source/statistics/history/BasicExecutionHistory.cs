using GeneticAlgorithmLib.source.statistics.output;
using GeneticAlgorithmLib.source.statistics.output.implementations;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.runtime;

namespace GeneticAlgorithmLib.source.statistics.history
{
    public class BasicExecutionHistory<T> : ExecutionHistory<T>
    {
        public BasicExecutionHistory() : base(new DefaultOutputPrinter())
        {
            UseFitnessMeasure(new AverageMeasure());
            UseFitnessMeasure(new BestPerformerMeasure());
            UseFitnessMeasure(new StandardDeviationMeasure());
            UseStatistic(new RunTimeStatistic(new AverageMeasure()));
        }

        public BasicExecutionHistory<T> OutputToFile()
        {
            OutputPrinter = new FileOutputPrinter();
            return this;
        }

        public BasicExecutionHistory<T> OutputToFile(IOutputPrinter printer)
        {
            OutputPrinter = printer;
            return this;
        }
    }
}