using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GeneticAlgorithmLib.source.statistics.calculatedResults;
using GeneticAlgorithmLib.source.statistics.history;
using GeneticAlgorithmLib.source.statistics.output;
using GeneticAlgorithmLib.source.statistics.output.implementations;

namespace CodeBreakerLib.connectors.output
{
    public class CsvFileOutputPrinter : FileOutputPrinter
    {
        public override void Print<T>(List<StatisticOutput> runStatistics, RunRecord<T> runRecord)
        {

            WriteToCsv(runStatistics,runRecord);
            
            base.Print(runStatistics,runRecord);
        }

        private void WriteToCsv<T>(List<StatisticOutput> runStatistics, RunRecord<T> runRecord)
        {
            foreach (var runStatistic in runStatistics)
            {
                var runNumber = runRecord.GetRunNumber();
                var statisticType = runStatistic.GetHeading();
                var values = runStatistic.GetGenerationValues();
                if (values != null)
                {
                    
                    var calculatedValues = values.ToList().Select(c => c.GetResult()).ToList();
                    CreateCsvFile(statisticType,calculatedValues,runRecord);
                }
            }
        }

        //Creates timeseries data for the generations
        private void CreateCsvFile<T>(string statisticType, List<double> values, RunRecord<T> runRecord)
        {
            var numberOfGenerations = values.Count;
            var generationRange = Enumerable.Range(1, numberOfGenerations);

            var fileName = $"{runRecord.AdditionalRunInfo}_Run{runRecord.GetRunNumber()}_{statisticType}";
            fileName = Path.Combine("csv", fileName);
            
            var header = string.Join(",", generationRange);
            
            //Replace commas with full stops
            var convertedValues = values.Select(v => v.ToString().Replace(",", "."));
            var csvValues = string.Join(",", convertedValues);

            var data = header + "\n" + csvValues;


            TryWriteOutputToFile(data, fileName);

        }
    }
}