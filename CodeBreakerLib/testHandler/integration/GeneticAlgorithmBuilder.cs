using System;
using System.Linq;
using AutomaticallyDefinedFunctions.generators;
using CodeBreakerLib.connectors;
using CodeBreakerLib.connectors.operators;
using CodeBreakerLib.connectors.operators.implementation;
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
        private readonly int _seed;
        private readonly int _terminalChance;
        private readonly int _maxFunctionDepth;
        private readonly int _maxMainDepth;
        private readonly int _maxGenerations;
        private readonly int _populationSize;

        public GeneticAlgorithmBuilder(int seed, int terminalChance, int maxFunctionDepth, int maxMainDepth, int maxGenerations, int populationSize)
        {
            _seed = seed;
            _terminalChance = terminalChance;
            _maxFunctionDepth = maxFunctionDepth;
            _maxMainDepth = maxMainDepth;
            _maxGenerations = maxGenerations;
            _populationSize = populationSize;
        }
        
        public GeneticAlgorithmBuilder(): this(0,65,3,5,50,1000) { }
        
        
        public GeneticAlgorithm<T> Build<T>(Test<object> test) where T : IComparable
        {
            var settings = new AdfSettings( _maxFunctionDepth, _maxMainDepth,test.GetArguments().Count,_terminalChance);
            
            IPopulationGenerator<T> populationGenerator = 
                new AdfPopulationGenerator<T>(_seed,settings);

            IPopulationMutator<T> populationMutator =
                new AdfMutator<T>(populationGenerator)
                    .UseOperator(new ReproductiveOperator<T>(40))
                    .UseOperator(new MutationOperator<T>(30,3))
                    .UseOperator(new FunctionSwapOperator<T>(30));

            var fitnessFunction = CreateFitnessFunction(test);;

            var controlModel = CreateControlModel(fitnessFunction,populationMutator);

            return CreateGa(populationGenerator, controlModel, fitnessFunction);
        }

        private static GeneticAlgorithm<T> CreateGa<T>(IPopulationGenerator<T> populationGenerator,
            IControlModel<T> controlModel, IFitnessFunction fitnessFunction) where T : IComparable
        {
            IExecutionHistory<T> history = new BasicExecutionHistory<T>();//.OutputToFile();
            
            return new GeneticAlgorithm<T>(populationGenerator,controlModel,fitnessFunction,history);
        }

        private IFitnessFunction CreateFitnessFunction(Test<object> test)
        {
            return new CompositeFitnessFunction()
                .AddEvaluation(new CodeCoverageFitnessFunction(test,new StatementCoverageCalculator()), 1);
        }

        private IControlModel<T> CreateControlModel<T>(IFitnessFunction fitnessFunction,IPopulationMutator<T> populationMutator)
        {
            return new SteadyStateControlModel<T>(populationMutator)
                .UseSelection(new FitnessProportionateSelection(fitnessFunction))
                .UseTerminationCriteria(new GenerationCountCriteria(_maxGenerations))
                .UseTerminationCriteria(new NoAverageImprovementCriteria(5))
                .UseTerminationCriteria(new DesiredFitnessForFitnessFunctionCriteria(typeof(StatementCoverageCalculator),100))
                .SetPopulationSize(_populationSize);
        }
    }
}