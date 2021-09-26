using System;
using TestObjects.source.capture;

namespace CodeBreakerLib.coverage.calculators
{
    public interface ICoverageCalculator
    {
        double Calculate(CoverageResults coverageInfo);
    }
}