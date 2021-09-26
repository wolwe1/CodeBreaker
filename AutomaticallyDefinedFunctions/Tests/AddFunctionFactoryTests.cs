using AutomaticallyDefinedFunctions.factories.addFunction;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using Xunit;

namespace AutomaticallyDefinedFunctions.Tests
{
    public class AddFunctionFactoryTests
    {
        [Fact]
        public void StringCallGeneratesStringAdder()
        {
            var adder = AddFunctionFactory.CreateAddFunction<string>();
            var adder2 = AddFunctionFactory.CreateAddFunction<double>();
            var adder3 = AddFunctionFactory.CreateAddFunction<bool>();

            Assert.Equal(typeof(StringAddFunction),adder.GetType());
            Assert.Equal(typeof(NumericAddFunc),adder2.GetType());
            Assert.Equal(typeof(BooleanAddFunc),adder3.GetType());
        }
    }
}