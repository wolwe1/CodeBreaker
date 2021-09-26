namespace AutomaticallyDefinedFunctions.generators
{
    public class AdfSettings
    {
        public readonly int MaxFunctionDepth;
        public readonly int MaxMainDepth;
        public readonly int ArgumentCount;
        public readonly int TerminalChance;
        
        //Optional
        public int NumberOfFunctions;

        public AdfSettings(int maxFunctionDepth, int maxMainDepth, int argumentCount, int terminalChance)
        {
            MaxFunctionDepth = maxFunctionDepth;
            MaxMainDepth = maxMainDepth;
            ArgumentCount = argumentCount;
            TerminalChance = terminalChance;

            NumberOfFunctions = 1;
        }
    }
}