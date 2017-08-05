using System;

using Hassium.Compiler.Emit;
using Hassium.Compiler.Exceptions;
using Hassium.Runtime;
using Hassium.Runtime.Types;

namespace Hassium
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
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
        }
    }
}
