using System;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations
{
    public interface IRuntimeMeasurement
    {
        double GetRunTime(TimeSpan time);
        string GetScale();
    }
}