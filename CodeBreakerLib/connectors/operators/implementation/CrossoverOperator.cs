// using System;
// using System.Collections.Generic;
// using AutomaticallyDefinedFunctions.factories;
// using AutomaticallyDefinedFunctions.parsing;
// using GeneticAlgorithmLib.source.core.population;
//
// namespace CodeBreakerLib.connectors.operators.implementation
// {
//     public class CrossoverOperator<T> : Operator<T> where T : IComparable
//     {
//         public CrossoverOperator(int applicationPercentage) : base(applicationPercentage)
//         {
//         }
//
//         protected override List<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
//         {
//             var parent = GetAdfFromParents(parents,generator);
//             var parent2 = GetAdfFromParents(parents,generator);
//             
//             var mainToMutate = RandomNumberFactory.Next(parent.GetNumberOfMainPrograms());
//             var main2ToMutate = RandomNumberFactory.Next(parent2.GetNumberOfMainPrograms());
//             
//             var numberOfNodes = parent.GetMainNodeCount(mainToMutate);
//             var nodeToReplace = RandomNumberFactory.Next(numberOfNodes);
//             
//             var numberOfNodes2 = parent.GetMainNodeCount(main2ToMutate);
//             var nodeToReplace2 = RandomNumberFactory.Next(numberOfNodes2);
//             
//             var populationGenerator = (AdfPopulationGenerator<T>) generator;
//
//             var parentSubTree = parent.GetSubTreeOfMain(mainToMutate, nodeToReplace);
//             
//             parent.ReplaceNodeInMain(mainToMutate,nodeToReplace,populationGenerator.GetFunctionGenerator());
//
//             return new List<string>(){adf.GetId()};
//         }
//
//         private string Swap(string parent, int pointOnChromosome, string parent2, int pointOnChromosome2)
//         {
//             var parentIsFunc = AdfParser.IsFunctionIdentifier(parent[pointOnChromosome]);
//             var parent2IsFunc = AdfParser.IsFunctionIdentifier(parent2[pointOnChromosome2]);
//
//             var isolatedSegment = AdfParser.IsolateFirstNode(parent[pointOnChromosome..]);
//             var isolatedSegment2 = AdfParser.IsolateFirstNode(parent2[pointOnChromosome2..]);
//
//             var newFirstHalf = AdfParser.PlaceChromosomeInNode(isolatedSegment, isolatedSegment2);
//
//             return $"{newFirstHalf}{parent[pointOnChromosome]}";
//         }
//     }
// }