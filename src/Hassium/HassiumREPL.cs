using Hassium.Compiler.Emit;
using Hassium.Compiler.Exceptions;
using Hassium.Compiler.Lexer;
using Hassium.Compiler.Parser;
using Hassium.Runtime;
using Hassium.Runtime.Types;

using System;
using System.Collections.Generic;

namespace Hassium
{
    public class HassiumREPL
    {
        public static void Run(HassiumConfig config)
        {
            HassiumModule module = new HassiumModule();
            module.AddAttribute("__global__", new HassiumClass("__global__"));
            var attribs = new Dictionary<string, HassiumObject>();

            VirtualMachine vm = new VirtualMachine(module);

            while (true)
            {
                Console.Write("(1)> ");
                string code = Console.ReadLine();

                try
                {
                    // Read
                    var tokens = new Scanner().Scan("stdin", code);

                    // If we missed a closing ), }, or ], keep reading and appending lines until the code is good.
                    int line = 2;
                    while (countOpenTokens(tokens) > countCloseTokens(tokens))
                    {
                        Console.Write("({0})> ", line++);
                        string temp = Console.ReadLine();
                        foreach (var token in new Scanner().Scan("stdin", temp))
                            tokens.Add(token);
                        code += temp + System.Environment.NewLine;
                    }

                    var ast = new Parser().Parse(tokens);
                    module = new HassiumCompiler(config.SuppressWarnings).Compile(ast, module);

                    // Import
                    foreach (var attrib in module.BoundAttributes["__global__"].BoundAttributes)
                    {
                        if (attribs.ContainsKey(attrib.Key))
                            attribs.Remove(attrib.Key);
                        attribs.Add(attrib.Key, attrib.Value);
                    }
                    module.BoundAttributes["__global__"].BoundAttributes = attribs;
                    var init = (module.BoundAttributes["__global__"].BoundAttributes["__init__"] as HassiumMethod);
                    init.Module = module;

                    // Eval
                    vm.ImportGlobals();
                    var ret = vm.ExecuteMethod(init);
                    
                    // PrintLine
                    if (!(ret is HassiumNull))
                        Console.WriteLine(ret.ToString(vm, ret, vm.CurrentSourceLocation).String);
                    else
                        Console.WriteLine();
                }
                catch (CompilerException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (config.Dev)
                        Console.WriteLine(ex);
                }
                catch (ParserException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (config.Dev)
                        Console.WriteLine(ex);
                }
                catch (ScannerException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (config.Dev)
                        Console.WriteLine(ex);
                }
                catch (UnhandledException ex)
                {
                    Console.WriteLine("Unhandled Exception:");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("\nNear:");
                    ex.SourceLocation.PrintLocation(new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(code)));
                    Console.WriteLine(ex.CallStack);
                }
            }
        }

        private static int countOpenTokens(List<Token> tokens)
        {
            int count = 0;
            foreach (var token in tokens)
                if (token.TokenType == TokenType.OpenCurlyBrace || token.TokenType == TokenType.OpenParentheses || token.TokenType == TokenType.OpenSquareBrace)
                    count++;
            return count;
        }

        private static int countCloseTokens(List<Token> tokens)
        {
            int count = 0;
            foreach (var token in tokens)
                if (token.TokenType == TokenType.CloseCurlyBrace || token.TokenType == TokenType.CloseParentheses || token.TokenType == TokenType.CloseSquareBrace)
                    count++;
            return count;
        }
    }
}