using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.history;

namespace CodeBreakerLib.testHandler
{
    public class TestHistory
    {
        private ITypelessExecutionHistory _executionHistory;
        private readonly Test<object> _test;

        public TestHistory(ITypelessExecutionHistory executionHistory,Test<object> test)
        {
            _executionHistory = executionHistory;
            _test = test;
        }

        public string GetTestName()
        {
            return _test.GetName();
        }

        public List<string> GetBestAdfs()
        {
            return _executionHistory.GetBestPerformerIds();
        }

        public Type GetInputType()
        {
            return _test.GetArguments().FirstOrDefault();
        }

        public Type GetResponseType()
        {
            return _test.GetUnderlyingReturnType();
        }

        public int GetNumberOfInputs()
        {
            return _test.GetArguments().Count;
        }

        public int GetRunNumber()
        {
            return _executionHistory.RunNumber;
        }
    }
}