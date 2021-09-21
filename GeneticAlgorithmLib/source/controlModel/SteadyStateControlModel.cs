using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.controlModel.selectionMethods;
using GeneticAlgorithmLib.source.controlModel.terminationCriteria;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.mockImplementations;
using GeneticAlgorithmLib.source.operators;
using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.controlModel
{
    public class SteadyStateControlModel<T> : IControlModel<T>
    {
        private readonly List<ITerminationCriteria> _terminationCriteria;
        private int _popSize;
        private IPopulationMutator<T> _populationMutator;
        private ISelectionMethod _selectionMethod;

        public SteadyStateControlModel(IPopulationMutator<T> mutator)
        {
            _terminationCriteria = new List<ITerminationCriteria>();
            _selectionMethod = new FitnessProportionateSelection(new ValueDistanceFitnessFunction().SetGoal(10));
            _populationMutator = mutator;
            _popSize = 0;
        }

        public bool TerminationCriteriaMet(int generationCount, GenerationRecord<T> generationRecord)
        {
            foreach (var criterion in _terminationCriteria)
                if (criterion.Met(generationCount, generationRecord))
                {
                    Console.WriteLine($"Run stopped due to: {criterion.GetReason()}");
                    return true;
                }

            return false;
        }

        public List<string> SelectParents(GenerationRecord<T> results)
        {
            return _selectionMethod.Select(results);
        }

        public List<IPopulationMember<T>> ApplyOperators(List<string> parents)
        {
            return _populationMutator.ApplyOperators(parents);
        }

        public int GetInitialPopulationSize()
        {
            return _popSize;
        }

        public SteadyStateControlModel<T> SetPopulationSize(int popSize)
        {
            _popSize = popSize;

            return this;
        }

        public SteadyStateControlModel<T> UseSelection(ISelectionMethod newMethod)
        {
            _selectionMethod = newMethod;

            return this;
        }

        public SteadyStateControlModel<T> UseMutator(IPopulationMutator<T> mutator)
        {
            _populationMutator = mutator;
            return this;
        }

        public SteadyStateControlModel<T> UseTerminationCriteria(ITerminationCriteria newCriteria)
        {
            _terminationCriteria.Add(newCriteria);

            return this;
        }
    }
}