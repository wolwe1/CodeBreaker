using System;
using System.Text.RegularExpressions;

namespace AutomaticallyDefinedFunctions.parsing
{
    public static class AdfParser
    {
        public static (string[], string[]) ParseAdfId(string adfId)
        {
            var content = adfId["ADF(".Length..^1];

            var contents = content.Split("-");

            return (contents[0].Split(";"),
                contents[1].Split(";"));
        }

        public static string GetIdWithoutDelimiters(string id)
        {
            return id.Replace("[", "").Replace("]", "");
        }

        public static bool IsFunctionIdentifier(string id)
        {
            return id == NodeCategory.If  || 
                   id == NodeCategory.Loop || 
                   id == NodeCategory.Add;
        }
        
        public static bool IsFunctionIdentifier(char id)
        {
            return IsFunctionIdentifier(id.ToString());
        }
        
        public static string GetTypeInfo(string id,string symbol)
        {
            var match = Regex.Match(id, @$"^{symbol}<.*?,.*?>");
            
            return match.Groups[0].Value;
        }
        
        public static Type GetAuxType(string typeInfo)
        {
            if (typeInfo == "")
                return null;
            
            var auxType = typeInfo.Split(",")[1];
            auxType = auxType.Remove(auxType.Length - 1);
            
            return Type.GetType(auxType);
        }

        public static string GetValueFromQuotes(string id)
        {
            //Find entire value
            var match = Regex.Match(id, @"^\'.*?\'");
            //Retrieve first match
            var currentId = match.Groups[0].Value;
            //Strip quotes
            return currentId[1..^1];
        }

        public static string GetChromosomeAfterFunctionAt(string chromosome, int pointOfFunctionStart)
        {
            var functionOnwards = chromosome[pointOfFunctionStart..];

            if (!IsFunctionIdentifier(functionOnwards[0]))
                throw new Exception("Cannot remove function from chromosome, the point chosen is not a function");

            var functionEnd = GetFunctionEnd(functionOnwards);

            return chromosome[functionEnd..];
        }

        public static string IsolateFirstNode(string chromosome)
        {
            var end = GetFunctionEnd(chromosome);

            return chromosome[..end];
        }

        public static string PlaceChromosomeInNode(string node, string chromosome)
        {
            var searchIndex = node.IndexOf("[", StringComparison.Ordinal);

            return $"{node[0..searchIndex]}{chromosome}]";
        }

        private static int GetFunctionEnd(string functionId)
        {
            var funcOpen = 1;
            var searchIndex = functionId.IndexOf("[", StringComparison.Ordinal);

            for (var i = searchIndex; i < functionId.Length; i++)
            {
                var currentChar = functionId[i];

                switch (currentChar)
                {
                    case ']':
                        funcOpen--;
                        break;
                    case '[':
                        funcOpen++;
                        break;
                }

                if (funcOpen == 0)
                    return i;

            }

            throw new Exception($"Function was never closed, bad ID:  {functionId}");
        }
    }
}