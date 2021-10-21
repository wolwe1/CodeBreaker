using System;
using AutomaticallyDefinedFunctions.generators.adf;
using CodeBreakerLib.connectors;
using CodeBreakerLib.connectors.ga.state;
using CodeBreakerLib.connectors.operators;
using CodeBreakerLib.connectors.operators.implementation;
using CodeBreakerLib.connectors.output;
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
        
        //public GeneticAlgorithmBuilder(): this(0,65,4,10,50,100) { }
        public GeneticAlgorithmBuilder(): this(0,65,4,10,15,25) { }
        
        public GeneticAlgorithm<T> Build<T>(Test<object> test) where T : IComparable
        {
            var returnType = test.GetUnderlyingReturnType();

            if (returnType == typeof(string))
                return BuildGa<T, string>(test);

            if (returnType == typeof(double))
                return BuildGa<T, double>(test);

            if (returnType == typeof(bool))
                return BuildGa<T, bool>(test);

            throw new Exception("Could not create GA based on test response");

        }

        private GeneticAlgorithm<T> BuildGa<T,TU>(Test<object> test) where T : IComparable where TU : IComparable
        {
            var populationGenerator = CreatePopulationGenerator<T,TU>(test);
            var populationMutator = CreatePopulationMutator<T, TU>(populationGenerator);
            var fitnessFunction = CreateFitnessFunction<TU>(test);
            var controlModel = CreateControlModel(fitnessFunction,populationMutator);

            return CreateGa(populationGenerator, controlModel);
        }

        private IPopulationMutator<T> CreatePopulationMutator<T, TU>(IPopulationGenerator<T> populationGenerator) where T : IComparable where TU : IComparable
        {
            return new AdfMutator<T>(populationGenerator)
                .UseOperator(new ReproductiveOperator<T>(30))
                .UseOperator(new MutationOperator<T,TU>(40, 5))
                .UseOperator(new CrossoverOperator<T,TU>(30));
        }

        public IPopulationGenerator<T> CreatePopulationGenerator<T,TU>(Test<object> test) where T : IComparable where TU : IComparable
        {
            var settings = new StateAdfSettings<T,TU>( _maxFunctionDepth, _maxMainDepth,test.GetArguments().Count,_terminalChance);

            return new StateAdfPopulationGenerator<T,TU>(_seed,settings);
        }
        public static GeneticAlgorithm<T> CreateGa<T>(IPopulationGenerator<T> populationGenerator,
            IControlModel<T> controlModel) where T : IComparable
        {
            IExecutionHistory<T> history = new ExecutionOutput<T>().OutputToFile(new CsvFileOutputPrinter());
            
            return new GeneticAlgorithm<T>(populationGenerator,controlModel,history).Print();
        }

        public IFitnessFunction CreateFitnessFunction<TU>(Test<object> test) where TU : IComparable
        {
            return new CompositeFitnessFunction()
                .AddEvaluation(new CodeCoverageFitnessFunction<TU>(test,new StatementCoverageCalculator(),5), 1);
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