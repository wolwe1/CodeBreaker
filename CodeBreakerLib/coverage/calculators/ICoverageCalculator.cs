using System;
using TestObjects.source.capture;

namespace CodeBreakerLib.coverage.calculators
{
    public interface ICoverageCalculator
    {
        double Calculate<T>(CoverageResults<T> coverageInfo) where T : IComparable;
    }
}