using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;

namespace GeneticAlgorithmLib.source.fitnessFunctions
{
    public class CompositeFitnessFunction : FitnessFunction
    {
        private readonly List<Tuple<IFitnessFunction, double>> _fitnessCriteria;

        public CompositeFitnessFunction()
        {
            _fitnessCriteria = new List<Tuple<IFitnessFunction, double>>();
        }

        public override Fitness Evaluate<T>(IPopulationMember<T> member)
        {
            var totalFitness = new Fitness();

            foreach (var criterion in _fitnessCriteria)
            {
                var multiplier = criterion.Item2;
                var rawFitness = criterion.Item1.Evaluate(member);

                totalFitness.AddEvaluation(rawFitness, multiplier); // += rawFitness * multiplier;
            }

            return totalFitness;
        }

        public CompositeFitnessFunction AddEvaluation(IFitnessFunction function, double multiplyer)
        {
            _fitnessCriteria.Add(new Tuple<IFitnessFunction, double>(function, multiplyer));

            return this;
        }
    }
}