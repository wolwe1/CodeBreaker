using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CodeBreakerLib.TestHandler
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
            var className = UserExperience.Get("Class name");
            var functionName = UserExperience.Get("Function name");

            return AddTest(className, functionName);
        }
        
        public Test<object> AddTest(string className, string functionName)
        {
            var type = GetClassByName(className);
            var method = type.GetMethod(functionName);
            
            var objectOfClass = Activator.CreateInstance(type,null);
            var argumentTypes = method.GetParameters().Select(x => x.ParameterType).ToList();
            var returnType = method.ReturnType;

            return new Test<object>(objectOfClass,method,argumentTypes,returnType);
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
        
        private Delegate CreateDelegate(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            
            if (!method.IsStatic)
                throw new ArgumentException("The provided method must be static.", "method");

            if (method.IsGenericMethod)
                throw new ArgumentException("The provided method must not be generic.", "method");
            
            return method.CreateDelegate(Expression.GetDelegateType(
                (from parameter in method.GetParameters() select parameter.ParameterType)
                .Concat(new[] { method.ReturnType })
                .ToArray()));
        }
        
    }
}