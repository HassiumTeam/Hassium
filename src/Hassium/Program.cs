// Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//  * Redistributions of source code must retain the above copyright notice, this list
//    of conditions and the following disclaimer.
// 
//  * Redistributions in binary form must reproduce the above copyright notice, this
//    list of conditions and the following disclaimer in the documentation and/or
//    other materials provided with the distribution.
// Neither the name of the copyright holder nor the names of its contributors may be
// used to endorse or promote products derived from this software without specific
// prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
// DAMAGE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;
using Hassium.Lexer;
using Hassium.Parser;
using Hassium.Parser.Ast;
using Hassium.Semantics;

namespace Hassium
{
    public static class Program
    {
        // set this to true so run the code without exception handling, so the debugger can stop at exceptions
        private static bool disableTryCatch = false;


        public static class options
        {
            public static bool Debug { get; set; }
            public static string FilePath { get; set; }
            public static bool ShowTime { get; set; }
            public static string Code { get; set; }
            public static bool Golf { get; set; }
            public static bool Secure { get; set; }
            public static bool Compile { get; set; }
            public static bool CompileRead { get; set; }
        }

        public static Interpreter.Interpreter CurrentInterpreter = new Interpreter.Interpreter();
        private static Stopwatch st;

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static void Main(string[] args)
        {
            Initialize(args);

            if (options.Compile)
            {
                string fileContents = "";
                List<Token> tokens = new Lexer.Lexer(options.Code).Tokenize();
                foreach (Token token in tokens)
                    fileContents += token.TokenClass + " " + token.Value + "\n";

                File.WriteAllBytes("a.out", System.Text.Encoding.ASCII.GetBytes(fileContents));
            }
            if (options.Golf)
            {
                Console.WriteLine(Lexer.Lexer.Minimize(options.Code));
                Environment.Exit(0);
            }
            if (options.ShowTime)
            {
                st = new Stopwatch();
                st.Start();
            }
            if (disableTryCatch)
            {
                List<Token> tokens = new Lexer.Lexer(options.Code).Tokenize();
                if (options.Debug)
                    Debug.Debug.PrintTokens(tokens);
                Parser.Parser hassiumParser = new Parser.Parser(tokens, options.Code);
                AstNode ast = hassiumParser.Parse();
                CurrentInterpreter.SymbolTable = new SemanticAnalyser(ast).Analyse();
                CurrentInterpreter.Code = ast;
                CurrentInterpreter.HandleErrors = false;
                CurrentInterpreter.Execute();
            }
            else if (!options.CompileRead && !disableTryCatch)
            {
                try
                {
                    List<Token> tokens = new Lexer.Lexer(options.Code).Tokenize();
                    if (options.Debug)
                        Debug.Debug.PrintTokens(tokens);
                    Parser.Parser hassiumParser = new Parser.Parser(tokens, options.Code);
                    AstNode ast = hassiumParser.Parse();
                    CurrentInterpreter.SymbolTable = new SemanticAnalyser(ast).Analyse();
                    CurrentInterpreter.Code = ast;
                    CurrentInterpreter.Execute();
                }
                catch(ParseException e)
                {
                    Console.WriteLine();
                    printError(Program.options.Code, e);

                    Console.WriteLine("\nStack Trace: \n" + e.StackTrace);
                    Environment.Exit(-1);
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("There has been an error. Message: " + e.Message);
                    Console.WriteLine("\nStack Trace: \n" + e.StackTrace);
                    Environment.Exit(-1);
                }
            }
            if (options.CompileRead)
            {
                List<Token> tokens = new List<Token>();
                string[] strTokens = System.Text.Encoding.ASCII.GetString(File.ReadAllBytes(options.FilePath)).Split('\n');
                for (int x = 0; x < strTokens.Length - 1; x++)
                {
                    string token = strTokens[x];
                    tokens.Add(new Token(ParseEnum<TokenType>(token.Split(' ')[0]), token.Substring(token.IndexOf(" ") + 1), -1));
                }

                AstNode node = new Parser.Parser(tokens, strTokens.ToString()).Parse();
                new Interpreter.Interpreter(new SemanticAnalyser(node).Analyse(), node).Execute();
                return;
            }

            if (options.ShowTime)
            {
                st.Stop();
                Console.WriteLine("\n" + st.Elapsed + " seconds");
            }
            Environment.Exit(CurrentInterpreter.Exitcode);
        }

        public static void printError(string str, ParseException e)
        {
            var idx = e.Position;
            if (idx == -1)
            {
                Console.WriteLine("Error at position <unknown>: " + e.Message);
                return;
            }
            var line = str.Substring(0, idx).Split('\n').Length;
            var _x = str.Split('\n').Take(line);
            var res = _x.Last();
            string trimd = res.Trim();
            _x = _x.Take(line - 1);
            var column = idx - (string.Join("\n", _x).Length + (_x.Any() ? 1 : 0)) + 1;
            Console.WriteLine("Error at position " + idx + ", line " + line
                              + " column " + column + ": " +
                              e.Message);
            Console.WriteLine("   " + trimd);
            Console.WriteLine(new string(' ', 2 + (column - (res.Length - trimd.Length))) + '^');
        }

        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.Major + "." +
                   Assembly.GetExecutingAssembly().GetName().Version.Minor + "." +
                   Assembly.GetExecutingAssembly().GetName().Version.Build;
        }

        private static void Initialize(IList<string> args)
        {
            if (args.Count <= 0)
                enterInteractive();

            int i = 0;
            bool ff = false;
            for (i = 0; i < args.Count; i++)
            {
                if (ff)
                {
                    i--;
                    break;
                }
                switch (args[i].ToLower())
                {
                    case "-h":
                    case "--help":
                        Console.WriteLine(
                            "Hassium " + GetVersion() + "\n\n" +
                            "USAGE: Hassium.exe [OPTIONS] [FILE] [ARGUMENTS]\n" +
                            "Options:\n" +
                            "-h    --help\tShows this help\n" +
                            "-d    --debug\tDisplays tokens from lexer\n" +
                            "-r    --repl\tEnters interactive interpreter (enabled by default)\n" +
                            "-g    --golf\tShrinks the code down as best it can\n" +
                            "-t    --time\tShow the running time of the program\n" +
                            "-v    --version\tShows the version and info of the Interpreter\n" +
                            "-c    --compile\tCompiles the tokens and saves them in a.out\n" +
                            "-cr   --compileRead\tReads compiled tokens and executes them\n " +
                            "-s    --safe\tEnables the secure mode (disable dangerous functions)");
                        Environment.Exit(0);
                        break;
                    case "-d":
                    case "--debug":
                        options.Debug = true;
                        break;
                    case "-t":
                    case "--time":
                        options.ShowTime = true;
                        break;
                    case "-g":
                    case "--golf":
                        options.Golf = true;
                        break;
                    case "-s":
                    case "--safe":
                        options.Secure = true;
                        break;
                    case "-r":
                    case "--repl":
                        enterInteractive();
                        break;
                    case "-v":
                    case "--version":
                        displayInfo();
                        break;
                    case "-c":
                    case "--compile":
                        options.Compile = true;
                        break;
                    case "-cr":
                    case "--compileRead":
                        options.CompileRead = true;
                        break;
                    default:
                        if (File.Exists(args[i]))
                        {
                            options.FilePath = args[i];
                            ff = true;
                            break;
                        }
                        else
                            throw new ArgumentException("The file " + args[i] + " does not exist.");
                }
            }
            CurrentInterpreter.SetVariable("args", new HassiumArray(args.Skip(i + 1)), null, true);

            // zdimension: without that, decimal numbers doesn't work on other cultures (in france and other countries we use , instead of . for floating-point number)
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;


            options.Code = File.ReadAllText(options.FilePath).Replace("\t", "    "); // replace tabs by 4 spaces
            options.Code = options.Code.Replace("\r\n", "\n").Replace("\r", "\n"); // convert all newlines to linux style (\n)
        }

        private static void enterInteractive()
        {
            Console.WriteLine("Hassium REPL " + GetVersion() + " - (c) HassiumTeam 2015");
            CurrentInterpreter = new Interpreter.Interpreter(false);
            if (options.ShowTime)
            {
                st = new Stopwatch();
            }
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (input == "exit") break;
                if (options.ShowTime)
                {
                    st.Reset();
                    st.Start();
                }
                List<Token> tokens = new Lexer.Lexer(input).Tokenize();
                if (options.Debug)
                    Debug.Debug.PrintTokens(tokens);
                Parser.Parser hassiumParser = new Parser.Parser(tokens, input);
                AstNode ast = hassiumParser.Parse();
                CurrentInterpreter.SymbolTable = new SemanticAnalyser(ast).Analyse();
                CurrentInterpreter.Code = ast;
                CurrentInterpreter.Execute(!ast.Any(x => !(x is BinOpNode || x is NumberNode || x is StringNode)));
                if (options.ShowTime)
                {
                    st.Stop();
                    Console.WriteLine("\n" + st.Elapsed + " seconds");
                }
            }
        }

        private static void displayInfo()
        {
            Console.WriteLine("Hassium Interpreter Version " + GetVersion());
            Console.WriteLine(
                "Creators: Jacob Misirian (MisirianSoft), Monsieur Z (zdimension), and Sloan Crandell (GruntTheDivine)");
            Console.WriteLine("Official Website: http://HassiumLang.com");
            Console.WriteLine("Find us on GitHub at https://github.com/HassiumTeam/Hassium");
            Console.WriteLine("Hassium is open source and licensed under the GPLV2 License\n\n");
        }
    }
}