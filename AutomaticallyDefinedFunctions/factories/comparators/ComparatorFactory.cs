using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.source.ifStatement;

namespace AutomaticallyDefinedFunctions.factories
{
    public static class ComparatorFactory
    {
        private static void ValidateType(Type t)
        {
            if (t != typeof(string) || t == typeof(double) || t == typeof(bool))
                throw new InvalidOperationException($"Unable to generate comparator function of type {t}");
        }
        public static LessThanComparator<T> CreateLessThanComparator<T>() where T : IComparable
        {
            ValidateType(typeof(T));

            return new LessThanComparator<T>();
        }
        
        public static EqualsComparator<T> CreateEqualsComparator<T>() where T : IComparable
        {
            ValidateType(typeof(T));

            return new EqualsComparator<T>();
        }
        
        public static GreaterThanComparator<T> CreateGreaterThanComparator<T>() where T : IComparable
        {
            ValidateType(typeof(T));

            return new GreaterThanComparator<T>();
        }

        public static List<NodeComparator<T>> GetAllComparators<T>() where T : IComparable
        {
            ValidateType(typeof(T));
            return new List<NodeComparator<T>>(){new LessThanComparator<T>(),new EqualsComparator<T>(),new GreaterThanComparator<T>()};
        }

    }
}