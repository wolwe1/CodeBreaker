using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.boolean;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.comparators
{
    public class NotNullComparatorFactory : ComparatorFactory
    {
        public NotNullComparatorFactory() : base(NodeCategory.NotNull) { }

        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            return new NotNullComparator<T>(parent.Choose<T>(maxDepth - 1));
        }
        
        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            return new NotNullComparator<T>(functionCreator.GenerateChildFromId<T>(ref id));
        }
        
        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(string);
        }
    }
}