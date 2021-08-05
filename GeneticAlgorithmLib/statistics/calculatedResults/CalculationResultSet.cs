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
        
       
        public CalculationResultSet(IEnumerable<CalculationResult> results)
        {
            _calculatedResults = results;
        }

        /// <summary>
        /// Create a calculated result set from a list of <see cref="Result"/>
        /// </summary>
        public static CalculationResultSet Create<T>(IEnumerable<Result<T>> results)
        {
            var calculationResults = results.Select( x => new CalculationResult(x.GetFitness(),x.GetMemberId()));
            return new CalculationResultSet(calculationResults);
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