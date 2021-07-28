using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeBreakerLib
{
    public class Test<T>
    {
        public object ClassName;
        public MethodInfo FunctionName;
        public List<Type> ArgumentTypes;
        private Type ReturnType;
        private Delegate Function;

        public Test(object className, MethodInfo functionName, List<Type> argumentTypes)
        {
            ClassName = className;
            FunctionName = functionName;
            ArgumentTypes = argumentTypes;
        }

        public Test(object className, MethodInfo functionName, List<Type> argumentTypes, Type returnType, Delegate function)
        {
            ClassName = className;
            FunctionName = functionName;
            ArgumentTypes = argumentTypes;
            ReturnType = returnType;
            Function = function;
        }

        public Test(object className, MethodInfo functionName, List<Type> argumentTypes, Type returnType)
        {
            ClassName = className;
            FunctionName = functionName;
            ArgumentTypes = argumentTypes;
            ReturnType = returnType;
        }

        public T Run(List<object> parameters)
        {
            if (ValidParameters(parameters))
            {
                object[] parms = parameters.Cast<object>().ToArray();
                return (T) FunctionName.Invoke(ClassName, parms);
            }
            else
                throw new Exception($"Provided parameters for method {ClassName}.{FunctionName} do not match.\n" +
                                    $"Provided[{string.Join(",", parameters)}]\n" +
                                    $"Expected[{string.Join(",", ArgumentTypes)}]\n"
                );
        }

        private bool ValidParameters(List<object> parameters)
        {
            for (var i = 0; i < ArgumentTypes.Count(); i++)
            {
                var expectedArgument = ArgumentTypes[i];
                var givenParameter = parameters[i];

                if (!givenParameter.GetType().Equals(expectedArgument))
                    return false;
            }

            return true;
        }
    }
}