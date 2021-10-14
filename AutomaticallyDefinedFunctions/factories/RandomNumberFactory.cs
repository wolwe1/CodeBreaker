using System;

namespace AutomaticallyDefinedFunctions.factories
{
    public static class RandomNumberFactory
    {
        private static Random _random = new();

        public static int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public static bool TrueOrFalse()
        {
            return Next(2) == 0;
        }

        public static void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        public static bool AboveThreshold(int threshold)
        {
            var num = Next(100);

            return num > threshold;
        }
    }
}