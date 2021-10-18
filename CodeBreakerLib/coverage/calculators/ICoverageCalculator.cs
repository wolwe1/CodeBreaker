using System.Collections.Generic;
using TestObjects.source.capture;

namespace CodeBreakerLib.coverage.calculators
{
    public interface ICoverageCalculator
    {
        double Calculate(CoverageResultWrapper coverageInfo);
        double Calculate(List<CoverageResultWrapper> coverageValues);
    }
}