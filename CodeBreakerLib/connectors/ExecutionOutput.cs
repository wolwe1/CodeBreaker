using GeneticAlgorithmLib.source.statistics.history;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.other;

namespace CodeBreakerLib.connectors
{
    public class ExecutionOutput<T> : BasicExecutionHistory<T>
    {
        public ExecutionOutput() : base()
        {
            UseStatistic(new BestMemberOutputPrinter());
        }

    }
}