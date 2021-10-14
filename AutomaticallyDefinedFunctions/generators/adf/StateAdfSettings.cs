using AutomaticallyDefinedFunctions.factories.valueNodes.state;

namespace AutomaticallyDefinedFunctions.generators.adf
{
    public class StateAdfSettings<TAdfResponse,TProgResponse> : AdfSettings
    {
        public StateAdfSettings(int maxFunctionDepth, int maxMainDepth, int argumentCount, int terminalChance) : base(
            maxFunctionDepth, maxMainDepth, argumentCount, terminalChance)
        {
            ValueNodeFactories.Add(new ProgramResponseStateFactory<TProgResponse>());
            ValueNodeFactories.Add(new ExecutionCountStateFactory());
            ValueNodeFactories.Add(new LastOutputStateFactory<TAdfResponse>());
        }
    }
}