using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmLib.statistics.calculatedResults
{

    /// <summary>
    /// Wrapper for Calculation results, allows for easy conversion from Result to Calculation result
    /// </summary>
    public class CalculationResultSet
    {
        private readonly IEnumerable<CalculationResult> _calculatedResults;
        
        /// <summary>
        /// Create a calculated result set from a list of <see cref="Result"/>
        /// </summary>
        public CalculationResultSet(IEnumerable<Result> results)
        {
            _calculatedResults = results.Select( x => new CalculationResult(x.GetFitness(),x.GetMemberId()));
        }
        
        public CalculationResultSet(IEnumerable<CalculationResult> calculatedResults)
        {
            _calculatedResults = calculatedResults;
        }

        /// <summary>
        /// Create a list of the internal <see cref="CalculationResult"/>s
        /// </summary>
        public List<CalculationResult> ToList()
        {
            return _calculatedResults.ToList();
        }
        /// <summary>
        /// Map current result set to a new one after applying the function
        /// </summary>
        /// <param name="function">Function to map the <see cref="CalculationResult"/> values</param>
        /// <returns>A new <see cref="CalculationResultSet"/> with mapped values</returns>
        public CalculationResultSet Map(CalculationResultExtensions.MapFunc function)
        {
            return new(_calculatedResults.Map(function));
        }
        
        /// <summary>
        /// Get accumulated value of underlying <see cref="CalculationResult"/> set
        /// </summary>
        /// <param name="function">Function to accumulate the <see cref="CalculationResult"/> values</param>
        /// <returns>The accumulated values</returns>
        public double Accumulate(CalculationResultExtensions.AccFunc function)
        {
            return _calculatedResults.Accumulator(function); 
        }
        
        
    }

    
}