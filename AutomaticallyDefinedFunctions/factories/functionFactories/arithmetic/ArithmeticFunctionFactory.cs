using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic
{
    public abstract class ArithmeticFunctionFactory : FunctionFactory
    {
        protected ArithmeticFunctionFactory(string symbol) : base(symbol) { }
        
        protected abstract ArithmeticFunc<T> CreateArithmeticFunction<T>() where T : IComparable;
        public override FunctionNode<T> CreateFunction<T, TU>(int maxDepth, FunctionCreator parent)
        {
            var addFunc = CreateArithmeticFunction<T>();

            var firstChild = parent.Choose<T>(maxDepth - 1);
            var secondChild = parent.Choose<T>(maxDepth - 1);

            return addFunc.AddChild(firstChild).AddChild(secondChild);
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            var leftChild = functionCreator.GenerateChildFromId<T>(ref id);
            
            var rightChild = functionCreator.GenerateChildFromId<T>(ref id);
            
            var addFunc = CreateArithmeticFunction<T>()
                .AddChild(leftChild)
                .AddChild(rightChild);
            
            return addFunc;
        }
        
    }
}