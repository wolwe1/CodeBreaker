using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.operators;

namespace GeneticAlgorithmLib.source.mockImplementations
{
    public class NoOpMutator<T> : IPopulationMutator<T>
    {
        private readonly IPopulationGenerator<T> _generator;

        public NoOpMutator(IPopulationGenerator<T> generator)
        {
            _generator = generator;
        }

        public List<IPopulationMember<T>> ApplyOperators(List<string> parents)
        {
            var newMembers = new List<IPopulationMember<T>>();
            foreach (var chromosome in parents)
            {
                var newMember = _generator.GenerateFromId(chromosome);
                newMembers.Add(newMember);
            }

            Console.WriteLine("No OP mutator in use, returning unmodified members");
            return newMembers;
        }
    }
}