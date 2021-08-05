﻿using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.controlModel.selectionMethods;
using GeneticAlgorithmLib.controlModel.terminationCriteria;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel
{
    public class SteadyStateControlModel<T> : IControlModel<T>
    {
        private readonly List<ITerminationCriteria> _terminationCriteria;
        private ISelectionMethod _selectionMethod;

        public SteadyStateControlModel()
        {
            _terminationCriteria = new List<ITerminationCriteria>();
            _selectionMethod = new FitnessProportionateSelection(new );
        }

        public bool TerminationCriteriaNotMet(int generationCount, EvaluationResults<T> evaluationResults)
        {
            foreach (var criterion in _terminationCriteria)
            {
                if (criterion.Met(generationCount, evaluationResults))
                    return true;
            }

            return false;
        }

        public List<string> SelectParents(EvaluationResults<T> results)
        {
            return _selectionMethod.Select(results);
        }

        public List<IPopulationMember<T>> ApplyOperators(List<string> parents)
        {
            throw new NotImplementedException();
        }

        public int GetInitialPopulationSize()
        {
            throw new NotImplementedException();
        }

        public List<IPopulationMember<T>> ApplyOperators(List<IPopulationMember<T>> parents)
        {
            throw new System.NotImplementedException();
        }

        public SteadyStateControlModel<T> UseSelection(ISelectionMethod newMethod)
        {
            _selectionMethod = newMethod;

            return this;
        }
        
        public SteadyStateControlModel<T> UseTerminationCriteria(ITerminationCriteria newCriteria)
        {
            _terminationCriteria.Add(newCriteria);

            return this;
        }
    }
}