using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes.standard
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
        
        public static ValueNode<string> Get(string id)
        {
            return new ValueNode<string>(id);
        }
        
        
    }
}