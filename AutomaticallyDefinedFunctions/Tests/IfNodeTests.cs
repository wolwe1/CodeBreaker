﻿using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.factories.addFunction;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;
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
        public void IfFunctionsTriggerCorrectBranch<T, U>(IfNode<T,U> ifStatement, T expectedResult ) where T : IComparable where U : IComparable
        {
            Assert.Equal(expectedResult, ifStatement.GetValue());
        }

        [Theory]
        [MemberData(nameof(EqualityCombinations))]
        public void ComparatorsChainUsingOr(string expectedValue,IfNode<string,double> ifStatement,NodeComparator<double> comparator)
        {
            ifStatement.SetComparisonOperator(comparator);
            Assert.True(expectedValue == ifStatement.GetValue());
        }

        public static IEnumerable<object[]> EqualityCombinations()
        {
            var smaller = NodeBuilder.CreateAdditionFunction(1, 2); 
            var bigger = NodeBuilder.CreateAdditionFunction(5, 5);

            var trueStatement = new ValueNode<string>("The statement is true");
            var falseStatement = new ValueNode<string>("The statement is false");

            var equals = new EqualsComparator<double>();
            var lessThan = new LessThanComparator<double>();
            var greaterThan = new GreaterThanComparator<double>();
            var lessThanOrEquals = lessThan.SetAdditionalComparator(equals);
            var greaterThanOrEquals = greaterThan.SetAdditionalComparator(equals);
            
            var ifStatement = NodeBuilder
                .CreateIfStatement(smaller, bigger, trueStatement, falseStatement, equals);

            yield return new object[] {falseStatement.GetValue(),ifStatement,equals };
            
            yield return new object[] {trueStatement.GetValue(),ifStatement,lessThan };
            
            yield return new object[] {falseStatement.GetValue(),ifStatement,greaterThan };
            
            yield return new object[] {trueStatement.GetValue(),ifStatement,lessThanOrEquals };

            yield return new object[] {falseStatement.GetValue(),ifStatement,greaterThanOrEquals };
            
        }

        [Fact]
        public void ComparatorsReturnTrueIfChainedWithEqualsWhenBranchesAreEqual()
        {
            var smaller = NodeBuilder.CreateAdditionFunction(1, 2); 

            var trueStatement = new ValueNode<string>("The statement is true");
            var falseStatement = new ValueNode<string>("The statement is false");

            var equals = new EqualsComparator<double>();
            var lessThan = new LessThanComparator<double>();
            var greaterThan = new GreaterThanComparator<double>();
            var lessThanOrEquals = lessThan.SetAdditionalComparator(equals);
            var greaterThanOrEquals = lessThan.SetAdditionalComparator(equals);
            
            var ifStatement = NodeBuilder
                .CreateIfStatement(smaller, smaller, trueStatement, falseStatement, lessThanOrEquals);
            
            Assert.Equal(trueStatement.GetValue(),ifStatement.GetValue());
            ifStatement.SetComparisonOperator(greaterThanOrEquals);
            Assert.Equal(trueStatement.GetValue(),ifStatement.GetValue());

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

        private static IfNode<double,double> CreateIfStatement(AddFunc<double> firstAndTrue,AddFunc<double> secondAndFalse,NodeComparator<double> op)
        {
            return NodeBuilder.CreateIfStatement(firstAndTrue, secondAndFalse,
                firstAndTrue, secondAndFalse, op);
        }
        
        [Theory]
        [MemberData(nameof(IfFunctionCombinations))]
        public void IFFunctionOnlyValidWithAllProperties<T, U>(IfNode<T,U> ifStatement, bool expectedResult ) where T : IComparable where U : IComparable
        {
            Assert.Equal(expectedResult, ifStatement.IsValid());
        }
        
        public static IEnumerable<object[]> IfFunctionCombinations()
        {
            var smaller = NodeBuilder.CreateAdditionFunction(1, 2); 
            var bigger = NodeBuilder.CreateAdditionFunction(5, 5);
            var trueStatement = new ValueNode<string>("The statement is true");
            var falseStatement = new ValueNode<string>("The statement is false");

            var equals = new EqualsComparator<double>();

            var func = NodeBuilder.CreateIfStatement<string,double>(null, null, null, null, null);
            var func2 = NodeBuilder.CreateIfStatement<string,double>(smaller, null, null, null, null);
            var func3 = NodeBuilder.CreateIfStatement<string,double>(smaller, bigger, null, null, null);
            var func4 = NodeBuilder.CreateIfStatement<string,double>(smaller, bigger, trueStatement, null, null);
            var func5 = NodeBuilder.CreateIfStatement<string,double>(smaller, bigger, trueStatement, null, null);
            var func6 = NodeBuilder.CreateIfStatement<string,double>(smaller, bigger, trueStatement, falseStatement, equals);

            yield return new object[] {func,false};
            
            yield return new object[] {func2,false};
            
            yield return new object[] {func3,false};
            
            yield return new object[] {func4,false};
            
            yield return new object[] {func5,false};
            
            yield return new object[] {func6,true};
        }

    }
}