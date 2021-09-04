using System;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.generators.functionGenerators
{
    public abstract class FunctionGenerator<T> : Generator<T,FunctionGenerator<T>> where T : IComparable
    {
        protected FunctionGenerator(Random generator, int terminalChance) : base(generator, terminalChance){}

        public abstract FunctionNode<T> Generate();
        
    }
}