using System;
using System.Collections.Generic;
using System.IO;

using Hassium.Compiler;
using Hassium.Runtime;

namespace Hassium
{
    public class HassiumArgumentConfig
    {
        public List<string> Arguments { get; private set; }
        public string FilePath { get; set; }

        public static void ExecuteConfig(HassiumArgumentConfig config)
        {
            try
            {
                var module = Compiler.CodeGen.Compiler.CompileModuleFromSource(File.ReadAllText(config.FilePath));
                new VirtualMachine().Execute(module, config.Arguments.ToArray());
            }
            catch (CompileException ex)
            {
                Console.WriteLine("At {0}:", ex.SourceLocation);
                Console.WriteLine(ex.Message);
            }
            catch (InternalException ex)
            {
                Console.WriteLine("At location {0}:", ex.VM.CurrentSourceLocation);
                Console.WriteLine("{0} at:", ex.Message);
                while (ex.VM.CallStack.Count > 0)
                    Console.WriteLine(ex.VM.CallStack.Pop());
            }
        }

        public HassiumArgumentConfig()
        {
            Arguments = new List<string>();
        }
    }
}

