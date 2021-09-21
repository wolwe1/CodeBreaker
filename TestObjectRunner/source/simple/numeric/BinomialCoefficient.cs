namespace TestObjects.source.simple.numeric
{
    public class BinomialCoefficient
    {
        public int Get(int n, int k)
        {
            // Base Cases
            if (k > n)
                return 0;
            
            if (k == 0 || k == n)
                return 1;
 
            // Recur
            return Get(n - 1, k - 1)
                   + Get(n - 1, k);
        }
    }
}