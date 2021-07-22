using System;

namespace AutomaticallyDefinedFunctions.Extensions
{
    public static class AddFunctionFactory
    {
        public static AddFunc<T> CreateAddFunction<T>() where T : IComparable
        {
            if (typeof(T) == typeof(string))
            {
                return (AddFunc<T>) (object) new StringAddFunction();
            }

            if (typeof(T) == typeof(double))
            {
                return (AddFunc<T>)  (object) new NumericAddFunc();
            }
            
            if (typeof(T) == typeof(bool))
            {
                return (AddFunc<T>)  (object) new BooleanAddFunc();
            }

            throw new InvalidOperationException($"Unable to generate add function of type {typeof(T)}");
        }
        
    }
}