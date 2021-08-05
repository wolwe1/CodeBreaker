using System;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.fitnessFunctions;

namespace GeneticAlgorithm
{
    public class OurSimpleFitnessFunction : FitnessFunction
    {
        public override double Evaluate<T>(IPopulationMember<T> member)
        {
            var memberResult = (double)(object)member.GetResult();

            return Math.Abs(memberResult - 10);
        }
    }
}