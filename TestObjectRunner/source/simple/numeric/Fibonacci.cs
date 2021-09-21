using TestObjects.source.capture;

namespace TestObjects.source.simple.numeric
{
    public class Fibonacci
    {
        public CoverageResults<double> Get(int n)
        {
            var coverage = new CoverageResults<double>("Fibonacci","Get",5);
            
            coverage.AddStartNode(NodeType.If);
            if (n <= 1)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(n);
            }

            coverage.AddNode(2,NodeType.Return);
            var nMinus1 = Get(n - 1);
            coverage.AddNode(3,NodeType.Return);
            var nMinus2 = Get(n - 2);
            
            //Merge histories
            coverage.Merge(nMinus1);
            coverage.Merge(nMinus2);

            coverage.AddNode(4,NodeType.Return);
            return coverage.SetResult(nMinus1.GetReturnValue() + nMinus2.GetReturnValue());
        }

        public CoverageResults<double> GetIterative(int n)
        {
            var coverage = new CoverageResults<double>("Fibonacci","GetIterative",8);
            
            coverage.AddStartNode(NodeType.Statement);
            var last = 1;
            coverage.AddNode(1,NodeType.Statement);
            var lastMinusTwo = 1;
            coverage.AddNode(2,NodeType.Statement);
            var total = 1;
            
            coverage.AddNode(3,NodeType.Loop);
            for (var i = 3; i <= n; i++)
            {
                coverage.AddNode(4,NodeType.Statement);
                total = last + lastMinusTwo;
                coverage.AddNode(5,NodeType.Statement);
                lastMinusTwo = last;
                coverage.AddNode(6,NodeType.Statement);
                last = total;
            }

            coverage.AddEndNode(7,NodeType.Return);
            return coverage.SetResult(total);
        }
    }
}