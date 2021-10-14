using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.comparators
{
    public class GreaterThanComparatorFactory : ComparatorFactory
    {
        public GreaterThanComparatorFactory() : base(NodeCategory.GreaterThan) { }

        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            var leftPredicate = parent.Choose<T>(maxDepth - 1);
            var rightPredicate = parent.Choose<T>(maxDepth - 1);

            return new GreaterThanComparator<T>(leftPredicate, rightPredicate);
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            var leftPredicate = functionCreator.GenerateChildFromId<T>(ref id);
            var rightPredicate = functionCreator.GenerateChildFromId<T>(ref id);

            return new GreaterThanComparator<T>(leftPredicate, rightPredicate);
        }
        
        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(bool) || typeof(T) == typeof(double);
        }

    }
}