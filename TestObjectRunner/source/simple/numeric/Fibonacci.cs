using System;
using System.Threading;
using TestObjects.source.capture;

namespace TestObjects.source.simple.numeric
{
    public class Fibonacci
    {
        public CoverageResults<double> Get(int n)
        {
            return FunctionWatcher.Execute(GetRecursive, (double)n).Result;
        }
        
        // public CoverageResults GetIterative(int n)
        // {
        //     return FunctionWatcher.Execute(GetIter, n).Result;
        // }
        public CoverageResults<double> GetRecursive(double n, CancellationToken cancellationToken)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<double>("Fibonacci","Get",5);
            
            //Prevent blowing the stack
            if (cancellationToken.IsCancellationRequested)
                 cancellationToken.ThrowIfCancellationRequested();  //Unwind the stack
            

            coverage.AddStartNode(NodeType.If);
            if (n <= 1)
            {
                coverage.AddNode(1,NodeType.Return);
                return coverage.SetResult(n);
            }

            coverage.AddNode(2,NodeType.Return);
            var nMinus1 = GetRecursive(n - 1,cancellationToken);
            coverage.AddNode(3,NodeType.Return);
            var nMinus2 = GetRecursive(n - 2,cancellationToken);
            
            //Merge histories
            coverage.Merge(nMinus1);
            coverage.Merge(nMinus2);

            coverage.AddNode(4,NodeType.Return);
            if(nMinus1.GetReturnValue() is not null && nMinus2.GetReturnValue() is not null)
                return coverage.SetResult(nMinus1.GetReturnValue() + nMinus2.GetReturnValue());

            return coverage.SetResult(-1);
        }

        public CoverageResults<double> GetIterative(int n)
        {
            var coverage = CoverageResultWrapper.SetupCoverage<double>("Fibonacci","GetIterative",8);

            //Be a nice user
            if (n >= 1000)
                return coverage.SetResult(-1);
            
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