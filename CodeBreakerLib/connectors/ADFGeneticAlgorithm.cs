using System;
using CodeBreakerLib.fitnessFunctions;
using GeneticAlgorithmLib.source.controlModel;
using GeneticAlgorithmLib.source.core;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics.history;

namespace CodeBreakerLib.connectors
{
    public class ADFGeneticAlgorithm<T> : GeneticAlgorithm<T> where T : IComparable
    {
        public ADFGeneticAlgorithm(IPopulationGenerator<T> populationGenerator, IControlModel<T> controlModel, ADFFitnessFunction fitnessFunction, IExecutionHistory<T> history) : base(populationGenerator, controlModel, fitnessFunction, history)
        {
            
        }
    }
}