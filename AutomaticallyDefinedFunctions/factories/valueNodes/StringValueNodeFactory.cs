using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes
{
    public static class StringValueNodeFactory
    {
        public static IEnumerable<ValueNode<string>> GetAll()
        {
            return Enumerable.Range('a', 'z' - 'a' + 1).Select(c => new ValueNode<string>( ((char)c).ToString()));
        }

        public static ValueNode<string> Get()
        {
            var choice = RandomNumberFactory.Next(26);
            return GetAll().ElementAt(choice);
        }
    }
}