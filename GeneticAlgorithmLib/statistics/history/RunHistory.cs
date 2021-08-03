using System.Collections.Generic;

namespace GeneticAlgorithmLib.statistics
{
    public class RunHistory
    {
        private int _runCount;
        private List<EvaluationResults> _evaluationResults;

        public RunHistory(int runCount)
        {
            _runCount = runCount;
        }

        public void AddGeneration(EvaluationResults evaluationResult)
        {
            _evaluationResults.Add(evaluationResult);
        }
    }
}