using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.generators.adf;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.adf;
using Xunit;

namespace AutomaticallyDefinedFunctions.Tests.adf
{
    public class FunctionDefinitionTests
    {
        [Theory]
        [MemberData(nameof(AdfTypeGeneratorSet))]
        public void CreateDefinitionsToEvaluate<T>(AdfGenerator<T> generator) where T : IComparable
        {
            for (var i = 0; i < 50; i++)
            {
                var functionDefinition = generator.Generate().GetDefinitions().First();
                FunctionDefinitionsCorrectlyProduceId(functionDefinition,generator);
            }
        }

        public void FunctionDefinitionsCorrectlyProduceId<T>(FunctionDefinition<T> adf,AdfGenerator<T> generator) where T : IComparable
        {
            var originalId = adf.GetId();

            var functionFromId = generator.GenerateFunctionFromId(originalId);

            Assert.Equal(functionFromId.GetId(), originalId);
        }

        public static IEnumerable<object[]> AdfTypeGeneratorSet()
        {
            yield return new object[]
            {
                NodeBuilder.CreateStateAdfGenerator<string,string>()
            };
            
            yield return new object[]
            {
                NodeBuilder.CreateStateAdfGenerator<double,string>()
            };
            
            yield return new object[]
            {
                NodeBuilder.CreateStateAdfGenerator<bool,string>()

            };
        }
    }
}