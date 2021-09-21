using System.Collections.Generic;
using TestObjects.source.capture;

namespace TestObjects.source.simple.strings
{
    public class Anagram
    {
        public bool Get(string stringOne, string stringTwo)
        {
            var coverage = new CoverageResults<string>("Anagram","Get",16);
            
            coverage.AddStartNode(NodeType.Statement);
            stringOne = stringOne.ToLower().Replace(" ","");
            coverage.AddNode(1,NodeType.Statement);
            stringTwo = stringTwo.ToLower().Replace(" ","");
            
            coverage.AddNode(2,NodeType.If);
            if (stringOne.Length != stringTwo.Length)
            {
                coverage.AddEndNode(3,NodeType.Return);
                return false;
            }
            
            coverage.AddNode(4,NodeType.FunctionCall);
            var firstStringCharacterSet = GetCharacterDictionary(stringOne,coverage);
            
            coverage.AddNode(11,NodeType.Loop);
            foreach (var character in stringTwo)
            {
                coverage.AddNode(12,NodeType.If);
                if (!firstStringCharacterSet.ContainsKey(character))
                {
                    coverage.AddEndNode(13,NodeType.Return);
                    return false;
                }
                
                coverage.AddNode(14,NodeType.If);
                if (--firstStringCharacterSet[character] < 0)
                {
                    coverage.AddEndNode(15,NodeType.Return);
                    return false;
                }
            }
            
            coverage.AddEndNode(16,NodeType.Return);
            return true;
        }

        private static Dictionary<char, int> GetCharacterDictionary(string stringOne,CoverageResults<string> coverage)
        {
            coverage.AddNode(5,NodeType.Statement);
            var firstStringCharacterSet = new Dictionary<char, int>();

            coverage.AddNode(6,NodeType.Loop);
            foreach (var character in stringOne)
            {
                coverage.AddNode(7,NodeType.If);
                if (firstStringCharacterSet.ContainsKey(character))
                {
                    coverage.AddNode(8,NodeType.Statement);
                    firstStringCharacterSet[character] += 1;
                }
                else
                {
                    coverage.AddNode(9,NodeType.Statement);
                    firstStringCharacterSet[character] = 1;
                }
                    
            }
            coverage.AddEndNode(10,NodeType.Return);
            return firstStringCharacterSet;
        }

        public bool GetRecursive(string str, string strTwo)
        {
            throw new System.NotImplementedException();
        }
    }
}