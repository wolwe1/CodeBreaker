using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.comparators
{
    public class NotEqualComparatorFactory : ComparatorFactory
    {
        public NotEqualComparatorFactory() : base(NodeCategory.NotEqual) { }

        public override FunctionNode<T> CreateFunction<T, TU>(int maxDepth, FunctionCreator parent)
        {
            return new NotEqualComparator<T>(parent.Choose<T>(maxDepth - 1),parent.Choose<T>(maxDepth - 1));
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            return new NotEqualComparator<T>(
                functionCreator.GenerateChildFromId<T>(ref id),
                functionCreator.GenerateChildFromId<T>(ref id));

        }
    }
}