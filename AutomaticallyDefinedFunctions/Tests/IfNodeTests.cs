using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.builders;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.source.ifStatement;
using Xunit;

namespace AutomaticallyDefinedFunctions.Tests
{
    public class IfNodeTests
    {
        public IfNodeTests()
        {
            
        }

        [Theory]
        [MemberData(nameof(ValueFunctionCombinations))]
        public void IfFunctionsTriggerCorrectBranch<T, U>(IfNode<T,U> ifStatement, U expectedResult ) where T : IComparable where U : IComparable
        {
            Assert.Equal(expectedResult, ifStatement.GetValue());
        }

        public static IEnumerable<object[]> ValueFunctionCombinations()
        {
            var smaller = NodeBuilder.CreateAdditionFunction(1, 2); 
            var bigger = NodeBuilder.CreateAdditionFunction(5, 5);
            
            yield return new object[] {CreateIfStatement(smaller,bigger,new EqualsComparator<double>())
                ,bigger.GetValue()};
            yield return new object[] {CreateIfStatement(bigger,smaller,new EqualsComparator<double>())
                ,smaller.GetValue()};
            
            yield return new object[] {CreateIfStatement(smaller,bigger,new GreaterThanComparator<double>())
                ,bigger.GetValue()};
            yield return new object[] {CreateIfStatement(bigger,smaller,new GreaterThanComparator<double>())
                ,bigger.GetValue()};
            
            yield return new object[] {CreateIfStatement(smaller,bigger,new LessThanComparator<double>())
                ,smaller.GetValue()};
            yield return new object[] {CreateIfStatement(bigger,smaller,new LessThanComparator<double>())
                ,smaller.GetValue()};
        }

        private static IfNode<double,double> CreateIfStatement(AddFunc firstAndTrue,AddFunc secondAndFalse,NodeComparator<double> op)
        {
            return NodeBuilder.CreateIfStatement(firstAndTrue, secondAndFalse,
                firstAndTrue, secondAndFalse, op);
        }
        
    }
}