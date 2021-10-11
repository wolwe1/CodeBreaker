using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.comparators
{
    public class LessThanComparatorFactory : ComparatorFactory
    {
        public LessThanComparatorFactory() : base(NodeCategory.LessThan) { }

        public override FunctionNode<T> CreateFunction<T, TU>(int maxDepth, FunctionGenerator parent)
        {
            var leftPredicate = parent.Choose<T>(maxDepth - 1);
            var rightPredicate = parent.Choose<T>(maxDepth - 1);

            return new LessThanComparator<T>(leftPredicate, rightPredicate);
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionGenerator functionGenerator)
        {
            var leftPredicate = functionGenerator.GenerateChildFromId<T>(ref id);
            var rightPredicate = functionGenerator.GenerateChildFromId<T>(ref id);

            return new LessThanComparator<T>(leftPredicate, rightPredicate);
        }
        
        public override bool CanDispatchFunctionOfType(Type t)
        {
            return t == typeof(string) || t == typeof(bool) || t == typeof(double);
        }
        
    }
}