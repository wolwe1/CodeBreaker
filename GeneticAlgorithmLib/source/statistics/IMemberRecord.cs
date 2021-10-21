using GeneticAlgorithmLib.source.fitnessFunctions;

namespace GeneticAlgorithmLib.source.statistics
{
    public interface IMemberRecord
    {
        double GetFitnessValue();

        string GetMemberId();

        Fitness GetFitness();
    }
}