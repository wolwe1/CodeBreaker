using System.Collections.Generic;

namespace GeneticAlgorithmLib.source.statistics.history
{
    public interface ITypelessExecutionHistory
    {
        /// <summary>
        ///     Initialised a new run in the execution history
        /// </summary>
        void NewRun();

        /// <summary>
        ///     Initialise a new generation for the current run
        /// </summary>
        void NewGeneration();
        void Summarise();

        List<string> GetBestPerformerIds();

        int RunNumber { get; set; }
    }
    public interface IExecutionHistory<T> : ITypelessExecutionHistory
    {
        /// <summary>
        ///     Adds the information about a generation to the current run
        /// </summary>
        /// <param name="generationRecord">The evaluation results of the current run</param>
        void CloseGeneration(GenerationRecord<T> generationRecord);
    }
}