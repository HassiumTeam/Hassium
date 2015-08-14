using System;
using System.Collections.Generic;

namespace Hassium
{
    public class PrintFunction : IFunction
    {
        public PrintFunction()
        {
        }

        public string Main(List<Token> tokens)
        {
            return tokens[0].Value;
        }
    }
}

