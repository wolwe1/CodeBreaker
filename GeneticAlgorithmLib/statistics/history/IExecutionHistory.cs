namespace GeneticAlgorithmLib.statistics
{
    public interface IExecutionHistory
    {
        /// <summary>
        /// Initialised a new run in the execution history
        /// </summary>
        void NewRun();
        
        /// <summary>
        /// Adds the information about a generation to the current run
        /// </summary>
        /// <param name="evaluationResults">The evaluation results of the current run</param>
        void AddGenerationHistory(EvaluationResults evaluationResults);
    }
}