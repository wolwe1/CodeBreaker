using System;
using GeneticAlgorithmLib.source.controlModel;
using GeneticAlgorithmLib.source.core;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics.history;

namespace CodeBreakerLib.connectors.ga
{
    public class ADFGeneticAlgorithm<T> : GeneticAlgorithm<T> where T : IComparable
    {
        public ADFGeneticAlgorithm(IPopulationGenerator<T> populationGenerator, IControlModel<T> controlModel, IExecutionHistory<T> history) : base(populationGenerator, controlModel, history)
        {
            
        }
    }
}