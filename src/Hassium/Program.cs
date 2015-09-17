using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;
using Hassium.Lexer;
using Hassium.Parser;
using Hassium.Parser.Ast;
using Hassium.Semantics;

namespace Hassium
{
	public static class HassiumInterpreter
	{
		private static bool disableTryCatch = false; // set this to true so run the code without exception handling, so the debugger can stop at exceptions

		private static class options
		{
			public static bool Debug;
			public static string FilePath = "";
			public static bool ShowTime;
			public static string Code = "";
		}

		public static Interpreter.Interpreter CurrentInterpreter = new Interpreter.Interpreter();
		private static Stopwatch st;
		public static void Main(string[] args)
		{
			preformSetUp(args);

			
			if(options.ShowTime)
			{
				st = new Stopwatch();
				st.Start();
			}
			if (disableTryCatch)
			{
				List<Token> tokens = new Lexer.Lexer(options.Code).Tokenize();
				if (options.Debug)
					Debug.Debug.PrintTokens(tokens);
				Parser.Parser hassiumParser = new Parser.Parser(tokens);
				AstNode ast = hassiumParser.Parse();
				CurrentInterpreter.SymbolTable = new SemanticAnalyser(ast).Analyse();
				CurrentInterpreter.Code = ast;
				CurrentInterpreter.Execute();
			}
			else
			{
				try
				{
					List<Token> tokens = new Lexer.Lexer(options.Code).Tokenize();
					if (options.Debug)
						Debug.Debug.PrintTokens(tokens);
					Parser.Parser hassiumParser = new Parser.Parser(tokens);
					AstNode ast = hassiumParser.Parse();
					CurrentInterpreter.SymbolTable = new SemanticAnalyser(ast).Analyse();
					CurrentInterpreter.Code = ast;
					CurrentInterpreter.Execute();
				}
				catch (Exception e)
				{
					Console.WriteLine();
					if (e is ParseException)
					{
						printErr(options.Code, (ParseException) e);
					}
					else
					{
						Console.WriteLine("There has been an error. Message: " + e.Message);
					}
					Console.WriteLine("\nStack Trace: \n" + e.StackTrace);
					Environment.Exit(-1);
				}
			}
			if (options.ShowTime)
			{
				st.Stop();
				Console.WriteLine("\n" + st.Elapsed + " seconds");
			}
			Environment.Exit(CurrentInterpreter.exitcode);
		}

		private static void printErr(string str, ParseException e)
		{
			var idx = e.Position;
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

		private static void preformSetUp(IList<string> args)
		{
			if (args.Count <= 0)
				enterInteractive();

			int i = 0;
			for (i = 0; i < args.Count; i++)
			{
				switch(args[i].ToLower())
				{
					case "-h":
					case "--help":
						Console.WriteLine(
						"Hassium " + GetVersion() + "\n\n" +
						"USAGE: Hassium.exe [OPTIONS] [FILE] [ARGUMENTS]\n" +
						"Arguments:\n" +
						"-h  --help\tShows this help\n" +
						"-d  --debug\tDisplays tokens from lexer\n" +
						"-r  --repl\tEnters interactive interpreter (enabled by default)\n" +
						"-t  --time\tShow the running time of the program");
						Environment.Exit(0);
						break;
					case "-v":
					case "--version":
						Console.WriteLine("Hassium " + GetVersion());
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
					case "-r":
					case "--repl":
						enterInteractive();
						break;
                    default:
                        if (File.Exists(args[i]))
                        {
                            options.FilePath = args[i];
                            goto setArgs;
                        }
                        else
							throw new ArgumentException("The file " + args[i] + " does not exist.");
				}
			}
            setArgs:
			CurrentInterpreter.SetVariable("args", new HassiumArray(args.Skip(i + 1)), null, true);

			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture; // zdimension: without that, decimal numbers doesn't work on other cultures (in france and other countries we use , instead of . for floating-point number)

			options.Code = File.ReadAllText(options.FilePath).Replace("\t", "    ");
			options.Code = options.Code.Replace("\r\n", "\n").Replace("\r", "\n");
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
				if(options.ShowTime)
                {
                    st.Reset();
                    st.Start();
                }
				List<Token> tokens = new Lexer.Lexer(input).Tokenize();
				if (options.Debug)
					Debug.Debug.PrintTokens(tokens);
				Parser.Parser hassiumParser = new Parser.Parser(tokens);
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
	}
}

