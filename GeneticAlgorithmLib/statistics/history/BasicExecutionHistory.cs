using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmLib.statistics
{
    public class BasicExecutionHistory : IExecutionHistory
    {
        private List<RunHistory> _runHistory;
        private int _currentRunCount;

        public BasicExecutionHistory()
        {
            _currentRunCount = 0;
            _runHistory = new List<RunHistory>();
        }

        public void NewRun()
        {
            var newRun = new RunHistory(_currentRunCount++);
            
            _runHistory.Add(newRun);
            
        }

        public void AddGenerationHistory(EvaluationResults evaluationResults)
        {
            var targetRun = _runHistory.ElementAt(_currentRunCount - 1);

            targetRun.AddGeneration(evaluationResults);
        }
    }
}