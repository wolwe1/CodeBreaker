using System;
using GeneticAlgorithmLib.controlModel;
using GeneticAlgorithmLib.controlModel.selectionMethods;
using GeneticAlgorithmLib.controlModel.terminationCriteria;
using GeneticAlgorithmLib.fitnessFunctions;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //IPopulationGenerator populationGenerator = new IPopulationGenerator(); 

            CompositeFitnessFunction fitnessFunction =
                new CompositeFitnessFunction().AddEvaluation(new OurSimpleFitnessFunction(), 1);
            
            IControlModel<double> controlModel = new SteadyStateControlModel<double>()
                .UseSelection(new FitnessProportionateSelection(fitnessFunction))
                .UseTerminationCriteria(new GenerationCountCriteria(1));
            
            IExecutionHistory<double> history = new BasicExecutionHistory<double>();
            //var geneticAlgorithm = new GeneticAlgorithm();
        }
    }
}