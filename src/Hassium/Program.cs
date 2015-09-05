using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;
using Hassium.Lexer;
using Hassium.Parser;
using Hassium.Semantics;

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

		private static bool disableTryCatch = false; // set this to true so run the code without exception handling, so the debugger can stop at exceptions

		private static class options
		{
			public static bool Debug;
			public static string FilePath = "";
			public static bool ShowTime;
			public static string Code = "";
		}

		public static Interpreter.Interpreter CurrentInterpreter = new Interpreter.Interpreter();

		public static void Main(string[] args)
		{
			preformSetUp(args);

			Stopwatch st = null;
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
			if(options.ShowTime)
			{
				st.Stop();
				Console.WriteLine("\n" + st.Elapsed + " seconds");
			}
		}

		private static void printErr(string str, ParseException e)
		{
			var idx = e.Position;
			string newline = "";
			if (str.Contains("\r\n")) newline = "\r\n";
			else if (str.Contains("\r")) newline = "\r";
			else newline = "\n";
			int lower = str.Substring(0, idx + 1).LastIndexOf(newline, StringComparison.Ordinal) + newline.Length;
			int upper = str.Substring(idx).IndexOf(newline) + idx;
			string res = str.Substring(lower, upper - lower);
			string trimd = res.Trim();
			int line = Regex.Matches(str.Substring(0, lower), newline).Count + 1;
			int column = (idx - lower + 1);
			Console.WriteLine("Error at position " + idx + ", line " + line
							  + " column " + column + ": " +
							  e.Message);
			Console.WriteLine("   " + trimd);
			Console.WriteLine(new string(' ', 3 + (idx - lower - (res.Length - trimd.Length))) + '^');
		}

		private static void preformSetUp(IList<string> args)
		{
			if (args.Count <= 0)
				enterInteractive();

			int i = 0;
			for (i = 0; i < args.Count; i++)
			{
				if (args[i].StartsWith("-h") || args[i].StartsWith("--help"))
				{
					Console.WriteLine(
						"USAGE: Hassium.exe [OPTIONS] [FILE] [ARGUMENTS]\nArguments:\n-h  --help\tShows this help\n-d  --debug\tDisplays tokens from lexer\n-r  --repl\tEnters interactive interpreter\n-t  --time\tShow the running time of the program");
					Environment.Exit(0);
				}
				else if (args[i].StartsWith("-d") || args[i].StartsWith("--debug"))
				{
					options.Debug = true;
				}
				else if (args[i].StartsWith("-t") || args[i].StartsWith("--time"))
				{
					options.ShowTime = true;
				}
				else if (args[i].StartsWith("-r") || args[i].StartsWith("--repl"))
				{
					enterInteractive();
				}
				else
				{
					if (File.Exists(args[i]))
						options.FilePath = args[i];
					else
						throw new ArgumentException("The file " + args[i] + " does not exist.");
				}
			}
			CurrentInterpreter.SetVariable("args", new HassiumArray(args.Skip(i + 1)), null, true);

			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture; // zdimension: without that, decimal numbers doesn't work on other cultures (in france and other countries we use , instead of . for floating-point number)

			options.Code = File.ReadAllText(options.FilePath);

			preprocessorDirectives();
		}

		private static void preprocessorDirectives()
		{
			/*
			 *
______ ________  ________  _   _ _____   _____ _   _ _____ _____   _____ ______  ___  ______ 
| ___ \  ___|  \/  |  _  || | | |  ___| |_   _| | | |_   _/  ___| /  __ \| ___ \/ _ \ | ___ \
| |_/ / |__ | .  . | | | || | | | |__     | | | |_| | | | \ `--.  | /  \/| |_/ / /_\ \| |_/ /
|    /|  __|| |\/| | | | || | | |  __|    | | |  _  | | |  `--. \ | |    |    /|  _  ||  __/ 
| |\ \| |___| |  | \ \_/ /\ \_/ / |___    | | | | | |_| |_/\__/ / | \__/\| |\ \| | | || |    
\_| \_\____/\_|  |_/\___/  \___/\____/    \_/ \_| |_/\___/\____/   \____/\_| \_\_| |_/\_|    
																							 

					 ,_____._,                ,~-__,__,;\
				   ,/ /  / /~,\              ~'-_ ,~_/__--\
				 ,~'\/__:_; / ~\,_          /-__ ~\/  \";/,\
				/ \ ,/\_\_~\ /  /|,';;;;`,|\  /\/     \=/- |
			   ~--,/_/__  \ ~\ |  `._____.'  |/\/     __---=--
			  /==/./ \ /\  ;\~\_\          _/\/,,__--'._/-' `
			 |==|/    \==\;  ;\|  , \ \ / /L /::/ \,~~ |==|-|
			 |//\\,__/== |: ;  |L_\  \ V / /L::/-__/  /=/-,|
			  \ / | | \ /: ;  ;\ |\\  \ / //|: |__\_/=/--/,,
			   \______,/; ;   ;;\ @|\  | /|@|;: \__\__\_/  ``,,
					 ,;  ;   ;;;|\/' \ |/ '\/;;               '',=``,
					,;  ;   ;;;;\  {  \|' }/;:'                  ,;;;
					,;  ;  ;;;;;:| {   |  }|;'                    , ;;;
				   ,; ;' ::::::;/    ./ \  \                        :, ""
				  ,;;;;`,'`'`';/   ./    \  \_                       :,
				  ,;;     '''|____/   \__/\___|                       :,
				  ,;;             \_.  / _/                            :,
				  ,;;                \/\/                               :,
				  ,;;                                                    :,
				  ,;;                                                    :,
				  ,;;,                         ,--------.,       :;      :,
				   `:';__--                   /       .   \,     :;      :,
		.___________--`                      |        \    \___  :;      :,
	   /                                    |:        | ;  \  \ :;       :,
	  |                                     ;;;      / ;    |   :;      :.
	 |:                       /             ;;;     | ,;    |  :;    ,,:'
	 ;;;     /--;:,:;,,:;;;,;/';,,,,,,,,,,';;:    /  ;      |,;:,,,;:'
	 ;:;    |`"';;;';';;'';;;         |   ;;:     / :;      |;;;'  |
	 ;;    /                          |  ;;:     / :;-_____/   |   |
	 ;:   |                           / ;;:     / :;           |   \
	 ;     |                         |:,;;     | :;            /    |
	;      |                         /-;'   ':| ;:            |:,,;/
	|:  ,;/                         |  ;      |:;             /-__-\
	 ;   |                          \  ::,,,:/:;             |      |
	 /   |                           \/`\    |:;             \  /\  /
	/-____\                              |    \'              \/'`\/
   |      `)                            /-____-\
   | ^   /                             |        |
   |/`\/                               |        |
										\  /\  /
										 \/'`\/
______ ________  ________  _   _ _____   _____ _   _ _____ _____   _____ ______  ___  ______ 
| ___ \  ___|  \/  |  _  || | | |  ___| |_   _| | | |_   _/  ___| /  __ \| ___ \/ _ \ | ___ \
| |_/ / |__ | .  . | | | || | | | |__     | | | |_| | | | \ `--.  | /  \/| |_/ / /_\ \| |_/ /
|    /|  __|| |\/| | | | || | | |  __|    | | |  _  | | |  `--. \ | |    |    /|  _  ||  __/ 
| |\ \| |___| |  | \ \_/ /\ \_/ / |___    | | | | | |_| |_/\__/ / | \__/\| |\ \| | | || |    
\_| \_\____/\_|  |_/\___/  \___/\____/    \_/ \_| |_/\___/\____/   \____/\_| \_\_| |_/\_|    
																							 

*/
			foreach (string line in File.ReadAllLines(options.FilePath))
			{
				if (line.StartsWith("$INCLUDE"))
					options.Code += File.ReadAllText(line.Substring(9, line.Substring(9).LastIndexOf("$", StringComparison.Ordinal))) + options.Code;
				else if (line.StartsWith("$IMPORT"))
				{
					if (File.Exists(line.Substring(8, line.Substring(8).LastIndexOf("$", StringComparison.Ordinal))))
						foreach (KeyValuePair<string, InternalFunction> entry in Interpreter.Interpreter.GetFunctions(line.Substring(8, line.Substring(8).LastIndexOf("$", StringComparison.Ordinal))))
							CurrentInterpreter.SetVariable(entry.Key, entry.Value, null, true);
				}
				else if (line.StartsWith("$DEFINE"))
				{
					string[] parts = line.Substring(8, line.Substring(8).LastIndexOf("$", StringComparison.Ordinal)).Split(' ');
					options.Code = options.Code.Replace(parts[0], parts[1]);
				}
			}
		}

		private static void enterInteractive()
		{
			CurrentInterpreter = new Interpreter.Interpreter(false);
			while (true)
			{
				Console.Write("> ");
				string input = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(input)) return;
				Stopwatch st = null;
				if (options.ShowTime)
				{
					st = new Stopwatch();
					st.Start();
				}
				List<Token> tokens = new Lexer.Lexer(input).Tokenize();
				if (options.Debug)
					Debug.Debug.PrintTokens(tokens);
				Parser.Parser hassiumParser = new Parser.Parser(tokens);
				AstNode ast = hassiumParser.Parse();
				CurrentInterpreter.SymbolTable = new SemanticAnalyser(ast).Analyse();
				CurrentInterpreter.Code = ast;
				CurrentInterpreter.Execute();
				if (options.ShowTime)
				{
					st.Stop();
					Console.WriteLine("\n" + st.Elapsed + " seconds");
				}
			}
		}
	}
}

