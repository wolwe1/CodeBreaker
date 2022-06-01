using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics.history;

namespace GeneticAlgorithmLib.source.core
{
    public interface IGeneticAlgorithm<T>
    {
        public List<IPopulationMember<T>> CreateInitialPopulation();

        public IExecutionHistory<T> Run();
    }
}