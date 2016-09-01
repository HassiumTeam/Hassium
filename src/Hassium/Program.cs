using System;
using System.Collections.Generic;

using Hassium.Compiler;
using Hassium.Compiler.CodeGen;
using Hassium.HassiumBuilder;
using Hassium.Runtime;

namespace Hassium
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /*
            try
            {
            ModuleBuilder module = new ModuleBuilder();
            var method = module.AddMethod("main", new List<Hassium.Compiler.Parser.Ast.FuncParameter>());
            method.AddAstNode(ExpressionBuilder.CreateFunctionCall("println", "\"Hello, World!\""));
            new VirtualMachine().Execute(module.Build(), new string[0]);
            }
            catch (CompileException ex)
            {
                Console.WriteLine(ex.SourceLocation);
                Console.WriteLine(ex.Message);
            }*/
            HassiumArgumentConfig.ExecuteConfig(new HassiumArgumentParser().Parse(args));
        }
    }
}