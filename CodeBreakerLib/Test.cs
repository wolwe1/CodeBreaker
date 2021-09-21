using System;
using System.Collections.Generic;
using System.Linq;
using CodeBreakerLib.dynamicLoading;
using CodeBreakerLib.exceptions;

namespace CodeBreakerLib
{
    public class Test<T>
    {
        private readonly DynamicMethod _method;

        public Test(DynamicMethod method)
        {
            _method = method;
        }

        public T Run(List<object> parameters)
        {
            if (!ValidParameters(parameters))
            {
                try
                {
                    parameters = castParametersIfApplicable(parameters);
                }
                catch (Exception e)
                {
                    throw ParameterMismatchException.Create(_method,parameters);
                }
                
            }

            var parms = parameters.Cast<object>().ToArray();
            
            return (T) _method.InvokeMethod(parms);
        }

        private bool ValidParameters(List<object> parameters)
        {
            for (var i = 0; i < _method.GetArguments().Count; i++)
            {
                var expectedArgument = _method.GetArguments()[i];
                var givenParameter = parameters[i];

                if (givenParameter.GetType() != expectedArgument) 
                    return false;
            }

            return true;
        }

        public List<object> castParametersIfApplicable(List<object> parameters)
        {
            var convertedParams = new List<object>();
            for (var i = 0; i < _method.GetArguments().Count; i++)
            {
                var expectedArgument = _method.GetArguments()[i];
                var givenParameter = parameters[i];
                
                var convertedParam = Convert.ChangeType(givenParameter, expectedArgument);
                convertedParams.Add(convertedParam);
            }

            return convertedParams;
        }

        public List<Type> GetArguments()
        {
            return _method.GetArguments();
        }

        public string GetName()
        {
            return _method.GetFullName();
        }
    }
}