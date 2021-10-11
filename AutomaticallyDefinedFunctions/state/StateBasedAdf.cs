using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace AutomaticallyDefinedFunctions.state
{
    public class StateBasedAdf<T,TU> : Adf<T> where T : IComparable where TU : IComparable
    {
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
                    case ProgramOutputStateNode<TU> programOutputNode:
                        programOutputNode.UpdateState(programResponse);
                        break;
                    default: throw new Exception("Unknown state node");
                }
            }
        }
    }
}