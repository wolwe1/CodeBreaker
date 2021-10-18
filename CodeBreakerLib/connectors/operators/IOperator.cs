using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics;

namespace CodeBreakerLib.connectors.operators
{
    public interface IOperator<T>
    {
        IEnumerable<string> CreateModifiedChildren(List<string> parents, IPopulationGenerator<T> populationGenerator);
        IEnumerable<IPopulationMember<T>> CreateModifiedChildren(List<MemberRecord<T>> parents, IPopulationGenerator<T> populationGenerator);
    }
}