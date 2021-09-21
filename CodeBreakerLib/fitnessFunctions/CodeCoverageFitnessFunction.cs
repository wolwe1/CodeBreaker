using System.Collections.Generic;
using AutomaticallyDefinedFunctions.exceptions;
using CodeBreakerLib.connectors;
using CodeBreakerLib.coverage.calculators;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using TestObjects.source.capture;

namespace CodeBreakerLib.fitnessFunctions
{
    public class CodeCoverageFitnessFunction : ADFFitnessFunction
    {
        private readonly ICoverageCalculator _coverageCalculator;
        public CodeCoverageFitnessFunction(Test<object> test, ICoverageCalculator coverageCalculator) : base(test)
        {
            _coverageCalculator = coverageCalculator;
        }
        
        public override Fitness Evaluate<T>(IPopulationMember<T> member)
        {
            try
            {
                var input = ((ADFPopulationMember<T>) member).GetResult();

                var result = Test.Run(new List<object>() {input});

                var coverageInfo = (CoverageResults<T>) result;

                return new Fitness(_coverageCalculator.GetType(), _coverageCalculator.Calculate(coverageInfo));
            }
            catch (ProgramLoopException)
            {
                return new Fitness(_coverageCalculator.GetType(),0);
            }
            
        }
        
        
    }
}