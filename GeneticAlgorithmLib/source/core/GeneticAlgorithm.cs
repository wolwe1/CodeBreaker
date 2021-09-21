using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.controlModel;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.history;

namespace GeneticAlgorithmLib.source.core
{
    public class GeneticAlgorithm<T> : IGeneticAlgorithm<T> where T : IComparable
    {
        protected readonly IControlModel<T> ControlModel;
        protected readonly IFitnessFunction FitnessFunction;
        protected readonly IExecutionHistory<T> History;
        protected readonly IPopulationGenerator<T> PopulationGenerator;

        public GeneticAlgorithm(IPopulationGenerator<T> populationGenerator, IControlModel<T> controlModel,
            IFitnessFunction fitnessFunction, IExecutionHistory<T> history)
        {
            PopulationGenerator = populationGenerator;
            ControlModel = controlModel;
            FitnessFunction = fitnessFunction;
            History = history;
        }

        public List<IPopulationMember<T>> CreateInitialPopulation()
        {
            var populationMembers = new List<IPopulationMember<T>>();

            var initialPopulationSize = ControlModel.GetInitialPopulationSize();

            for (var i = 0; i < initialPopulationSize; i++)
            {
                var newMember = PopulationGenerator.GenerateNewMember();
                populationMembers.Add(newMember);
            }

            return populationMembers;
        }

        public IExecutionHistory<T> Run()
        {
            History.NewRun();

            var generationCount = 0;
            var population = CreateInitialPopulation();

            GenerationRecord<T> results = null;
            while (!ControlModel.TerminationCriteriaMet(generationCount++, results))
            {
                History.NewGeneration();

                results = Evaluate(population);

                var parents = ControlModel.SelectParents(results);

                population = ControlModel.ApplyOperators(parents);

                History.CloseGeneration(results);
            }

            return History;
        }

        protected GenerationRecord<T> Evaluate(List<IPopulationMember<T>> population)
        {
            var results = new GenerationRecord<T>();

            foreach (var member in population)
            {
                var fitness = FitnessFunction.Evaluate(member);
                results.Add(member, fitness);
            }

            return results;
        }
    }
}