using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.controlModel
{
    public interface IControlModel<T>
    {
        bool TerminationCriteriaMet(int generationCount, GenerationRecord<T> generationRecord);
        List<string> SelectParents(GenerationRecord<T> results);
        List<IPopulationMember<T>> ApplyOperators(List<string> parents);
        int GetInitialPopulationSize();
    }
}