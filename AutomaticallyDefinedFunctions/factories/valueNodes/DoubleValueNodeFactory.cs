using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes
{
    public static class DoubleValueNodeFactory
    {
        public static IEnumerable<ValueNode<double>> GetAll()
        {
            return Enumerable.Range(0, 10).Select(d => new ValueNode<double>(d));
        }

        public static ValueNode<double> Get()
        {
            var choice = RandomNumberFactory.Next(10);
            return new ValueNode<double>(choice);
        }
    }
}