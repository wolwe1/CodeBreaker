using GeneticAlgorithmLib.source.statistics.history;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.other;

namespace CodeBreakerLib.connectors.output
{
    public class ExecutionOutput<T> : BasicExecutionHistory<T>
    {
        public ExecutionOutput() : base()
        {
            UseStatistic(new BestAdfOutputPrinter());
            UseStatistic(new BestMemberIdPrinter());
        }

    }
}