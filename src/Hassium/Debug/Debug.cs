using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.Lexer;

namespace Hassium.Debug
{
    public static class Debug
    {
        /// <summary>
        /// Prints the tokens.
        /// </summary>
        /// <param name="tokens">The list of tokens to print.</param>
        public static void PrintTokens(List<Token> tokens)
        {
            var position = 0;
            foreach (Token token in tokens)
            {
                Console.WriteLine(position +
                                  new string(' ', tokens.Count.ToString().Length + 2 - position.ToString().Length) +
                                  "Type: " + token.TokenClass +
                                  new string(' ',
                                      tokens.Max(x => x.TokenClass.ToString().Length) + 3 -
                                      token.TokenClass.ToString().Length) + "Value: " + token.Value);
                position++;
            }
            Console.WriteLine("-- END OF TOKEN LIST --");
            Console.WriteLine();
        }
    }
}