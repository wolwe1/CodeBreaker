using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;
using GeneticAlgorithmLib.statistics.calculatedResults;

namespace GeneticAlgorithmLib.fitnessFunctions
{
    public abstract class FitnessFunction : IFitnessFunction
    {
        public abstract double Evaluate<T>(IPopulationMember<T> member);

        public CalculationResultSet GetNormalisedFitnessValues<T>(EvaluationResults<T> results)
        {
            var fitnessValues = results.GetFitnessValues();
            var totalFitness = results.GetTotalFitness();
            
            return fitnessValues.Map(x => x / totalFitness);
        }
    }
}