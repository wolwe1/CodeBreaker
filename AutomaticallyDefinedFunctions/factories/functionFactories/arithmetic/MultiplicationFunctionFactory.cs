using System;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic.multiplicationFunction;

namespace AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic
{
    public class MultiplicationFunctionFactory : ArithmeticFunctionFactory
    {
        public MultiplicationFunctionFactory(): base(NodeCategory.Multiplication){}

        private static MultiplicationFunc<T> CreateMultiplicationFunction<T>() where T : IComparable
        {
            if (typeof(T) == typeof(double))
            {
                return (MultiplicationFunc<T>) (object) new MultiplicationFunc<double>(new NumericMultiplicationFunc());
            }

            throw new InvalidOperationException($"Unable to generate add function of type {typeof(T)}");
        }
        protected override ArithmeticFunc<T> CreateArithmeticFunction<T>()
        {
            return CreateMultiplicationFunction<T>();
        }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double);
        }
    }
}