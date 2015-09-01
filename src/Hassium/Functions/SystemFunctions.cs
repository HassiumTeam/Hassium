using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.Functions
{
    public class SystemFunctions : ILibrary
    {
        [IntFunc("exit")]
        public static HassiumObject Exit(HassiumObject[] args)
        {
            Environment.Exit(args.Length > 0 ? Convert.ToInt32((object) args[0]) : 0);

            return null;
        }

        [IntFunc("system")]
        public static HassiumObject System(HassiumObject[] args)
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

        [IntFunc("date")]
        public static HassiumObject Date(HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 1:
                    return DateTime.Now.ToString(args[0].ToString());
                case 3:
                    return new HassiumDate(new DateTime(args[0].HNum().ValueInt, args[1].HNum().ValueInt,
                        args[2].HNum().ValueInt));
                case 6:
                    return new HassiumDate(new DateTime(args[0].HNum().ValueInt, args[1].HNum().ValueInt,
                        args[2].HNum().ValueInt, args[3].HNum().ValueInt, args[4].HNum().ValueInt,
                        args[5].HNum().ValueInt));
                default:
                    return new HassiumDate(DateTime.Now);
            }
        }

        [IntFunc("dateParse")]
        public static HassiumObject DateParse(HassiumObject[] args)
        {
            return args.Length == 2 ? new HassiumDate(DateTime.ParseExact(args[0].ToString(), args[1].ToString(), CultureInfo.InvariantCulture)) : new HassiumDate(DateTime.Parse(args[0].ToString()));
        }

        [IntFunc("time")]
        public static HassiumObject Time(HassiumObject[] args)
        {
            return args.Length == 0
                ? new HassiumDate(DateTime.Now)
                : new HassiumDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    args[0].HNum().ValueInt, args[1].HNum().ValueInt, args[2].HNum().ValueInt));
        }

        [IntFunc("currentUser")]
        public static HassiumObject CurrentUser(HassiumObject[] args)
        {
            return Environment.UserName;
        }

        [IntFunc("sleep")]
        public static HassiumObject Sleep(HassiumObject[] args)
        {
            Thread.Sleep(Convert.ToInt32(args[0].ToString()));
            return null;
        }

        [IntFunc("eval")]
        public static HassiumObject Eval(HassiumObject[] args)
        {
            List<Token> tokens = new Lexer(args[0].ToString()).Tokenize();
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
