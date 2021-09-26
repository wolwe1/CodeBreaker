using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.controlModel.selectionMethods;
using GeneticAlgorithmLib.source.controlModel.terminationCriteria;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.operators;
using GeneticAlgorithmLib.source.statistics;

namespace GeneticAlgorithmLib.source.controlModel
{
    public abstract class ControlModel<T> : IControlModel<T> where T : IComparable
    {
        private readonly List<ITerminationCriteria> _terminationCriteria;
        private ISelectionMethod _selectionMethod;
        private readonly IPopulationMutator<T> _populationMutator;
        private readonly IFitnessFunction _fitnessFunction;
        private int _popSize;

        protected ControlModel(IPopulationMutator<T> populationMutator, IFitnessFunction fitnessFunction)
        {
            _populationMutator = populationMutator;
            _fitnessFunction = fitnessFunction;
            _terminationCriteria = new List<ITerminationCriteria>();
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

        public GenerationRecord<T> Evaluate(List<IPopulationMember<T>> population)
        {
            var results = new GenerationRecord<T>();

            foreach (var member in population)
            {
                var fitness = _fitnessFunction.Evaluate(member);
                results.Add(member, fitness);
            }

            return results;
        }
        
        public ControlModel<T> UseSelection(ISelectionMethod newMethod)
        {
            _selectionMethod = newMethod;

            return this;
        }

        public ControlModel<T> UseTerminationCriteria(ITerminationCriteria newCriteria)
        {
            _terminationCriteria.Add(newCriteria);

            return this;
        }
        
        public ControlModel<T> SetPopulationSize(int popSize)
        {
            _popSize = popSize;

            return this;
        }
        
        public int GetInitialPopulationSize()
        {
            return _popSize;
        }
    }
}