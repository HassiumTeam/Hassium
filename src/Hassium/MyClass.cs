using System;
using System.IO;
using System.Collections.Generic;

namespace Hassium
{
    public static class HassiumInterpreter
    {
        public static string LibPath = "";
        public static bool IsLinux
        {
            get
            {
                int p = (int) Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public static void Main(string[] args)
        {
            List<Token> tokens = new Lexer(File.ReadAllText(args[0])).Tokenize();
            //Debug.PrintTokens(tokens);
            Parser hassiumParser = new Parser(tokens);
            AstNode ast = hassiumParser.Parse();
            new Interpreter(ast).Execute();
        }
    }
}

