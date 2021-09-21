using System;
using CodeBreakerLib.connectors;
using CodeBreakerLib.coverage.calculators;
using CodeBreakerLib.fitnessFunctions;
using GeneticAlgorithmLib.source.controlModel;
using GeneticAlgorithmLib.source.controlModel.selectionMethods;
using GeneticAlgorithmLib.source.controlModel.terminationCriteria;
using GeneticAlgorithmLib.source.core;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.mockImplementations;
using GeneticAlgorithmLib.source.operators;
using GeneticAlgorithmLib.source.statistics.history;

namespace CodeBreakerLib.testHandler.integration
{
    public class GeneticAlgorithmBuilder
    {
        public static GeneticAlgorithm<T> Build<T>(Test<object>? test) where T : IComparable
        {
            IPopulationGenerator<T> populationGenerator = new ADFPopulationGenerator<T>(0,  65,  3,  5);
            IPopulationMutator<T> populationMutator = new NoOpMutator<T>(populationGenerator);

            var fitnessFunction =
                new CompositeFitnessFunction().AddEvaluation(new CodeCoverageFitnessFunction(test,new StatementCoverageCalculator()), 1);
            
            IControlModel<T> controlModel = new SteadyStateControlModel<T>(populationMutator)
                .UseSelection(new FitnessProportionateSelection(fitnessFunction))
                .UseTerminationCriteria(new GenerationCountCriteria(50))
                .UseTerminationCriteria(new NoAverageImprovementCriteria(5))
                .UseTerminationCriteria(new DesiredFitnessForFitnessFunctionCriteria(typeof(StatementCoverageCalculator),100))
                .SetPopulationSize(1000);
            
            IExecutionHistory<T> history = new BasicExecutionHistory<T>();
            var geneticAlgorithm = new GeneticAlgorithm<T>(populationGenerator,controlModel,fitnessFunction,history);
            
            return geneticAlgorithm;
        }
    }
}