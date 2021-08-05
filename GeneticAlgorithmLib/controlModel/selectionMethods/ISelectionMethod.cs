using System.Collections.Generic;
using GeneticAlgorithmLib.core.population;
using GeneticAlgorithmLib.statistics;

namespace GeneticAlgorithmLib.controlModel.selectionMethods
{
    /// <summary>
    /// Represents the selection method being used to select parents for the next generation
    /// </summary>
    public interface ISelectionMethod
    {
        /// <summary>
        /// Selects the ID's of members to be used in the next population, using the chosen underlying method
        /// </summary>
        /// <param name="results">The result set produced by evaluating the population</param>
        /// <returns>List of member ID's selected for the next population</returns>
        List<string> Select<T>(EvaluationResults<T> results);
    }
}