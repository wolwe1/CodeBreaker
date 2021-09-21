using System;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;

namespace GeneticAlgorithmLib.source.mockImplementations
{
    public class ValueDistanceFitnessFunction : FitnessFunction
    {
        private double _goal;

        public override Fitness Evaluate<T>(IPopulationMember<T> member)
        {
            var memberResult = (double) (object) member.GetResult();

            var distanceToTarget = Math.Abs(_goal - memberResult);

            return new Fitness(typeof(ValueDistanceFitnessFunction),Math.Abs(_goal - distanceToTarget));
        }

        public ValueDistanceFitnessFunction SetGoal(double goal)
        {
            _goal = goal;
            return this;
        }
    }
}