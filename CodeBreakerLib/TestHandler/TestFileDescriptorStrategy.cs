using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeBreakerLib.TestHandler
{
    public class TestFileDescriptorStrategy : ITestStrategy
    {
        private TestLookupStrategy _helperStrategy;
        private string projectDirectory;
        public TestFileDescriptorStrategy()
        {
            _helperStrategy = new TestLookupStrategy();
            string workingDirectory = Environment.CurrentDirectory;

            var parentDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var pathSegments = parentDirectory.Split("\\");
            pathSegments = pathSegments.Take(pathSegments.Length - 1).ToArray();
            projectDirectory = string.Join("\\", pathSegments) + "\\CodeBreakerLib\\";
            
        }

        public List<Test<object>> Setup()
        {
            var fileName = "sample.txt";
            var target = projectDirectory + fileName;

            if (!File.Exists(target)) throw new Exception("Test sample file does not exist");
            
            string[] lines = File.ReadAllLines(target);

            return GetTestsFromHelper(lines);

        }

        public Test<object> AddTest()
        {
            throw new System.NotImplementedException();
        }

        public List<Test<object>> GetTestsFromHelper(string[] lines)
        {
            List<Test<object>> tests = new List<Test<object>>();
            
            for (int i = 0; i < lines.Length; i+= 2)
            {
                var className = lines[i];
                var functionName = lines[i+1];

                var test = _helperStrategy.AddTest(className, functionName);

                tests.Add(test);
            }

            return tests;
        }
    }
}