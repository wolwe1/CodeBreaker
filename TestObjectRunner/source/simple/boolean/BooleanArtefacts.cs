using System;

namespace TestObjects.source.simple.boolean
{
    public class BooleanArtefacts
    {

        public string TrueOrNothing(bool activate)
        {
            const string Message = "The statement was activated";

            if (activate)
            {
                Console.WriteLine(Message);
                return Message;
            }

            return "";
        }

        public string EitherOr(bool activate)
        {
            const string Message = "Either";
            const string MessageTwo = "Or";
            string result;

            if (activate)
            {
                Console.WriteLine(Message);
                result = Message;
            }
            else
            {
                Console.WriteLine(MessageTwo);
                result = MessageTwo;
            }

            return result;
        }
        
        public string And(bool first, bool second)
        {
            const string Message = "The statement was activated";

            if (first && second)
            {
                Console.WriteLine(Message);
                return Message;
            }

            return "";
        }
        
        public string Or(bool first, bool second)
        {
            const string Message = "The statement was activated";

            if (first || second)
            {
                Console.WriteLine(Message);
                return Message;
            }

            return "";
        }
        
        public string AndOr(bool first, bool second)
        {
            var result = "Neither value were true";
            const string MessageAnd = "Both values were true";
            const string MessageOrOne = "The first value was true";
            const string MessageOrTwo = "The second value was true";

            if (first && second)
            {
                Console.WriteLine(MessageAnd);
                result = MessageAnd;
            }
            else if(first)
            {
                Console.WriteLine(MessageOrOne);
                result = MessageOrOne;
            }
            else if (second)
            {
                Console.WriteLine(MessageOrTwo);
                result = MessageOrTwo;
            }

            return result;
        }
    }
}