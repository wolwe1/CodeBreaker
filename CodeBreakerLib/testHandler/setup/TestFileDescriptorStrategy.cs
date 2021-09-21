using System;
using System.Collections.Generic;
using System.IO;

namespace CodeBreakerLib.testHandler.setup
{
    public class TestFileDescriptorStrategy : ITestStrategy
    {
        private readonly TestLookupStrategy _helperStrategy;
        private readonly string _projectDirectory;
        public TestFileDescriptorStrategy()
        {
            _helperStrategy = new TestLookupStrategy();
            // string workingDirectory = Environment.CurrentDirectory;
            //
            // var parentDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            // var pathSegments = parentDirectory.Split("\\");
            // pathSegments = pathSegments.Take(pathSegments.Length - 1).ToArray();
            // projectDirectory = string.Join("\\", pathSegments) + "\\CodeBreakerLib\\";
            _projectDirectory =
                "D:\\Honours\\second year\\COS700\\Research project\\code\\Implementation\\CodeBreaker\\CodeBreakerLib\\";

        }

        public List<Test<object>> Setup()
        {
            const string fileName = "sample.txt";
            var target = _projectDirectory + fileName;

            if (!File.Exists(target)) throw new Exception($"Test sample file: {target} does not exist");
            
            var lines = File.ReadAllLines(target);

            return GetTestsFromHelper(lines);

        }

        public Test<object> AddTest()
        {
            throw new NotImplementedException();
        }

        private List<Test<object>> GetTestsFromHelper(string[] lines)
        {
            var tests = new List<Test<object>>();
            
            for (var i = 0; i < lines.Length; i+= 3)
            {
                var pathToDll = lines[i];
                var className = lines[i+1];
                var functionName = lines[i+2];

                var test = _helperStrategy.AddTest(pathToDll,className, functionName);

                tests.Add(test);
            }

            return tests;
        }
    }
}