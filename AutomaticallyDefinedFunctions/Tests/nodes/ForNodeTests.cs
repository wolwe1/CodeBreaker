using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.structure.forLoop;
using AutomaticallyDefinedFunctions.structure.ifStatement.comparators;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;
using Xunit;

namespace AutomaticallyDefinedFunctions.Tests.nodes
{
    public class ForNodeTests
    {
        [Theory]
        [MemberData(nameof(InvalidForLoopCombinations))]
        public void IFFunctionOnlyValidWithAllProperties<T>(ForLoopNode<T,double> loop ) where T : IComparable
        {
            Assert.Throws<InvalidStructureException>(() => loop.GetValue());
        }
        
        [Theory]
        [MemberData(nameof(LoopFunctionCombinations))]
        public void LoopOutputsCorrectly<T>(ForLoopNode<T,double> loop, T expectedResult ) where T : IComparable
        {
            Assert.Equal(expectedResult,loop.GetValue());
        }
        
        public static IEnumerable<object[]> LoopFunctionCombinations()
        {
            var stringLoop = NodeBuilder.CreateSimpleForLoop(3d, "a");
            var numberLoop = NodeBuilder.CreateSimpleForLoop(3d, 1d);
            var booleanLoop = NodeBuilder.CreateSimpleForLoop(3d, true);
            
            yield return new object[] {stringLoop,"aaa"};
            
            yield return new object[] {numberLoop,3};
            
            yield return new object[] {booleanLoop,true};
        }
        
        public static IEnumerable<object[]> InvalidForLoopCombinations()
        {
            var counter = new ValueNode<double>(0);
            var increment = new ValueNode<double>(1);
            var bounds = new ValueNode<double>(10);
            var comparator = new LessThanComparator<double>();
            var block = new ValueNode<string>("a");

            var equals = new EqualsComparator<double>();

            var func = NodeBuilder.CreateForLoop<string,double>(null, null, null, null, null);
            var func2 = NodeBuilder.CreateForLoop<string,double>(counter, null, null, null, null);
            var func3 = NodeBuilder.CreateForLoop<string,double>(counter, increment, null, null, null);
            var func4 = NodeBuilder.CreateForLoop<string,double>(counter, increment, bounds, null, null);
            var func5 = NodeBuilder.CreateForLoop<string,double>(counter, increment, bounds, comparator, null);

            yield return new object[] {func};
            
            yield return new object[] {func2};
            
            yield return new object[] {func3};
            
            yield return new object[] {func4};
            
            yield return new object[] {func5};
        }
    }
}