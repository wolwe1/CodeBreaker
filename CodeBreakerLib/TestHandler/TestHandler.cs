using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CodeBreakerLib.TestHandler
{

    public class TestHandler
    {
        public ITestStrategy TestStrategy;
        private List<Test<object>> _tests;
        private int _currentTest = 0;

        public TestHandler(ITestStrategy testStrategy)
        {
            TestStrategy = testStrategy;
            _tests = new List<Test<object>>();
        }

        public void Setup()
        {
            _tests = TestStrategy.Setup();
        }

        public List<object> RunAllTests(List<List<object>> arguments)
        {
            List<object> results = new List<object>();

            for (int i = 0; i < _tests.Count; i++)
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