using System.Collections.Generic;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.statistics
{
    public interface IGenerationRecord
    {
        CalculationResultSet GetFitnessValues();

        double GetTotalFitness();

        int Size();
        List<IMemberRecord> GetMembers();
    }
}