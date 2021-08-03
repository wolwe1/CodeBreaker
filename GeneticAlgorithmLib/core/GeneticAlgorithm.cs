using System.Collections.Generic;
using GeneticAlgorithmLib.controlModel;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.core
{
    public class GeneticAlgorithm : IGeneticAlgorithm
    {
        private readonly IPopulationGenerator _populationGenerator;
        private readonly IControlModel _controlModel;
        private readonly IFitnessFunction _fitnessFunction;
        private readonly IExecutionHistory _history;

        protected GeneticAlgorithm(IPopulationGenerator populationGenerator, IControlModel controlModel, IFitnessFunction fitnessFunction, IExecutionHistory history)
        {
            _populationGenerator = populationGenerator;
            _controlModel = controlModel;
            _fitnessFunction = fitnessFunction;
            _history = history;
        }

        public List<IPopulationMember> CreateInitialPopulation()
        {
            var populationMembers = new List<IPopulationMember>();
            
            var initialPopulationSize = _controlModel.GetInitialPopulationSize();

            for (var i = 0; i < initialPopulationSize; i++)
            {
                IPopulationMember newMember = _populationGenerator.GenerateNewMember();
                populationMembers.Add(newMember);
            }
            return populationMembers;
        }

        public IExecutionHistory Run()
        {
            _history.NewRun();
            
            var generationCount = 0;
            var population = CreateInitialPopulation();

            EvaluationResults results = null;
            while (_controlModel.TerminationCriteriaNotMet(generationCount,results))
            {
                results = Evaluate(population);
                _history.AddGenerationHistory(results);
                
                var parents = _controlModel.SelectParents(results);
                
                population = _controlModel.ApplyOperators(parents);
                
            }

            return _history;
        }

        private EvaluationResults Evaluate(List<IPopulationMember> population)
        {
            var results = new EvaluationResults();
            
            foreach (var member in population)
            {
                var fitness = _fitnessFunction.Evaluate(member);
                results.Add(member, fitness);
            }

            return results;
        }
        

    }
    
}