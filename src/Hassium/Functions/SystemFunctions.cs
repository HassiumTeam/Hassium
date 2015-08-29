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
        public static object Exit(object[] args)
        {
            Environment.Exit(args.Length > 0 ? Convert.ToInt32(args[0]) : 0);

            return null;
        }

        [IntFunc("system")]
        public static object System(object[] args)
        {
			Process process = new Process();
			process.StartInfo.FileName = args[0].ToString();
			process.StartInfo.Arguments = String.Join("", args.Skip(1));
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = false;
			process.Start();

			string output = process.StandardOutput.ReadToEnd();
			process.WaitForExit();

			return output;
        }

        [IntFunc("datetime")]
        public static object DateTime(object[] args)
        {
            switch (args.Length)
            {
                case 3:
                    return new DateTime(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
                case 6:
                    return new DateTime(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]), Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), Convert.ToInt32(args[4]), Convert.ToInt32(args[5]));
            }
			return global::System.DateTime.Now.ToString();
        }

        [IntFunc("currentuser")]
        public static object CurrentUser(object[] args)
        {
            return Environment.UserName;
        }

        [IntFunc("eval")]
        public static object Eval(object[] args)
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
