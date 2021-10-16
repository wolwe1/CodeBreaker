// using System;
// using System.Collections.Generic;
// using System.Linq;
// using AutomaticallyDefinedFunctions.factories;
// using CodeBreakerLib.visitors;
// using GeneticAlgorithmLib.source.core.population;
//
// namespace CodeBreakerLib.connectors.operators.implementation
// {
//     public class FunctionSwapOperator<T> : Operator<T> where T : IComparable
//     {
//         public FunctionSwapOperator(int applicationPercentage) : base(applicationPercentage)
//         {
//         }
//
//         protected override IEnumerable<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
//         {
//             var firstAdf = GetAdfFromParents(parents,generator);
//             var secondAdf = GetAdfFromParents(parents,generator);
//
//             var firstDefinitionIndex = RandomNumberFactory.Next(firstAdf.GetNumberOfDefinitions());
//             var secondDefinitionToIndex = RandomNumberFactory.Next(secondAdf.GetNumberOfDefinitions());
//
//             var firstFunction = firstAdf.GetFunctionDefinition(firstDefinitionIndex);
//             var secondFunction = secondAdf.GetFunctionDefinition(secondDefinitionToIndex);
//
//             firstAdf = firstAdf.SetFunctionDefinition(firstDefinitionIndex, secondFunction);
//             secondAdf = secondAdf.SetFunctionDefinition(secondDefinitionToIndex, firstFunction);
//             
//             var oldDefinitionName = firstAdf.GetDefinitions().ElementAt(firstDefinitionIndex).GetName();
//             var oldDefinitionName2 = secondAdf.GetDefinitions().ElementAt(secondDefinitionToIndex).GetName();
//
//             
//             var secondAdfDefinitionCollector = new FunctionDefinitionCollectorVisitor<T>(oldDefinitionName2);
//
//             var firstAdfMains = firstAdf.GetMainPrograms().ToList();
//             
//             foreach (var mainProgram in firstAdfMains)
//             {
//                 var firstAdfDefinitionCollector = new FunctionDefinitionCollectorVisitor<T>(oldDefinitionName);
//             
//                 var instancesOfFunction
//                 
//             }
//             
//             FunctionDefinitions[definitionIndex] = newDefinition;
//             
//             return new List<string>(){firstAdf.GetId(),secondAdf.GetId()};
//         }
//     }
// }

//TODO: Finish this