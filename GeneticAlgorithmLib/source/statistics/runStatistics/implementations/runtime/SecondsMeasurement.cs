using System;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations
{
    public class SecondsMeasurement : IRuntimeMeasurement
    {
        public double GetRunTime(TimeSpan time)
        {
            return time.Seconds;
        }

        public string GetScale()
        {
            return "seconds";
        }
    }
}