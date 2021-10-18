using System;
using System.Collections.Generic;
using System.Text;
using AutomaticallyDefinedFunctions.structure.adf.helpers;
using CodeBreakerLib.connectors.ga;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.output;
using GeneticAlgorithmLib.source.statistics.runStatistics;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace CodeBreakerLib.connectors.output
{
    public class BestAdfOutputPrinter : IRunStatistic
    {
        private readonly BestPerformerMeasure _bestMemberMeasure;

        public BestAdfOutputPrinter()
        {
            _bestMemberMeasure = new BestPerformerMeasure();
        }
        public StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> evaluationResults)
        {
            var bestMemberInRun = _bestMemberMeasure.GetBestPerformer(evaluationResults);

            var memberOutput = bestMemberInRun.Member.GetResult();

            var strOutput = memberOutput.GetOutputString();

            return new StatisticOutput()
                .SetHeading("Best member Output")
                .SetRunValue(strOutput);
        }

        private string CreateOutputString<T>(List<Output<T>> outputs)
        {
            var strBuilder = new StringBuilder();

            for (var index = 0; index < outputs.Count; index++)
            {
                var output = outputs[index];
                strBuilder.AppendLine($"Output {index} : Failed - {output.Failed}");

                if (!output.Failed)
                    strBuilder.Append($" Value - {output.Value}");
            }

            return strBuilder.ToString();
        }
    }
}