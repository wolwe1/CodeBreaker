using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomaticallyDefinedFunctions.exceptions;
using CodeBreakerLib.connectors;
using CodeBreakerLib.connectors.ga;
using CodeBreakerLib.coverage.calculators;
using CodeBreakerLib.exceptions;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.statistics;
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
            var inputValues = TryGetMemberResults(member);
            
            var parameters = CreateParametersFromInputs(inputValues);
            
            var coverageInfo = TryRunTest<T>(parameters);
            
            return coverageInfo == null ? 
                new Fitness(_coverageCalculator.GetType(), 0) : 
                new Fitness(_coverageCalculator.GetType(), _coverageCalculator.Calculate(coverageInfo));
        }

        public override MemberRecord<T> GetBest<T>(IEnumerable<MemberRecord<T>> chosenMembers)
        {
            var membersOrderedByCoverage = chosenMembers
                .OrderByDescending(m => m.GetFitness().GetFitness());

            return membersOrderedByCoverage.FirstOrDefault();
        }

        private CoverageResults TryRunTest<T>(List<object> parameters)
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

        private static IEnumerable<T> TryGetMemberResults<T>(IPopulationMember<T> member) where T : IComparable
        {
            try
            {
                return ((AdfPopulationMember<T>) member).GetResult();
            }
            catch (ProgramLoopException)
            {
                return null;
            }
        }

        private static List<object> CreateParametersFromInputs<T>(IEnumerable<T> inputValues)
        {
            if (inputValues == null)
                return null;
                
            var parameters = new List<object>();
            foreach (var inputParameter in inputValues)
            {
                parameters.Add(inputParameter);
            }

            return parameters;
        }
        
    }
}