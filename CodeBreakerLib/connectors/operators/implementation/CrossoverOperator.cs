using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;
using CodeBreakerLib.visitors;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.operators.implementation
{
    public class CrossoverOperator<T> : Operator<T> where T : IComparable
    {
        public CrossoverOperator(int applicationPercentage) : base(applicationPercentage) { }

        protected override List<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
        {
            var parent = GetAdfFromParents(parents,generator);
            var parent2 = GetAdfFromParents(parents,generator);
            
            var mainToMutate = RandomNumberFactory.Next(parent.GetNumberOfMainPrograms());
            var main2ToMutate = RandomNumberFactory.Next(parent2.GetNumberOfMainPrograms());

            var main = parent.GetMainPrograms().ElementAt(mainToMutate);
            var main2 = parent2.GetMainPrograms().ElementAt(main2ToMutate);
            
            //Pick a type
            var typeOfNodesToSwap = RandomNumberFactory.Next(3);

            MainProgram<T> newMain1;
            MainProgram<T> newMain2;
            switch (typeOfNodesToSwap)
            {
                case 0: 
                    (newMain1,newMain2) = PerformCrossover<string>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;
                case 1: 
                    (newMain1,newMain2) = PerformCrossover<double>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;
                case 2: 
                    (newMain1,newMain2) = PerformCrossover<bool>(main, main2);
                    parent.SetMain(mainToMutate, newMain1);
                    parent2.SetMain(main2ToMutate, newMain2);
                    break;

                default: throw new Exception("Crossover could not select type of node to replace");
            }

            return new List<string>() {parent.GetId(), parent2.GetId()};
        }
        
        private (MainProgram<T> main, MainProgram<T> main2) PerformCrossover<TU>(MainProgram<T> main, MainProgram<T> main2) where TU : IComparable
        {
            var (nodesOfTypeInMain, originalNodesOfTypeInMain2) = GetNodesFromMainsOfType<TU>(main,main2);

            //Low chance that tree does not contain, no-op
            if (nodesOfTypeInMain.Count == 0 || originalNodesOfTypeInMain2.Count == 0)
                return (main, main2);
            
            var chosenNodeToSwap = nodesOfTypeInMain[RandomNumberFactory.Next(nodesOfTypeInMain.Count - 1) + 1];
           
            //Check if it is a comparator, they need to be replaced with another comparator
            List<INode<TU>> nodesOfTypeInMain2;
            if (chosenNodeToSwap is NodeComparator<TU>)
            {
                nodesOfTypeInMain2 = originalNodesOfTypeInMain2
                    .OfType<NodeComparator<TU>>()
                    .Select(c => (INode<TU>)c).ToList();
            }
            else
            {
                nodesOfTypeInMain2 = originalNodesOfTypeInMain2.Where( n => n is not NodeComparator<TU>).ToList();
            }

            if (nodesOfTypeInMain2.Count == 0)
                return (main, main2);
            
            var chosenNodeToSwap2 = nodesOfTypeInMain2[RandomNumberFactory.Next(nodesOfTypeInMain2.Count - 1) + 1];

            var copy = chosenNodeToSwap.GetCopy();
            var copy2 = chosenNodeToSwap2.GetCopy();

            var chosenNodeParent = (ChildManager) chosenNodeToSwap.Parent;
            var chosenNode2Parent = (ChildManager) chosenNodeToSwap2.Parent;
            
            chosenNodeParent.SetChild(chosenNodeToSwap,copy2);
            chosenNode2Parent.SetChild(chosenNodeToSwap2,copy);
            
            return (main,main2);

        }
        private static (List<INode<TU>>, List<INode<TU>>) GetNodesFromMainsOfType<TU>(MainProgram<T> main, MainProgram<T> main2) where TU : IComparable
        {
            var nodeCollector1 = new NodeCollectorVisitor<TU>();
            var nodeCollector2 = new NodeCollectorVisitor<TU>();

            main.Visit(nodeCollector1);
            main2.Visit(nodeCollector2);

            return (nodeCollector1.GetNodes(), nodeCollector2.GetNodes());
        }

        protected override int GetNumberOfOffspringToProduce(ICollection parents)
        {
            var numberOfOffspringToProduce = base.GetNumberOfOffspringToProduce(parents);

            //Ensure even number otherwise crossover cannot work
            if (numberOfOffspringToProduce % 2 != 0)
                return numberOfOffspringToProduce + 1;

            return numberOfOffspringToProduce;
        }
    }
}