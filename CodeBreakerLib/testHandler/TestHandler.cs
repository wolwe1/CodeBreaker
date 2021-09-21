using System;
using System.Collections.Generic;
using System.Linq;
using CodeBreakerLib.resultsHandler;
using CodeBreakerLib.testHandler.integration;
using CodeBreakerLib.testHandler.setup;

namespace CodeBreakerLib.testHandler
{

    public class TestHandler
    {
        private readonly ITestStrategy _testStrategy;
        private readonly ResultsHandler _resultsHandler = new();
        
        private List<Test<object>> _tests;
        private int _currentTest = 0;

        public TestHandler(ITestStrategy testStrategy)
        {
            _testStrategy = testStrategy;
            _tests = new List<Test<object>>();
        }

        public void Setup()
        {
            _tests = _testStrategy.Setup();
        }

        public void RunAllTests()
        {
            for (var i = 0; i < _tests.Count; i++)
            {
                RunGAAgainstNextTest();
            }
            
        }

        public Test<object>? GetNextTest()
        {
            return _currentTest >= _tests.Count ? null : _tests.ElementAt(_currentTest++);
        }

        public void RunGAAgainstNextTest()
        {
            var test = GetNextTest();
            Console.WriteLine($"Running GA against Test: {test.GetName()}");
            //Only support one arg now TODO: Implement for multiple
            var testArgs = test?.GetArguments()?.ElementAt(0);

            if (testArgs == typeof(string))
                RunGAAgainstTest<string>(test);
            else if (testArgs == typeof(double) || testArgs == typeof(int))
                RunGAAgainstTest<double>(test);
            else if (testArgs == typeof(bool))
                RunGAAgainstTest<bool>(test);
            else
                throw new Exception($"Could not dispatch GA for test of type {testArgs?.FullName}");
        }

        private void RunGAAgainstTest<T>(Test<object> test) where T : IComparable
        {
            var geneticAlgorithm = GeneticAlgorithmBuilder.Build<T>(test);
            var history = geneticAlgorithm.Run();
            history.Summarise();
        }
    }

}