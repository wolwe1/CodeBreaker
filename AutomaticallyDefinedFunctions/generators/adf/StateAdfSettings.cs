using AutomaticallyDefinedFunctions.factories.valueNodes;

namespace AutomaticallyDefinedFunctions.generators.adf
{
    public class StateAdfSettings<TAdfResponse,TProgResponse> : AdfSettings
    {
        public StateAdfSettings(int maxFunctionDepth, int maxMainDepth, int argumentCount, int terminalChance) : base(
            maxFunctionDepth, maxMainDepth, argumentCount, terminalChance)
        {
            ValueNodeFactories.Add(new StateNodeFactory<TAdfResponse,TProgResponse>());
        }
    }
}