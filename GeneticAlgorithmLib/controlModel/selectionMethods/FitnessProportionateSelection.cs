using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.statistics;
using GeneticAlgorithmLib.statistics.calculatedResults;

namespace GeneticAlgorithmLib.controlModel.selectionMethods
{
    public class FitnessProportionateSelection : ISelectionMethod
    {
        public List<string> Select(EvaluationResults results)
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
        
        private static List<CalculationResult> GetNumOccurrencesInNextPop(EvaluationResults results)
        {
            var popSize = results.Size();
            var normalisedFitnessValues = results.GetNormalisedFitnessValues();

            var numOccurrencesInNextPop = normalisedFitnessValues
                .Map(x => Convert.ToInt32(x * popSize))
                .ToList();
            return numOccurrencesInNextPop;
        }
    }
}