using System;
using AutomaticallyDefinedFunctions.factories.valueNodes.standard;
using AutomaticallyDefinedFunctions.parsing;

namespace AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    public interface IRandomStateNode {}
    
    public class RandomStateNode<T> : StateNode<T>, IRandomStateNode where T : IComparable
    {
        public RandomStateNode() : base(NodeCategory.Random) { }

        public override T GetValue()
        {
            return ValueNodeFactory.GetT<T>().GetValue();
        }

        public override INode<T> GetCopy()
        { 
            return new RandomStateNode<T>();
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}