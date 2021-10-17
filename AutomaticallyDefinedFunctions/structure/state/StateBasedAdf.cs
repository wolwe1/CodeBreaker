using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.structure.adf;
using AutomaticallyDefinedFunctions.structure.adf.helpers;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using AutomaticallyDefinedFunctions.visitors;

namespace AutomaticallyDefinedFunctions.structure.state
{
    public class StateBasedAdf<T,TU> : Adf<T> where T : IComparable where TU : IComparable
    {
        private AdfHistory<T,TU> _history;

        public StateBasedAdf()
        {
            _history = new AdfHistory<T,TU>();
        }

        private StateBasedAdf(List<MainProgram<T>> mainPrograms, List<FunctionDefinition<T>> functionDefinitions) :
            base(mainPrograms, functionDefinitions)
        {
            _history = new AdfHistory<T,TU>();
        }

        public void Update(AdfOutput<T> lastOutput,TU programResponse)
        {
            _history.AddHistory(lastOutput,programResponse);
            for (var index = 0; index < MainPrograms.Count; index++)
            {
                var mainProgram = MainPrograms[index];
                var outputOfMain = lastOutput.GetOutput(index);
                var visitor = new StateNodeVisitor();

                mainProgram.Visit(visitor);

                var stateNodes = visitor.StateNodes;

                UpdateStateNodes(stateNodes, outputOfMain, programResponse);
            }
        }

        private void UpdateStateNodes(List<IStateNode> stateNodes, Output<T> lastOutput, TU programResponse)
        {
            foreach (var stateNode in stateNodes)
            {
                switch (stateNode)
                {
                    case ExecutionCountStateNode exec:
                        exec.UpdateState(exec.GetValue() + 1);
                        break;
                    case LastOutputStateNode<T> lastOutputNode:
                        if(!lastOutput.Failed) //If the last output failed, the value would be undefined
                            lastOutputNode.UpdateState(lastOutput.Value);
                        break;
                    case ProgramResponseStateNode<TU> programOutputNode:
                        if(!lastOutput.Failed) //If the last output failed, the program response would be null
                            programOutputNode.UpdateState(programResponse);
                        break;
                    case IRandomStateNode: //No action needed
                        break;
                    case OutputFailedStateNode failedNode:
                        failedNode.UpdateState(lastOutput.Failed);
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

        public AdfHistory<T,TU> GetHistory()
        {
            return _history;
        }
    }
}