using System;
using AutomaticallyDefinedFunctions.factories.addFunction;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.generators.functionGenerators
{
    public class AddFunctionGenerator<T> : FunctionGenerator<T> where T : IComparable
    {
        public AddFunctionGenerator(Random generator, int terminalChance) : base(generator, terminalChance)
        {
        }

        public override FunctionNode<T> Generate()
        {
            var addFunc = AddFunctionFactory.CreateAddFunction<T>();

            var firstChild = Choose();
            var secondChild = Choose();

            return addFunc.AddChild(firstChild).AddChild(secondChild);
        }
    }
}