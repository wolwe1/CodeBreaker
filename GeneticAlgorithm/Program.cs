using System;
using GeneticAlgorithmLib.controlModel;
using GeneticAlgorithmLib.controlModel.selectionMethods;
using GeneticAlgorithmLib.controlModel.terminationCriteria;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //IPopulationGenerator populationGenerator = new IPopulationGenerator(); 
            IControlModel controlModel = new SteadyStateControlModel()
                .UseSelection(new FitnessProportionateSelection())
                .UseTerminationCriteria(new GenerationCountCriteria(1));
            //IFitnessFunction fitnessFunction, 
            IExecutionHistory history = new BasicExecutionHistory();
            //var geneticAlgorithm = new GeneticAlgorithm();
        }
    }
}