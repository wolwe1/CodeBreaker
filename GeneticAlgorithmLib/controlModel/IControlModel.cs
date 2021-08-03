using System.Collections.Generic;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel
{
    public interface IControlModel
    {
        bool TerminationCriteriaNotMet(int generationCount, EvaluationResults evaluationResults);
        List<string> SelectParents(EvaluationResults results);
        List<IPopulationMember> ApplyOperators(List<string> parents);
        int GetInitialPopulationSize();
    }
}