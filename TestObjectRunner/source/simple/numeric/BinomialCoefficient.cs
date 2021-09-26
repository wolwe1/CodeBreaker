using TestObjects.source.capture;

namespace TestObjects.source.simple.numeric
{
    public class BinomialCoefficient
    {
        public CoverageResults Get(int n, int k)
        {
            var coverage = CoverageResults.SetupCoverage<double>("BinomialCoefficient","Get",6);
            
            coverage.AddStartNode(NodeType.If);
            // Base Cases
            if (k > n)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(0);
            }
            
            coverage.AddNode(2,NodeType.If);
            if (k == 0 || k == n)
            {
                coverage.AddNode(3,NodeType.Return);
                return coverage.SetResult(1);
            }
 
            // Recur
            coverage.AddNode(4,NodeType.Return);
            var nMinus1KMinus1 = Get(n - 1, k - 1);
            coverage.AddNode(5,NodeType.Return);
            var nMinus1K = Get(n - 1, k);

            return coverage.Merge(nMinus1KMinus1)
                .Merge(nMinus1K)
                .SetResult(nMinus1KMinus1.GetReturnValue() + nMinus1K.GetReturnValue());
        }
    }
}