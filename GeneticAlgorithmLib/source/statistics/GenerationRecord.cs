using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.statistics
{
    /// <summary>
    /// Holds a set of IPopulationMember ID and Result pairs
    /// </summary>
    public class GenerationRecord<T>
    {
        private readonly List<MemberRecord<T>> _records;
        public TimeSpan RunTime { get; set; }

        public GenerationRecord()
        {
            _records = new List<MemberRecord<T>>();
        }

        public void Add(IPopulationMember<T> member, double fitness)
        {
            var newResult = new MemberRecord<T>(member,fitness);
            
            _records.Add(newResult);
        }

        public CalculationResultSet GetFitnessValues()
        {
            return CalculationResultSet.Create(_records);
        }

        public double GetTotalFitness()
        {
            return GetFitnessValues().Accumulate((s, n) => s + n);
        }

        public int Size()
        {
            return _records.Count;
        }

    }
    
}