using System.Collections.Generic;
using TestObjects.source.capture;

namespace CodeBreakerLib.coverage.calculators
{
    public interface ICoverageCalculator
    {
        double Calculate(CoverageResults coverageInfo);
        double Calculate(List<CoverageResults> coverageValues);
    }
}