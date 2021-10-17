using System.Collections.Generic;
using System.Linq;
using TestObjects.source.capture;

namespace CodeBreakerLib.coverage.calculators
{
    public class StatementCoverageCalculator : ICoverageCalculator
    {
        public double Calculate(CoverageResults coverageInfo)
        {
            var totalNodes = coverageInfo.NumberOfNodes;
            var nodesHit = coverageInfo.Coverages.Select(c => c.NodeNumber);
            var numberOfNodesHit = nodesHit.Distinct().Count();

            return ((double) numberOfNodesHit / totalNodes) * 100;
        }

        public double Calculate(List<CoverageResults> coverageValues)
        {
            var firstResult = coverageValues[0];

            for (var i = 1; i < coverageValues.Count; i++)
            {
                firstResult.Merge(coverageValues[i]);
            }

            return Calculate(firstResult);
        }
    }
}