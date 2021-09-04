using System;

namespace AutomaticallyDefinedFunctions.factories
{
    public static class RandomNumberFactory
    {
        private static readonly Random Random = new();

        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public static bool TrueOrFalse()
        {
            return Next(2) == 0;
        }
    }
}