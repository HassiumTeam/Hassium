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
        public static void Run()
        {
            HassiumModule module = new HassiumModule();
            module.Attributes.Add("__global__", new HassiumClass("__global__"));
            VirtualMachine vm = new VirtualMachine(module);
            
            while (true)
            {
                Console.Write("> ");
                string code = Console.ReadLine();


                /*var mod = HassiumCompiler.CompileModuleFromString("stdin", code);

                foreach (var pair in mod.ConstantPool)
                    if (!module.ConstantPool.ContainsKey(pair.Key))
                        module.ConstantPool.Add(pair.Key, pair.Value);
                foreach (var pair in mod.ObjectPool)
                    if (!module.ObjectPool.ContainsKey(pair.Key))
                        module.ObjectPool.Add(pair.Key, pair.Value);
                foreach (var pair in mod.Globals)
                    if (!module.Globals.ContainsKey(pair.Key))
                        module.Globals.Add(pair.Key, pair.Value);
                */
                try
                {
                    var tokens = new Scanner().Scan("stdin", code);
                    var ast = new Parser().Parse(tokens);
                    module = new HassiumCompiler(false).Compile(ast, module);
                }
                catch (CompilerException ex)
                {
                    Console.WriteLine(ex.Message);
                    //if (config.Dev)
                        Console.WriteLine(ex);
                }
                catch (ParserException ex)
                {
                    Console.WriteLine(ex.Message);
                  //  if (config.Dev)
                        Console.WriteLine(ex);
                }
                catch (ScannerException ex)
                {
                    Console.WriteLine(ex.Message);
                  //  if (config.Dev)
                        Console.WriteLine(ex);
                }

                var init = (module.Attributes["__global__"].Attributes["__init__"] as HassiumMethod);
                init.Module = module;

                vm.ImportGlobals();
                var ret = vm.ExecuteMethod(init);

                if (!(ret is HassiumNull))
                    Console.WriteLine(ret.ToString(vm, vm.CurrentSourceLocation).String);
            }
        }
    }
}
