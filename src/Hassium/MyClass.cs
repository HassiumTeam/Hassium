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
            if (IsLinux)
                LibPath = "/home/" + Environment.UserName + "/.Hassium/";
            else
                LibPath = "C:\\Users\\" + Environment.UserName + "\\.Hassium\\";

            if (!Directory.Exists("C:\\Users\\" + Environment.UserName + "\\.Hassium\\") && !Directory.Exists("/home/" + Environment.UserName + "/.Hassium/"))
            {
                Console.WriteLine("If you are on linux you need to run the dll install script in Hassium/lib/ to install the standard library. If you are on Windows then move all of the .dll files from the folders in Hassium/lib to C:\\Users\\YourName\\.Hassium\\");
                Environment.Exit(0);
            }
            List<Token> tokens = new Lexer(File.ReadAllText(args[0])).Tokenize();
            //Debug.PrintTokens(tokens);
            Parser hassiumParser = new Parser(tokens);
            AstNode ast = hassiumParser.Parse();
            new Interpreter(ast).Execute();
        }
    }
}

