using System;
using System.IO;
using System.Collections.Generic;

namespace Hassium
{
    public static class HassiumInterpreter
    {
        public static void Main(string[] args)
        {
            List<Token> tokens = new Lexer(File.ReadAllText("code.txt")).Tokenize();
            Debug.PrintTokens(tokens);
            Parser hassiumParser = new Parser(tokens);
            AstNode ast = hassiumParser.Parse();
            new Interpreter(ast).Execute();
        }
    }
}

