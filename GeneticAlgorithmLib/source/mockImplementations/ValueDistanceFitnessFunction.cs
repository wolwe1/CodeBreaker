using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.mockImplementations
{
    public class ValueDistanceFitnessFunction : FitnessFunction
    {
        private double _goal;

        public override Fitness Evaluate<T>(IPopulationMember<T> member)
        {
            var distanceToTarget = GetRawFitness(member);

            return new Fitness(typeof(ValueDistanceFitnessFunction),Math.Abs(_goal - distanceToTarget));
        }

        public override MemberRecord<T> GetBest<T>(IEnumerable<MemberRecord<T>> chosenMembers)
        {
            var bestFitness = chosenMembers.ToList().Min(GetRawFitness);

            return chosenMembers.FirstOrDefault(m => m.GetFitness().GetFitness() == bestFitness);
        }

        public ValueDistanceFitnessFunction SetGoal(double goal)
        {
            _goal = goal;
            return this;
        }

        private double GetRawFitness<T>(MemberRecord<T> member)
        {
            var memberResult = member.GetFitness().GetFitness();

            return Math.Abs(_goal - memberResult);
        }
        private double GetRawFitness<T>(IPopulationMember<T> member)
        {
            var memberResult = (double) (object) member.GetResult().GetOutputValues().ElementAt(0);

            return Math.Abs(_goal - memberResult);
        }
    }
}