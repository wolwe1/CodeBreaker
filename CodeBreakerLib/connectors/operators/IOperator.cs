using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.operators
{
    public interface IOperator<T>
    {
        IEnumerable<string> CreateModifiedChildren(List<string> parents, IPopulationGenerator<T> populationGenerator);
    }
}