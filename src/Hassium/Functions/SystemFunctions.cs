using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hassium.Functions
{
	public class SystemFunctions : ILibrary
	{
		[IntFunc("exit")]
		public static HassiumObject Exit(HassiumArray args)
		{
			Environment.Exit(args.Value.Length > 0 ? Convert.ToInt32((object)args[0]) : 0);

			return null;
		}

		[IntFunc("system")]
		public static HassiumObject System(HassiumArray args)
		{
			Process process = new Process();
			process.StartInfo.FileName = args[0].ToString();
			process.StartInfo.Arguments = String.Join("", args.Value.Skip(1));
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = false;
			process.Start();

			string output = process.StandardOutput.ReadToEnd();
			process.WaitForExit();

			return output;
		}

		[IntFunc("datetime")]
		public static HassiumObject DateTime(HassiumArray args)
		{
			switch (args.Value.Length)
			{
				case 3:
					return (HassiumObject)(object)(new DateTime(Convert.ToInt32((object)args[0]), Convert.ToInt32((object)args[1]), Convert.ToInt32((object)args[2])));
				case 6:
					return (HassiumObject)(object)(new DateTime(Convert.ToInt32((object)args[0]), Convert.ToInt32((object)args[1]), Convert.ToInt32((object)args[2]), Convert.ToInt32((object)args[3]), Convert.ToInt32((object)args[4]), Convert.ToInt32((object)args[5])));
			}
			return global::System.DateTime.Now.ToString();
		}

		[IntFunc("currentuser")]
		public static HassiumObject CurrentUser(HassiumArray args)
		{
			return Environment.UserName;
		}

		[IntFunc("eval")]
		public static HassiumObject Eval(HassiumArray args)
		{
			List<Token> tokens = new Lexer(args[0].ToString()).Tokenize();
			if (false)
				Debug.PrintTokens(tokens);
			Parser.Parser hassiumParser = new Parser.Parser(tokens);
			AstNode ast = hassiumParser.Parse();
			Interpreter intp = new Interpreter(new SemanticAnalyser(ast).Analyse(), ast, false);
			intp.Globals = HassiumInterpreter.CurrentInterpreter.Globals;
			intp.CallStack = HassiumInterpreter.CurrentInterpreter.CallStack;
			intp.Execute();
			return null;
		}
	}
}
