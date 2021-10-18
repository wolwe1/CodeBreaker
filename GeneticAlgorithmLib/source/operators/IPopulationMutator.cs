using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.operators
{
    public interface IPopulationMutator<T>
    {
        List<IPopulationMember<T>> ApplyOperators(List<string> parents);
        List<IPopulationMember<T>> ApplyOperators(List<MemberRecord<T>> parents);
    }
}