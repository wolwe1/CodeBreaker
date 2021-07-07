namespace TestObjectlib.source.simple.numeric
{
    public class Fibonacci
    {
        public int Get(int n)
        {
            if (n <= 1)
                return n;

            return Get(n - 1) + Get(n - 2);
        }

        public int GetIterative(int n)
        {
            var last = 1;
            var lastMinusTwo = 1;
            var total = 1;
            
            for (var i = 1; i <= n; i++)
            {
                if (i == 1 || i == 2)
                {
                    total = 1;
                }
                else
                {
                    total = last + lastMinusTwo;

                    lastMinusTwo = last;
                    last = total;
                }
            }

            return total;
        }
    }
}