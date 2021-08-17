using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;

namespace GeneticAlgorithmLib.source.operators
{
    public interface IPopulationMutator<T>
    {
        List<IPopulationMember<T>> ApplyOperators(List<string> parents);
    }
}