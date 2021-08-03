using System.Collections.Generic;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.core
{
    public interface IGeneticAlgorithm
    {
        public List<IPopulationMember> CreateInitialPopulation();

        public IExecutionHistory Run();
    }
}