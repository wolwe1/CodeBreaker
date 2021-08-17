using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;

namespace GeneticAlgorithmLib.source.fitnessFunctions
{
    public class CompositeFitnessFunction : FitnessFunction
    {
        private List<Tuple<IFitnessFunction, double>> _fitnessCriteria;

        public CompositeFitnessFunction()
        {
            _fitnessCriteria = new List<Tuple<IFitnessFunction, double>>();
        }

        public override double Evaluate<T>(IPopulationMember<T> member)
        {
            double evaluation = 0;

            foreach (var criterion in _fitnessCriteria)
            {
                var multiplier = criterion.Item2;
                var rawFitness = criterion.Item1.Evaluate(member);

                evaluation += rawFitness * multiplier;
            }

            return evaluation;
        }

        public CompositeFitnessFunction AddEvaluation(IFitnessFunction function, double multiplyer)
        {
            _fitnessCriteria.Add(new Tuple<IFitnessFunction, double>(function,multiplyer));

            return this;
        }
    }
}