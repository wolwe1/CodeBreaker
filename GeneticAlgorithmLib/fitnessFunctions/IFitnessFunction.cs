using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;
using GeneticAlgorithmLib.statistics.calculatedResults;

namespace GeneticAlgorithmLib.fitnessFunctions
{
    public interface IFitnessFunction
    {
        public double Evaluate<T>(IPopulationMember<T> member);
        CalculationResultSet GetNormalisedFitnessValues<T>(EvaluationResults<T> results);
    }
}