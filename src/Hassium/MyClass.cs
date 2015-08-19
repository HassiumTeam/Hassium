using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

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
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture; // zdimension: without that, decimal numbers doesn't work on other cultures (in france and other countries we use , instead of . for floating-point number)
            Interpreter.variables.Add("args", shiftArray(args, 1));

            List<Token> tokens = new Lexer(File.ReadAllText(args[0])).Tokenize();
            //Debug.PrintTokens(tokens);
            Parser hassiumParser = new Parser(tokens);
            AstNode ast = hassiumParser.Parse();
            new Interpreter(ast).Execute();
        }

        private static string[] shiftArray(string[] args, int startIndex = 1)
        {
            string[] result = new string[args.Length];

            for (int x = startIndex; x < args.Length; x++)
                result[x - startIndex] += args[x].ToString();

            return result;
        }
    }
}

