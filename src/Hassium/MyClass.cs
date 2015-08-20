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

        private static class options
        {
            public static bool Debug = false;
            public static string FilePath = "";
        }

        public static void Main(string[] args)
        {
            if (args[0].StartsWith("-d") || args[0].StartsWith("--debug"))
            {
                options.Debug = true;
                options.FilePath = args[1];
                Interpreter.variables.Add("args", shiftArray(args, 2));
            }
            else if (args[0].StartsWith("-h") || args[0].StartsWith("--help"))
            {
                Console.WriteLine("USAGE: Hassium.exe [OPTIONS] [FILE] [ARGUMENTS]\nArguments:\n-h  --help\tShows this help\n-d  --debug\tDisplays tokens from lexer\n");
                Environment.Exit(0);
            }
            else
            {
                options.FilePath = args[0];
                Interpreter.variables.Add("args", shiftArray(args, 1));
            }

            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture; // zdimension: without that, decimal numbers doesn't work on other cultures (in france and other countries we use , instead of . for floating-point number)

            Interpreter.variables.Add("true", true);
            Interpreter.variables.Add("false", false);

            List<Token> tokens = new Lexer(File.ReadAllText(options.FilePath)).Tokenize();
            if (options.Debug)
                Debug.PrintTokens(tokens);
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

