using System;
using System.Linq;
using TestObjects.source.capture;

namespace CodeBreakerLib.coverage.calculators
{
    public class StatementCoverageCalculator : ICoverageCalculator
    {
        public double Calculate<T>(CoverageResults<T> coverageInfo) where T : IComparable
        {
            var totalNodes = coverageInfo.NumberOfNodes;
            var nodesHit = coverageInfo.Coverages.Select(c => c.NodeNumber);
            var numberOfNodesHit = nodesHit.Distinct().Count();

            return ((double) numberOfNodesHit / totalNodes) * 100;
        }
    }
}