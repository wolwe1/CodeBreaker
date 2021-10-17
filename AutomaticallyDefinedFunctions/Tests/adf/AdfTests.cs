using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.generators.adf;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.adf;
using Xunit;

namespace AutomaticallyDefinedFunctions.Tests.adf
{
    public class AdfTests
    {
        [Theory]
        [MemberData(nameof(AdfTypeGeneratorSet))]
        public void CreateAdfsToEvaluate<T>(AdfGenerator<T> generator) where T : IComparable
        {
            for (var i = 0; i < 50; i++)
            {
                var adf = generator.Generate();
                AdfCorrectlyProduceId<T>(adf,generator);
            }
        }
        
        public void AdfCorrectlyProduceId<T>(Adf<T> adf,AdfGenerator<T> generator) where T : IComparable
        {
            var originalId = adf.GetId();

            var functionFromId = generator.GenerateFromId(originalId);

            Assert.Equal(functionFromId.GetId(), originalId);
        }

        public static IEnumerable<object[]> AdfTypeGeneratorSet()
        {
            yield return new object[]
            {
                NodeBuilder.CreateAdfGenerator<string>()
            };
            
            yield return new object[]
            {
                NodeBuilder.CreateAdfGenerator<double>()
            };
            
            yield return new object[]
            {
                NodeBuilder.CreateAdfGenerator<bool>()
            };
        }


    }
}