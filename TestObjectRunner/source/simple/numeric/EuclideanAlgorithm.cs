namespace TestObjects.source.simple.numeric
{
    public class EuclideanAlgorithm
    {

        public int Gcd(int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public int GcdRecursive(int a, int b)
        {
            if (b == 0)
                return a;

            return GcdRecursive(b, a % b);
        }
    }
}