using System.Collections.Generic;

namespace GeneticAlgorithmLib.statistics
{
    public class RunHistory<T>
    {
        private int _runCount;
        private List<EvaluationResults<T>> _evaluationResults;

        public RunHistory(int runCount)
        {
            _runCount = runCount;
        }

        public void AddGeneration(EvaluationResults<T> evaluationResult)
        {
            _evaluationResults.Add(evaluationResult);
        }
    }
}