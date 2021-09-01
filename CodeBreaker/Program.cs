using System;
using System.Collections.Generic;
using CodeBreakerLib;
using CodeBreakerLib.dynamicLoading;
using CodeBreakerLib.TestHandler;

namespace CodeBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var testHandler = new TestHandler(new TestFileDescriptorStrategy());
            testHandler.Setup();
            
            List<List<object>> parameters = new List<List<object>>();
            parameters.Add(new List<object>(){"racecar"});
            parameters.Add(new List<object>(){"Hello, World"});
            
            var results = testHandler.RunAllTests(parameters);
            Console.WriteLine(results[0]);
            Console.WriteLine(results[1]);
            
        }
    }
}