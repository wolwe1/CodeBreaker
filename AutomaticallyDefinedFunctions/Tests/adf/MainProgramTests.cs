using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure;
using Xunit;

namespace AutomaticallyDefinedFunctions.Tests.adf
{
    public class MainProgramTests
    {
        [Theory]
        [MemberData(nameof(AdfTypeGeneratorSet))]
        public void CreateMainProgramsToEvaluate<T>(AdfGenerator<T> generator) where T : IComparable
        {
            for (var i = 0; i < 50; i++)
            {
                var mainProgram = generator.Generate().GetMainPrograms().First();
                MainProgramCorrectlyProduceId(mainProgram,generator);
                ReproducedMainsReturnSameResult(mainProgram, generator);
            }
        }
        
        public void ReproducedMainsReturnSameResult<T>(MainProgram<T> mainProgram,AdfGenerator<T> generator) where T : IComparable
        {
            var originalId = mainProgram.GetId();

            var mainFromId = generator.GenerateMainFromId(originalId);

            var reproducedValue = TryRun(mainFromId);
            var originalValue = TryRun(mainProgram);
            Assert.Equal(originalValue, reproducedValue );
        }

        private T TryRun<T>(MainProgram<T> prog) where T : IComparable
        {
            try
            {
                return prog.GetValue();
            }
            catch (ProgramLoopException )
            {
                return default(T);
            }
        }
        
        public void MainProgramCorrectlyProduceId<T>(MainProgram<T> adf,AdfGenerator<T> generator) where T : IComparable
        {
            var originalId = adf.GetId();

            var mainFromId = generator.GenerateMainFromId(originalId);

            Assert.Equal(mainFromId.GetId(), originalId);
        }

        public static IEnumerable<object[]> AdfTypeGeneratorSet()
        {
            yield return new object[]
            {
                new AdfGenerator<string>(1,
                    new AdfSettings(2, 3, 1, 65))
            };
            
            yield return new object[]
            {
                new AdfGenerator<double>(1,
                    new AdfSettings(2, 3, 1, 65))
            };
            
            yield return new object[]
            {
                new AdfGenerator<bool>(1,
                    new AdfSettings(2, 3, 1, 65))
            };
        }
    }
}