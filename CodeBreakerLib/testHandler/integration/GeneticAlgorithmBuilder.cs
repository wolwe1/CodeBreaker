using System;
using AutomaticallyDefinedFunctions.generators.adf;
using CodeBreakerLib.connectors;
using CodeBreakerLib.connectors.ga.state;
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
        
        public GeneticAlgorithmBuilder(): this(0,65,6,15,50,100) { }

        
        public GeneticAlgorithm<T> Build<T>(Test<object> test) where T : IComparable
        {
            IPopulationGenerator<T> populationGenerator = CreatePopulationGenerator<T>(test);

            IPopulationMutator<T> populationMutator =
                new AdfMutator<T>(populationGenerator)
                    .UseOperator(new ReproductiveOperator<T>(30))
                    .UseOperator(new MutationOperator<T>(40, 5))
                    .UseOperator(new CrossoverOperator<T>(30));
                    //.UseOperator(new FunctionSwapOperator<T>(30));

            var fitnessFunction = CreateFitnessFunction(test);;

            var controlModel = CreateControlModel(fitnessFunction,populationMutator);

            return CreateGa(populationGenerator, controlModel);
        }

        public IPopulationGenerator<T> CreatePopulationGenerator<T>(Test<object> test) where T : IComparable
        {
            var returnType = test.GetReturnType();

            if (returnType == typeof(string))
                return CreatePopulationGenerator<T, string>(test);

            if (returnType == typeof(double))
                return CreatePopulationGenerator<T, double>(test);

            if (returnType == typeof(bool))
                return CreatePopulationGenerator<T, bool>(test);

            throw new Exception("Genetic algorithm builder could not dispatch for return type of function");

        }
        
        public IPopulationGenerator<T> CreatePopulationGenerator<T,TU>(Test<object> test) where T : IComparable where TU : IComparable
        {
            var settings = new StateAdfSettings<T,TU>( _maxFunctionDepth, _maxMainDepth,test.GetArguments().Count,_terminalChance);

            return new StateAdfPopulationGenerator<T,TU>(_seed,settings);
        }
        public static GeneticAlgorithm<T> CreateGa<T>(IPopulationGenerator<T> populationGenerator,
            IControlModel<T> controlModel) where T : IComparable
        {
            IExecutionHistory<T> history = new ExecutionOutput<T>().OutputToFile();
            
            return new GeneticAlgorithm<T>(populationGenerator,controlModel,history).Print();
        }

        public IFitnessFunction CreateFitnessFunction(Test<object> test)
        {
            var returnType = test.GetReturnType();

            if (returnType == typeof(string))
                return new CompositeFitnessFunction()
                    .AddEvaluation(new CodeCoverageFitnessFunction<string>(test,new StatementCoverageCalculator(),5), 1);

            if (returnType == typeof(double))
                return new CompositeFitnessFunction()
                    .AddEvaluation(new CodeCoverageFitnessFunction<double>(test,new StatementCoverageCalculator(),5), 1);

            
            if (returnType == typeof(bool))
                return new CompositeFitnessFunction()
                    .AddEvaluation(new CodeCoverageFitnessFunction<bool>(test,new StatementCoverageCalculator(),5), 1);


            throw new Exception("Genetic algorithm builder could not dispatch for return type of function");
        }

        public IControlModel<T> CreateControlModel<T>(IFitnessFunction fitnessFunction,IPopulationMutator<T> populationMutator) where T : IComparable
        {
            return new SteadyStateControlModel<T>(populationMutator,fitnessFunction)
                .UseSelection(new TournamentSelection(fitnessFunction,_seed,3))
                .UseTerminationCriteria(new GenerationCountCriteria(_maxGenerations))
                .UseTerminationCriteria(new NoAverageImprovementCriteria(5))
                .UseTerminationCriteria(new DesiredFitnessForFitnessFunctionCriteria(typeof(StatementCoverageCalculator),100))
                .SetPopulationSize(_populationSize);
        }
    }
}