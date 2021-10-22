using System;
using System.Collections.Generic;
using System.Linq;
using CodeBreakerLib.settings;
using CodeBreakerLib.testHandler.integration;
using CodeBreakerLib.testHandler.setup;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.history;

namespace CodeBreakerLib.testHandler
{
    public class TestHandler
    {
        private readonly ITestStrategy _testStrategy;
        
        private List<Test<object>> _tests;
        private int _currentTest = 0;
        private readonly GeneticAlgorithmBuilder _builder;

        public TestHandler(ITestStrategy testStrategy,GeneticAlgorithmBuilder builder)
        {
            _testStrategy = testStrategy;
            _tests = new List<Test<object>>();
            _builder = builder;
        }

        public void Setup()
        {
            _tests = _testStrategy.Setup();
        }
        
        public List<TestHistory> RunAllTestsMultiple()
        {
            List<TestHistory> testHistories = new List<TestHistory>();
            
            for (int i = 0; i < GlobalSettings.NumberOfRuns; i++)
            {
                testHistories.AddRange(RunAllTests(i));
            }

            return testHistories;
        }
        
        public List<TestHistory> RunAllTests(int seed = 0)
        {
            var testHistories = new List<TestHistory>();
            for (var i = 0; i < _tests.Count; i++)
            {
                var testHistory = RunGaAgainstNextTest(seed);
                testHistories.Add(testHistory);
            }
            return testHistories;
        }

        public List<Test<object>> GetTests()
        {
            return _tests;
        }
        
        private Test<object> GetNextTest()
        {
            return _currentTest >= _tests.Count ? null : _tests.ElementAt(_currentTest++);
        }

        private TestHistory RunGaAgainstNextTest(int seed)
        {
            var test = GetNextTest();
            
            if(test == null)
                return null;

            Console.WriteLine($"Running GA against Test: {test?.GetName()}");
            var inputType = test?.GetArguments()?.ElementAt(0);

            return DispatchGaForTest(inputType, test,seed);
            
        }

        private TestHistory DispatchGaForTest(Type argumentsType, Test<object> test, int seed)
        {
            if (argumentsType == typeof(string))
                return RunGaAgainstTest<string>(test,seed);
            
            if (argumentsType == typeof(double) || argumentsType == typeof(int))
                return RunGaAgainstTest<double>(test,seed);
            
            if (argumentsType == typeof(bool))
                return RunGaAgainstTest<bool>(test,seed);
            
            throw new Exception($"Could not dispatch GA for test of type {argumentsType?.FullName}");
        }
        
        private TestHistory RunGaAgainstTest<T>(Test<object> test, int seed) where T : IComparable
        {
            var geneticAlgorithm = _builder.Build<T>(test,seed);
            var history = geneticAlgorithm.Run();
            ((ExecutionHistory<T>) history).AdditionalExecutionInfo = test.GetName();
            history.Summarise();
            history.RunNumber = seed;
            return new TestHistory(history,test);
        }
    }

}