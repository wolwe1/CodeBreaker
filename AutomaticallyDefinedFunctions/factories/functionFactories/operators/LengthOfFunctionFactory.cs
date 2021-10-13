using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.other;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories.operators
{
    public class LengthOfFunctionFactory : FunctionFactory
    {
        public LengthOfFunctionFactory() : base(NodeCategory.LengthOf) { }

        public override FunctionNode<T> CreateFunction<T, TU>(int maxDepth, FunctionCreator parent)
        {
            //Sus boxing but T will be double
            return (FunctionNode<T>) (object) new LengthOfNode<TU>(parent.Choose<TU>(maxDepth - 1));
        }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double);
        }

        public override bool CanDispatchAux<T>()
        {
            return CanDispatch<T>();
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            return (FunctionNode<T>) (object) new LengthOfNode<TU>(functionCreator.GenerateChildFromId<TU>(ref id)); 
        }
    }
}