using System;

using Hassium.Compiler.Emit;
using Hassium.Compiler.Exceptions;
using Hassium.Runtime;
using Hassium.Runtime.Types;

namespace Hassium
{
    public class Program
    {
        public static string MasterPath = string.Empty;

        static void Main(string[] args)
        {
            try
            {
                MasterPath = System.IO.Directory.GetCurrentDirectory();
                VirtualMachine vm = new VirtualMachine();
                var module = HassiumCompiler.CompileModuleFromFilePath(args[0]);
                HassiumList hargs = new HassiumList(new HassiumObject[0]);
                foreach (var arg in args)
                    hargs.add(vm, null, new HassiumString(arg));

                vm.Execute(module, hargs);
            }
            catch (CompilerException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
            catch (ParserException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
            catch (ScannerException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
        }
    }
}
