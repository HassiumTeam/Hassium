using System;
using System.Collections.Generic;

namespace Hassium
{
    public static class Debug
    {
        public static void PrintTokens(List<Token> tokens)
        {
            foreach (Token token in tokens)
            {
                Console.WriteLine("Type: " + token.Operator + "\tValue: " + token.Value);
            }
        }
    }
}

