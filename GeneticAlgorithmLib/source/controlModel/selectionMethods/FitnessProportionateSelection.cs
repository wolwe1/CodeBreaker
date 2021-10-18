using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.fitnessFunctions;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.controlModel.selectionMethods
{
    public class FitnessProportionateSelection : SelectionMethod
    {
        public FitnessProportionateSelection(IFitnessFunction fitnessFunction) : base(fitnessFunction)
        {
        }

        /// <summary>
        /// Uses fitness proportionate selection to select parents. *May reduce parents
        /// </summary>
        /// <param name="results"></param>
        /// <param name="maxParentsToProduce"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override List<string> SelectReturningIds<T>(GenerationRecord<T> results, int maxParentsToProduce)
        {
            var numOccurrencesInNextPop = GetNumOccurrencesInNextPop(results);

            var selectedMembers = new List<string>();

            var candidates = numOccurrencesInNextPop.Where(x => x.GetResult() >= 1).ToList();

            foreach (var candidate in candidates) selectedMembers.AddRange(RepeatMemberIdBasedOnOccurence(candidate));

            if (selectedMembers.Count < maxParentsToProduce)
                selectedMembers.AddRange(
                    FillSelectedMembers(results, maxParentsToProduce - selectedMembers.Count)
                );
            
            return selectedMembers;
        }
        public override List<MemberRecord<T>> Select<T>(GenerationRecord<T> results,int maxParentsToProduce)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> FillSelectedMembers<T>(GenerationRecord<T> results, int numberOfClonesToMake)
        {
            var bestPerformer = FitnessFunction.GetBest(results);

            var clones = Enumerable.Repeat(bestPerformer.GetMemberId(), numberOfClonesToMake);

            return clones;
        }

        private IEnumerable<string> RepeatMemberIdBasedOnOccurence(CalculationResult candidate)
        {
            var selectedMembers = new List<string>();

            var numOccurencesInNextPop = candidate.GetResult();

            for (var i = 0; i < numOccurencesInNextPop; i++) selectedMembers.Add(candidate.GetMemberId());

            return selectedMembers;
        }

        private List<CalculationResult> GetNumOccurrencesInNextPop<T>(GenerationRecord<T> results)
        {
            var popSize = results.Size();
            var normalisedFitnessValues = FitnessFunction.GetNormalisedFitnessValues(results);

            var numOccurrencesInNextPop = normalisedFitnessValues
                .Map(x => Convert.ToInt32(x * popSize))
                .ToList();
            return numOccurrencesInNextPop;
        }
    }
}