using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using AutomaticallyDefinedFunctions.visitors;

namespace AutomaticallyDefinedFunctions.structure.state
{
    public class StateBasedAdf<T,TU> : Adf<T> where T : IComparable where TU : IComparable
    {
        public StateBasedAdf(){}
        private StateBasedAdf(List<MainProgram<T>> mainPrograms, List<FunctionDefinition<T>> functionDefinitions) : base(mainPrograms,functionDefinitions) { }

        public void Update(T lastOutput,TU programResponse)
        {
            foreach (var mainProgram in MainPrograms)
            {
                var visitor = new StateNodeVisitor();
                
                mainProgram.Visit(visitor);

                var stateNodes = visitor.StateNodes;

                UpdateStateNodes(stateNodes,lastOutput,programResponse);
            }
        }

        private void UpdateStateNodes(List<IStateNode> stateNodes, T lastOutput, TU programResponse)
        {
            foreach (var stateNode in stateNodes)
            {
                switch (stateNode)
                {
                    case ExecutionCountStateNode exec:
                        exec.UpdateState(exec.GetValue() + 1);
                        break;
                    case LastOutputStateNode<T> lastOutputNode:
                        lastOutputNode.UpdateState(lastOutput);
                        break;
                    case ProgramResponseStateNode<TU> programOutputNode:
                        programOutputNode.UpdateState(programResponse);
                        break;
                    default: throw new Exception("Unknown state node");
                }
            }
        }
        
        public override Adf<T> GetCopy()
        {
            var mainProgramCopies = MainPrograms.Select(main => main.GetCopy());
            var functionDefinitionCopies = FunctionDefinitions.Select(func => (FunctionDefinition<T>)func.GetCopy());
            return new StateBasedAdf<T,TU>(mainProgramCopies.ToList(), functionDefinitionCopies.ToList());
        }
    }
}