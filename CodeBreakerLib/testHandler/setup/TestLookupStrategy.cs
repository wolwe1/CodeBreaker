using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeBreakerLib.dynamicLoading;

namespace CodeBreakerLib.testHandler.setup
{
    public class TestLookupStrategy : ITestStrategy
    {
        public List<Test<object>> Setup()
        {
            List<Test<object>> _tests = new List<Test<object>>();
            while (UserExperience.Ask("Add test"))
            {
                _tests.Add(AddTest());
            }

            return _tests;
        }

        public Test<object> AddTest()
        {
            var pathToDll = UserExperience.Get("Full path to .dll");
            var className = UserExperience.Get("Class name");
            var functionName = UserExperience.Get("Function name");

            return AddTest(pathToDll,className, functionName);
        }
        
        public Test<object> AddTest(string assemblyLocation,string className, string functionName)
        {
            var methodToTest = AssemblyLoader.GetMethodFromAssembly(assemblyLocation, className, functionName);

            return new Test<object>(methodToTest);
        }
        
        public Type GetClassByName(string name)
        {
            var qualifiedName = LookupConversion(name);
            
            if (qualifiedName == null)
            {
                Assembly asm = typeof(TestHandler).Assembly;

                var assemblyTypes = asm.ExportedTypes;

                return assemblyTypes.FirstOrDefault(x => x.Name == name);
            }

            return Type.GetType(qualifiedName);
        }

#nullable enable
        private string? LookupConversion(string typeName)
        {
            return typeName.ToLower() switch
            {
                "int" => "System.Int32",
                "double" => "System.Double",
                "string" => "System.String",
                "boolean" => "System.Boolean",
                _ => null
            };
        }

    }
}