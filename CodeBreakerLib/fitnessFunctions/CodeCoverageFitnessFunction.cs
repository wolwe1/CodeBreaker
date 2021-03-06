using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.structure.adf;
using AutomaticallyDefinedFunctions.structure.adf.helpers;
using AutomaticallyDefinedFunctions.structure.state;
using CodeBreakerLib.connectors.ga;
using CodeBreakerLib.coverage.calculators;
using CodeBreakerLib.exceptions;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.statistics;
using TestObjects.source.capture;

namespace CodeBreakerLib.fitnessFunctions
{
    public class CodeCoverageFitnessFunction<TU> : AdfFitnessFunction where TU : IComparable
    {
        private readonly ICoverageCalculator _coverageCalculator;
        private readonly int _numberOfAttemptsPerMember;

        public CodeCoverageFitnessFunction(Test<object> test, ICoverageCalculator coverageCalculator, int numberOfAttemptsPerMember) : base(test)
        {
            _coverageCalculator = coverageCalculator;
            _numberOfAttemptsPerMember = numberOfAttemptsPerMember;
        }
        
        public override Fitness Evaluate<T>(IPopulationMember<T> member)
        {
            var adf = (StateBasedAdf<T,TU>) ((AdfPopulationMember<T>) member).GetAdf(false);
            
            var coverageValues = new List<CoverageResultWrapper>();

            for (var i = 0; i < _numberOfAttemptsPerMember; i++)
            {
                var inputValues =  adf.GetValues();

                var inputFailed = inputValues.Failed();

                if (!inputFailed)
                {
                    var parameters = CreateParametersFromInputs(inputValues.GetOutputValues());

                    var coverageInfo = TryRunTest<T>(parameters);
                    
                    if (coverageInfo is not null)
                    {
                        coverageValues.Add(coverageInfo);
                        adf.Update(inputValues,coverageInfo.GetReturnValue());
                    }
                    else
                    {
                        inputValues.FailOutputs(); //The outputs may have been valid, but the test was cancelled still
                        adf.Update(inputValues);
                    }
                }
                else
                {
                    inputValues.FailOutputs(); //The outputs may have been valid, but the test was cancelled still
                    adf.Update(inputValues);
                }
            }

            return coverageValues.Count == 0 ? 
                new Fitness(_coverageCalculator.GetType(), 0) :
                new Fitness(_coverageCalculator.GetType(), _coverageCalculator.Calculate(coverageValues));
        }
        
        public override MemberRecord<T> GetBest<T>(IEnumerable<MemberRecord<T>> chosenMembers)
        {
            var membersOrderedByCoverage = chosenMembers
                .OrderByDescending(m => m.GetFitness().GetFitness());

            return membersOrderedByCoverage.FirstOrDefault();
        }

        private CoverageResultWrapper TryRunTest<T>(List<object> parameters)
        {
            if (parameters is null)
                return null;

            try
            {
                return Test.Run(parameters);
            }
            catch (ParameterMismatchException)
            {
                return null;
            }
            
        }

        private static List<object> CreateParametersFromInputs<T>(IEnumerable<T> inputValues)
        {
            return inputValues?.Cast<object>().ToList();
        }
        
    }
}