using Hassium.Compiler.Emit;
using Hassium.Compiler.Exceptions;
using Hassium.Compiler.Lexer;
using Hassium.Compiler.Parser;
using Hassium.PackageManager;
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
            module.Attributes.Add("__global__", new HassiumClass("__global__"));
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

                var init = (module.Attributes["__global__"].Attributes["__init__"] as HassiumMethod);
                init.Module = module;
                //init.Instructions.Remove(init.Instructions[init.Instructions.Count - 1]);
                if (init.Instructions.Count > 0)
                {
                    if (init.Instructions[init.Instructions.Count - 1].InstructionType == InstructionType.Pop)
                        init.Instructions.Insert(init.Instructions.Count - 1, new HassiumInstruction(init.SourceLocation, InstructionType.Return, -1));
                    else
                        init.Instructions.Add(new HassiumInstruction(init.SourceLocation, InstructionType.Return, -1));
                }

                vm.ImportGlobals();
                var ret = vm.ExecuteMethod(init);

                if (!(ret is HassiumNull))
                    Console.WriteLine(ret.ToString(vm, vm.CurrentSourceLocation).String);
            }
        }
    }
}
