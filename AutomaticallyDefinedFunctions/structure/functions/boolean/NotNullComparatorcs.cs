using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.boolean
{
    public class NotNullComparator<TU> : NodeComparator<bool> where TU : IComparable
    {
        private INode<TU> Argument => GetChildAs<TU>(0);

        private NotNullComparator() : base(1)
        {
        }
        
        public NotNullComparator(INode<TU> argument) : this()
        {
            RegisterChildren(new List<INode>(){argument});
        }

        public override string GetId()
        {
            return CreateId<TU>(NodeCategory.NotNull);
        }

        public override bool GetValue()
        {
            return Argument.GetValue() is not null;
        }
        
        public override INode<bool> GetCopy()
        {
            return new NotNullComparator<TU>(Argument.GetCopy());
        }

        public override INode<bool> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            if (Argument.IsNullNode())
                return SetArgument(generator.CreateFunction<TU>(maxDepth));
            
            return SetArgument(Argument.ReplaceNullNodes(maxDepth, generator));
        }

        public NotNullComparator<TU> SetArgument(INode<TU> node)
        {
            return new NotNullComparator<TU>(node);
        }

        public override bool PassesCheck()
        {
            return GetValue();
        }
    }
}