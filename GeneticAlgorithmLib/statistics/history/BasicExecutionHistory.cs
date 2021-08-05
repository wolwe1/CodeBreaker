using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmLib.statistics
{
    public class BasicExecutionHistory<T> : IExecutionHistory<T>
    {
        private List<RunHistory<T>> _runHistory;
        private int _currentRunCount;

        public BasicExecutionHistory()
        {
            _currentRunCount = 0;
            _runHistory = new List<RunHistory<T>>();
        }

        public void NewRun()
        {
            var newRun = new RunHistory<T>(_currentRunCount++);
            
            _runHistory.Add(newRun);
            
        }

        public void AddGenerationHistory(EvaluationResults<T> evaluationResults)
        {
            var targetRun = _runHistory.ElementAt(_currentRunCount - 1);

            targetRun.AddGeneration(evaluationResults);
        }
    }
}