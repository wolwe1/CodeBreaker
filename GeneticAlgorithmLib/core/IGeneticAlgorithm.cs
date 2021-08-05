using System.Collections.Generic;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.core
{
    public interface IGeneticAlgorithm<T>
    {
        public List<IPopulationMember<T>> CreateInitialPopulation();

        public IExecutionHistory<T> Run();
    }
}