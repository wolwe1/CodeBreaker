using System.Collections.Generic;

namespace TestObjectlib.source.simple.strings
{
    public class Palindrome
    {
        public string Get(string str)
        {
            var palindrome = "";

            for (var i = str.Length - 1; i >= 0; i--)
            {
                palindrome += str[i];
            }

            return palindrome;
        }

        public string GetRecursive(string str)
        {
            if (str.Length == 0)
                return str;

            var tail = str[^1];
            var head = str.Substring(0, str.Length - 1);

            return tail + GetRecursive(head);
        }
    }
}