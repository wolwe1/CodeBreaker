using System.Collections.Generic;
using GeneticAlgorithmLib.source.controlModel;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.history;

namespace GeneticAlgorithmLib.source.core
{
    public class GeneticAlgorithm<T> : IGeneticAlgorithm<T>
    {
        private readonly IPopulationGenerator<T> _populationGenerator;
        private readonly IControlModel<T> _controlModel;
        private readonly IFitnessFunction _fitnessFunction;
        private readonly IExecutionHistory<T> _history;

        public GeneticAlgorithm(IPopulationGenerator<T> populationGenerator, IControlModel<T> controlModel, IFitnessFunction fitnessFunction, IExecutionHistory<T> history)
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

            GenerationRecord<T> results = null;
            while (!_controlModel.TerminationCriteriaMet(generationCount++,results))
            {
                _history.NewGeneration();
                
                results = Evaluate(population);

                var parents = _controlModel.SelectParents(results);
                
                population = _controlModel.ApplyOperators(parents);
                
                _history.CloseGeneration(results);
            }

            return _history;
        }

        private GenerationRecord<T> Evaluate(List<IPopulationMember<T>> population)
        {
            var results = new GenerationRecord<T>();
            
            foreach (var member in population)
            {
                var fitness = _fitnessFunction.Evaluate(member);
                results.Add(member, fitness);
            }

            return results;
        }
        

    }
    
}