using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics.calculatedResults;

namespace GeneticAlgorithmLib.statistics
{
    /// <summary>
    /// Holds a set of IPopulationMember ID and Result pairs
    /// </summary>
    public class EvaluationResults
    {
        private readonly Dictionary<string,Result> _record;

        public EvaluationResults()
        {
            _record = new Dictionary<string, Result>();
        }

        public void Add(IPopulationMember member, double fitness)
        {
            var memberId = member.GetId();
            var newResult = new Result(member,fitness);

            if (!_record.ContainsKey(memberId))
                _record.Add(memberId,newResult);
            else
                _record[memberId].AddDuplicate();
            
        }

        public CalculationResultSet GetFitnessValues()
        {
            var values = _record.Values.ToList();
            
            return new CalculationResultSet(values);
        }
        
        public CalculationResultSet GetNormalisedFitnessValues()
        {
            var popSize = Size();
            var fitnessValues = GetFitnessValues();
            var totalFitness = GetTotalFitness();
            
            return fitnessValues.Map(x => x / totalFitness);
        }

        public double GetTotalFitness()
        {
            return GetFitnessValues().Accumulate((s, n) => s + n);
        }

        public int Size()
        {
            return _record.Count;
        }
    }
    
}