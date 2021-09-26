using System;

namespace AutomaticallyDefinedFunctions.factories
{
    public static class RandomNumberFactory
    {
        private static Random Random = new();

        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public static bool TrueOrFalse()
        {
            return Next(2) == 0;
        }

        public static void SetSeed(int seed)
        {
            Random = new Random(seed);
        }

        public static bool AboveThreshold(int threshold)
        {
            var num = Next(100);

            return num > threshold;
        }
    }
}