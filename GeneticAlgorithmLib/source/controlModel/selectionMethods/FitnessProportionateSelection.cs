using System;
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

        public override List<string> Select<T>(GenerationRecord<T> results)
        {
            var numOccurrencesInNextPop = GetNumOccurrencesInNextPop(results);

            var selectedMembers = new List<string>();

            var candidates = numOccurrencesInNextPop.Where(x => x.GetResult() >= 1).ToList();
            
            foreach (var candidate in candidates)
            {
                selectedMembers.AddRange(RepeatMemberIdBasedOnOccurence(candidate));
            }

            return selectedMembers;
        }

        private IEnumerable<string> RepeatMemberIdBasedOnOccurence(CalculationResult candidate)
        {
            var selectedMembers = new List<string>();
            
            var numOccurencesInNextPop = candidate.GetResult();

            for (var i = 0; i < numOccurencesInNextPop; i++)
            {
                selectedMembers.Add(candidate.GetMemberId());
            }

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