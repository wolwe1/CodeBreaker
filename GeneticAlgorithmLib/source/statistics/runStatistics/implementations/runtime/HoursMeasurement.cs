using System;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations.runtime
{
    public class HoursMeasurement : IRuntimeMeasurement
    {
        public double GetRunTime(TimeSpan time)
        {
            return time.Hours;
        }

        public string GetScale()
        {
            return "hours";
        }
    }
}