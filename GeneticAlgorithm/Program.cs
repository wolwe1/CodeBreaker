using GeneticAlgorithmLib.source.controlModel;
using GeneticAlgorithmLib.source.controlModel.selectionMethods;
using GeneticAlgorithmLib.source.controlModel.terminationCriteria;
using GeneticAlgorithmLib.source.core;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.mockImplementations;
using GeneticAlgorithmLib.source.operators;
using GeneticAlgorithmLib.source.statistics.history;

namespace GeneticAlgorithm
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IPopulationGenerator<double> populationGenerator = new RandomNumberGenerator();
            IPopulationMutator<double> populationMutator = new NoOpMutator<double>(populationGenerator);

            var fitnessFunction =
                new CompositeFitnessFunction()
                    .AddEvaluation(new ValueDistanceFitnessFunction().SetGoal(10), 1);

            IControlModel<double> controlModel = new SteadyStateControlModel<double>(populationMutator,fitnessFunction)
                .UseSelection(new TournamentSelection(fitnessFunction,0,3))
                .UseTerminationCriteria(new GenerationCountCriteria(20))
                .UseTerminationCriteria(new NoAverageImprovementCriteria(5))
                .UseTerminationCriteria(new DesiredFitnessForFitnessFunctionCriteria(typeof(ValueDistanceFitnessFunction), 10))
                .SetPopulationSize(10);

            IExecutionHistory<double> history = new BasicExecutionHistory<double>();
            var geneticAlgorithm =
                new GeneticAlgorithm<double>(populationGenerator, controlModel, history);

            var runHistory = geneticAlgorithm.Run();

            runHistory.Summarise();
        }
    }
}