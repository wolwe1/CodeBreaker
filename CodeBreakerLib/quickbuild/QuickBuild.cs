﻿
using System;
using AutomaticallyDefinedFunctions.generators.adf;
using CodeBreakerLib.connectors.ga.state;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.quickBuild
{
    public class QuickBuild
    {
        public static IPopulationGenerator<T> CreateStatePopulationGenerator<T,TU>() where T : IComparable where TU : IComparable
        {
            var settings = new StateAdfSettings<T, TU>(3,8,1,60);

            return new StateAdfPopulationGenerator<T, TU>(0, settings);
        }
    }
}