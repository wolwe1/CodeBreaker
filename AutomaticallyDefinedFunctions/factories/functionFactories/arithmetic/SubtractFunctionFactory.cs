using System;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic.subtractFunction;

namespace AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic
{
    public class SubtractFunctionFactory : ArithmeticFunctionFactory
    {
        public SubtractFunctionFactory(): base(NodeCategory.Subtract){}
        public static SubtractFunc<T> CreateSubtractFunction<T>() where T : IComparable
        {
            if (typeof(T) == typeof(double))
            {
                return (SubtractFunc<T>) (object) new SubtractFunc<double>(new NumericSubtractFunc());
            }

            throw new InvalidOperationException($"Unable to generate add function of type {typeof(T)}");
        }
        protected override ArithmeticFunc<T> CreateArithmeticFunction<T>()
        {
            return CreateSubtractFunction<T>();
        }

        public override bool CanDispatchFunctionOfType(Type t)
        {
            return t == typeof(double);
        }
    }
}