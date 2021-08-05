using System.Collections.Generic;
using GeneticAlgorithmLib.controlModel;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.fitnessFunctions;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.core
{
    public class GeneticAlgorithm<T> : IGeneticAlgorithm<T>
    {
        private readonly IPopulationGenerator<T> _populationGenerator;
        private readonly IControlModel<T> _controlModel;
        private readonly IFitnessFunction _fitnessFunction;
        private readonly IExecutionHistory<T> _history;

        protected GeneticAlgorithm(IPopulationGenerator<T> populationGenerator, IControlModel<T> controlModel, IFitnessFunction fitnessFunction, IExecutionHistory<T> history)
        {
            _populationGenerator = populationGenerator;
            _controlModel = controlModel;
            _fitnessFunction = fitnessFunction;
            _history = history;
        }

        public List<IPopulationMember<T>> CreateInitialPopulation()
        {
            var populationMembers = new List<IPopulationMember<T>>();
            
            var initialPopulationSize = _controlModel.GetInitialPopulationSize();

            for (var i = 0; i < initialPopulationSize; i++)
            {
                IPopulationMember<T> newMember = _populationGenerator.GenerateNewMember();
                populationMembers.Add(newMember);
            }
            return populationMembers;
        }

        public IExecutionHistory<T> Run()
        {
            _history.NewRun();
            
            var generationCount = 0;
            var population = CreateInitialPopulation();

            EvaluationResults<T> results = null;
            while (_controlModel.TerminationCriteriaNotMet(generationCount,results))
            {
                results = Evaluate(population);
                _history.AddGenerationHistory(results);
                
                var parents = _controlModel.SelectParents(results);
                
                population = _controlModel.ApplyOperators(parents);
                
            }

            return _history;
        }

        private EvaluationResults<T> Evaluate(List<IPopulationMember<T>> population)
        {
            var results = new EvaluationResults<T>();
            
            foreach (var member in population)
            {
                var fitness = _fitnessFunction.Evaluate(member);
                results.Add(member, fitness);
            }

            return results;
        }
        

    }
    
}