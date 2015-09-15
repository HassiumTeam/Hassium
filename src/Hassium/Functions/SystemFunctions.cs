using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;
using Hassium.Semantics;

namespace Hassium.Functions
{
    public class SystemFunctions : ILibrary
    {

        [IntFunc("system", -1)]
        public static HassiumObject System(HassiumObject[] args)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = args[0].ToString(),
                    Arguments = string.Join("", args.Skip(1)),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = false
                }
            };
            process.Start();
                
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        [IntFunc("date", new []{1, 3, 6, 0})]
        public static HassiumObject Date(HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 1:
                    return DateTime.Now.ToString(args[0].ToString());
                case 3:
                    return new HassiumDate(new DateTime(args[0].HDouble().ValueInt, args[1].HDouble().ValueInt,
                        args[2].HDouble().ValueInt));
                case 6:
                    return new HassiumDate(new DateTime(args[0].HDouble().ValueInt, args[1].HDouble().ValueInt,
                        args[2].HDouble().ValueInt, args[3].HDouble().ValueInt, args[4].HDouble().ValueInt,
                        args[5].HDouble().ValueInt));
                default:
                    return new HassiumDate(DateTime.Now);
            }
        }

        [IntFunc("dateParse", new []{1, 2})]
        public static HassiumObject DateParse(HassiumObject[] args)
        {
            return args.Length == 2 ? new HassiumDate(DateTime.ParseExact(args[0].ToString(), args[1].ToString(), CultureInfo.InvariantCulture)) : new HassiumDate(DateTime.Parse(args[0].ToString()));
        }

        [IntFunc("time", new []{0, 3})]
        public static HassiumObject Time(HassiumObject[] args)
        {
            return args.Length == 0
                ? new HassiumDate(DateTime.Now)
                : new HassiumDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    args[0].HDouble().ValueInt, args[1].HDouble().ValueInt, args[2].HDouble().ValueInt));
        }

        [IntFunc("currentUser", 0)]
        public static HassiumObject CurrentUser(HassiumObject[] args)
        {
            return Environment.UserName;
        }

        [IntFunc("sleep", 1)]
        public static HassiumObject Sleep(HassiumObject[] args)
        {
            Thread.Sleep(args[0].HInt().Value);
            return null;
        }

        [IntFunc("eval", 1)]
        public static HassiumObject Eval(HassiumObject[] args)
        {
            var tokens = new Lexer.Lexer(args[0].ToString()).Tokenize();
            var hassiumParser = new Parser.Parser(tokens);
            var ast = hassiumParser.Parse();
            var intp = new Interpreter.Interpreter(new SemanticAnalyser(ast).Analyse(), ast, false)
            {
                Globals = HassiumInterpreter.CurrentInterpreter.Globals,
                CallStack = HassiumInterpreter.CurrentInterpreter.CallStack
            };
            intp.Execute();
            return null;
        }
    }
}
