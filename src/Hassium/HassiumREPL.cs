using Hassium.Compiler.Emit;
using Hassium.Compiler.Exceptions;
using Hassium.Compiler.Lexer;
using Hassium.Compiler.Parser;
using Hassium.Runtime;
using Hassium.Runtime.Types;

using System;

namespace Hassium
{
    public class HassiumREPL
    {
        public static void Run(HassiumConfig config)
        {
            HassiumModule module = new HassiumModule();
            module.AddAttribute("__global__", new HassiumClass("__global__"));
            VirtualMachine vm = new VirtualMachine(module);
            
            while (true)
            {
                Console.Write("> ");
                string code = Console.ReadLine();

                try
                {
                    var tokens = new Scanner().Scan("stdin", code);
                    var ast = new Parser().Parse(tokens);
                    module = new HassiumCompiler(config.SuppressWarnings).Compile(ast, module);

                    var init = (module.BoundAttributes["__global__"].BoundAttributes["__init__"] as HassiumMethod);
                    init.Module = module;

                    vm.ImportGlobals();
                    var ret = vm.ExecuteMethod(init);

                    if (!(ret is HassiumNull))
                        Console.WriteLine(ret.ToString(vm, ret, vm.CurrentSourceLocation).String);
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
    }
}
