using GeneticAlgorithmLib.source.statistics.history;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace CodeBreakerLib.connectors.output
{
    public class ExecutionOutput<T> : BasicExecutionHistory<T>
    {
        public ExecutionOutput() : base()
        {
            UseStatistic(new BestAdfOutputPrinter());
        }

    }
}