using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.source.forLoop;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;
using Xunit;

namespace AutomaticallyDefinedFunctions.Tests
{
    public class ForNodeTests
    {
        [Theory]
        [MemberData(nameof(LoopFunctionCombinations))]
        public void IFFunctionOnlyValidWithAllProperties<T>(ForLoopNode<T,double> loop, T expectedResult ) where T : IComparable
        {
            Assert.Equal(expectedResult, loop.GetValue());
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
    }
}