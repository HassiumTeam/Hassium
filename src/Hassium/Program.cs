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

            public static string Code = "";
        }

        public static void Main(string[] args)
        {
            preformSetUp(args);

            List<Token> tokens = new Lexer(options.Code).Tokenize();
            if (options.Debug)
                Debug.PrintTokens(tokens);
            Parser.Parser hassiumParser = new Parser.Parser(tokens);
            AstNode ast = hassiumParser.Parse();

            try
            {
                new Interpreter(new SemanticAnalyser(ast).Analyse(), ast).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                Console.WriteLine("Press Y to show full stack trace");
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static string[] shiftArray(string[] args, int startIndex = 1)
        {
            string[] result = new string[args.Length];

            for (int x = startIndex; x < args.Length; x++)
                result[x - startIndex] += args[x].ToString();

            return result;
        }

        private static void preformSetUp(string[] args)
        {
            if (args[0].StartsWith("-d") || args[0].StartsWith("--debug"))
            {
                options.Debug = true;
                options.FilePath = args[1];
                Interpreter.Globals.Add("args", shiftArray(args, 2));
            }
            else if (args[0].StartsWith("-h") || args[0].StartsWith("--help"))
            {
                Console.WriteLine("USAGE: Hassium.exe [OPTIONS] [FILE] [ARGUMENTS]\nArguments:\n-h  --help\tShows this help\n-d  --debug\tDisplays tokens from lexer\n");
                Environment.Exit(0);
            }
            else
            {
                options.FilePath = args[0];
                Interpreter.Globals.Add("args", shiftArray(args, 1));
            }

            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture; // zdimension: without that, decimal numbers doesn't work on other cultures (in france and other countries we use , instead of . for floating-point number)

            options.Code = File.ReadAllText(options.FilePath);

            preprocessorDirectives();
        }

        private static void preprocessorDirectives()
        {
            foreach (string line in File.ReadAllLines(options.FilePath))
            {
                if (line.StartsWith("$INCLUDE"))
                    options.Code += File.ReadAllText(line.Substring(9, line.Substring(9).LastIndexOf("$")));
                else if (line.StartsWith("$IMPORT"))
                    if (File.Exists(line.Substring(8, line.Substring(8).LastIndexOf("$"))))
                    foreach (KeyValuePair<string, InternalFunction> entry in Interpreter.GetFunctions(line.Substring(8, line.Substring(8).LastIndexOf("$"))))
                            Interpreter.Globals.Add(entry.Key, entry.Value);
            }
        }
    }
}

