using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CodeBreakerLib.TestHandler
{

    public class TestHandler
    {
        private readonly ITestStrategy _testStrategy;
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

        public List<object> RunAllTests(List<List<object>> arguments)
        {
            var results = new List<object>();

            for (var i = 0; i < _tests.Count; i++)
            {
                var currentTest = _tests.ElementAt(i);
                var argumentsForTest = arguments.ElementAt(i);
                results.Add(currentTest.Run(argumentsForTest));
            }

            return results;
        }

        public Test<object>? GetNextTest()
        {
            return _currentTest >= _tests.Count ? null : _tests.ElementAt(_currentTest++);
        }
        
    }

}