using System;
using System.Collections.Generic;
using System.Linq;
using CodeBreakerLib.testHandler.integration;
using CodeBreakerLib.testHandler.setup;
using GeneticAlgorithmLib.source.statistics;

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

        public void RunAllTests()
        {
            for (var i = 0; i < _tests.Count; i++)
            {
                RunGaAgainstNextTest();
            }
            
        }

        public List<Test<object>> GetTests()
        {
            return _tests;
        }
        
        private Test<object> GetNextTest()
        {
            return _currentTest >= _tests.Count ? null : _tests.ElementAt(_currentTest++);
        }

        private void RunGaAgainstNextTest()
        {
            var test = GetNextTest();
            
            if(test == null)
                return;

            Console.WriteLine($"Running GA against Test: {test?.GetName()}");
            var inputType = test?.GetArguments()?.ElementAt(0);

            DispatchGaForTest(inputType, test);
            
        }

        private void DispatchGaForTest(Type argumentsType, Test<object> test)
        {
            if (argumentsType == typeof(string))
                RunGaAgainstTest<string>(test);
            else if (argumentsType == typeof(double) || argumentsType == typeof(int))
                RunGaAgainstTest<double>(test);
            else if (argumentsType == typeof(bool))
                RunGaAgainstTest<bool>(test);
            else
                throw new Exception($"Could not dispatch GA for test of type {argumentsType?.FullName}");
        }
        
        private void RunGaAgainstTest<T>(Test<object> test) where T : IComparable
        {
            var geneticAlgorithm = _builder.Build<T>(test);
            var history = geneticAlgorithm.Run();
            ((ExecutionHistory<T>) history).AdditionalExecutionInfo = test.GetName();
            history.Summarise();
        }
    }

}