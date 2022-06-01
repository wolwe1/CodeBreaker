using System.Collections.Generic;
using AutomaticallyDefinedFunctions.generators.adf;
using AutomaticallyDefinedFunctions.structure.adf;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using AutomaticallyDefinedFunctions.structure.functions.other;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;
using AutomaticallyDefinedFunctions.structure.state;
using CodeBreakerLib;
using CodeBreakerLib.connectors.operators.implementation;
using CodeBreakerLib.dynamicLoading;
using CodeBreakerLib.quickbuild;
using CodeBreakerLib.settings;
using CodeBreakerLib.testHandler;
using CodeBreakerLib.testHandler.Generalisation;
using CodeBreakerLib.testHandler.integration;
using CodeBreakerLib.testHandler.setup;

namespace CodeBreaker
{
    class Program
    {
        static void Main(string[] args)
        {

            var testHandler = new TestHandler(new TestFileDescriptorStrategy("sample.txt"),new GeneticAlgorithmBuilder())
             .LoadTests()
             .RunAllTests();

            //TODO: Read from file and begin generalisation test
             // var adfsWithOriginTests = AdfLoader.ReadFromDirectory();
             //
             // var generalisationTestHandler = new GeneralisationTestHandler(new TestFileDescriptorStrategy("AllTests.txt"));
             // var results = generalisationTestHandler.Run(adfsWithOriginTests);
             // results.Summarise();

            //convertFiles();

            // var id = "ADF(MainFunc<System.Double,System.Double>[SUB<System.Double,System.Double>[Rand['0']DIV<System.Double,System.Double>[Func<System.Double,System.Double>[LOOP<System.Double,System.Double>[Func<System.Double,System.Double>[DIV<System.Double,System.Double>[Func<System.Double,System.Double>[SUB<System.Double,System.Double>[SUB<System.Double,System.Double>[V['1']ProgResp['1']]V['9']]]Rand['0']]]=<System.Double,System.Double>[ProgResp['1']LastOut['2']]SUB<System.Double,System.Double>[Func<System.Double,System.Double>[LOOP<System.Double,System.Double>[ProgResp['1']=<System.Double,System.Double>[V['6']ProgResp['1']]LastOut['2']]]ExecCount['5']]]]ExecCount['5']]]]*Func<System.Double,System.Double>[LOOP<System.Double,System.Double>[V['Null']<<System.Double,System.Double>[V['Null']V['Null']]V['Null']]])";
            //
            // var settings = new StateAdfSettings<double, double>(
            //     GlobalSettings.MaxFunctionDepth,
            //     GlobalSettings.MaxMainDepth,
            //     1,
            //     GlobalSettings.TerminalChance
            // );
            //
            // var generator = new StateAdfGenerator<double, double>(0, settings);
            //
            // var adf = generator.GenerateFromId(id);
            //
            // for (int i = 0; i < 5; i++)
            // {
            //     var values = adf.GetValues();
            // }
            
        }

        private static void convertFiles()
        {
             var files = new List<string>()
             {
                 "Anagram - Get",
                "Anagram - GetRecursive",
                 "Palindrome - Get",
                 "Palindrome - GetRecursive",
                 "Fibonacci - Get",
                 "Fibonacci - GetIterative",
                 "EuclideanAlgorithm - Get",
                 "EuclideanAlgorithm - GetRecursive",
                 "BinomialCoefficient - Get",
                 "Mandelbrot - Get",
                 "BooleanArtefacts - TrueOrNothing",
                 "BooleanArtefacts - EitherOr",
                 "BooleanArtefacts - And",
                 "BooleanArtefacts - Or",
                 "BooleanArtefacts - AndOr",
                 "BooleanArtefacts - Xor",
                 "IsPrime - Get",
                 "Substring - Get",
                "Remainder - Get",
                "Vowels - Get"
             };
            
             // foreach (var file in files)
             // {
                  Calculator.ConvertAvg("stdDeviation\\4-5pp\\combined-4-5pp-stdDev.txt");
             // }
            //Calculator.Format("csv","Remainder - Get_Run0_Avg fitness");
        }
        public static void TestMutation()
        {
            var adfGenerator = QuickBuild.CreateStatePopulationGenerator<string, double>();

            var adf = adfGenerator.GenerateNewMember();
            var adf2 = adfGenerator.GenerateNewMember();

            var crossoverOperator = new CrossoverOperator<string,double>(100);

            crossoverOperator.CreateModifiedChildren(new List<string>() {adf.GetId(),adf2.GetId()},adfGenerator );
        }

        private static MainProgram<string> CreateMainWithStringNodes()
        {
            var comp = new GreaterThanComparator<double>(
                new LengthOfNode<double>(new ProgramResponseStateNode<double>()),
                new ValueNode<double>(3));

            var func = new IfNode<string, double>()
                .SetComparisonOperator(comp)
                .SetFalseCodeBlock(new ValueNode<string>("Program output is boring"))
                .SetTrueCodeBlock(new ValueNode<string>("Program output is interesting!"));
            
            return new MainProgram<string>(func);
        }
    }
}