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

        [IntFunc("date", new[] {1, 3, 6, 0})]
        public static HassiumObject Date(HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 1:
                    return new HassiumDate(DateTime.Now).toString(new HassiumObject[] {args[0].ToString()});
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

        [IntFunc("dateParse", new[] {1, 2})]
        public static HassiumObject DateParse(HassiumObject[] args)
        {
            return args.Length == 2
                ? new HassiumDate(DateTime.ParseExact(args[0].ToString(), args[1].ToString(),
                    CultureInfo.InvariantCulture))
                : new HassiumDate(DateTime.Parse(args[0].ToString()));
        }

        [IntFunc("time", new[] {0, 1, 3})]
        public static HassiumObject Time(HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 0:
                    return new HassiumDate(DateTime.Now);
                case 1:
                    return new HassiumDate(DateTime.Now).toString(new HassiumObject[] {args[0].ToString()});
                default:
                    return new HassiumDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                        args[0].HDouble().ValueInt, args[1].HDouble().ValueInt, args[2].HDouble().ValueInt));
            }
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
                Globals = Program.CurrentInterpreter.Globals,
                CallStack = Program.CurrentInterpreter.CallStack
            };
            intp.Execute();
            return null;
        }
    }
}