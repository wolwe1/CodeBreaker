using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestObjectlib.source.simple.strings
{
    public class Anagram
    {
        public bool Get(string stringOne, string stringTwo)
        {
            stringOne = stringOne.ToLower().Replace(" ","");
            stringTwo = stringTwo.ToLower().Replace(" ","");
            
            if (stringOne.Length != stringTwo.Length)
                return false;
            
            var firstStringCharacterSet = GetCharacterDictionary(stringOne);
            
            foreach (var character in stringTwo)
            {
                if (!firstStringCharacterSet.ContainsKey(character))
                    return false;

                if (--firstStringCharacterSet[character] < 0)
                    return false;
            }

            return true;
        }

        private static Dictionary<char, int> GetCharacterDictionary(string stringOne )
        {
            var firstStringCharacterSet = new Dictionary<char, int>();
            
            foreach (var character in stringOne)
            {
                if (firstStringCharacterSet.ContainsKey(character))
                    firstStringCharacterSet[character] += 1;
                else
                    firstStringCharacterSet[character] = 1;
            }
            
            return firstStringCharacterSet;
        }

        public bool GetRecursive(string str, string strTwo)
        {
            throw new System.NotImplementedException();
        }
    }
}