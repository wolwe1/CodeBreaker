using System.Collections.Generic;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel
{
    public interface IControlModel<T>
    {
        bool TerminationCriteriaNotMet(int generationCount, EvaluationResults<T> evaluationResults);
        List<string> SelectParents(EvaluationResults<T> results);
        List<IPopulationMember<T>> ApplyOperators(List<string> parents);
        int GetInitialPopulationSize();
    }
}