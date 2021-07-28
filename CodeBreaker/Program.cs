using System;
using System.Collections.Generic;
using CodeBreakerLib;
using CodeBreakerLib.TestHandler;

namespace CodeBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var testHandler = new TestHandler(new TestFileDescriptorStrategy());
            testHandler.Setup();

            //var test = testHandler.GetNextTest();
            //var result = test.Run(new List<object>(){1,3});
            List<List<object>> parameters = new List<List<object>>();
            parameters.Add(new List<object>(){1,4});
            parameters.Add(new List<object>(){"Hello","World"});
            
            var results = testHandler.RunAllTests(parameters);
            Console.WriteLine(results);

        }
    }
}