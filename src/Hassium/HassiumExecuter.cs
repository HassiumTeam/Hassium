using System;
using System.IO;

using Hassium.CodeGen;
using Hassium.Lexer;
using Hassium.Parser;
using Hassium.SemanticAnalysis;
using Hassium.Runtime;

namespace Hassium
{
    public class HassiumExecuter
    {
        public static HassiumModule FromFilePath(string filePath, bool executeVM = true)
        {
            return FromString(File.ReadAllText(filePath), executeVM);
        }

        public static HassiumModule FromString(string source, bool executeVM = true)
        {
            HassiumModule module = null;
            try
            {
                Lexer.Lexer lexer = new Lexer.Lexer();
                Parser.Parser parser = new Parser.Parser();
                SemanticAnalyzer analyzer = new SemanticAnalyzer();
                HassiumCompiler compiler = new HassiumCompiler();
                var tokens = lexer.Scan(source);
                var ast = parser.Parse(tokens);
                var table = analyzer.Analyze(ast);
                module = compiler.Compile(ast, table, "MainModule");
                if (executeVM)
                {
                    VirtualMachine vm = new VirtualMachine();
                    try
                    {
                        vm.Execute(module);
                    }
                    catch (RuntimeException ex)
                    {
                        Console.WriteLine("Hassium Runtime Exception! Message: {0} at {1}", ex.Message, ex.SourceLocation.ToString());
                        foreach (string str in vm.CallStack)
                            Console.WriteLine("At {0} -> ", str);
                    }
                }
            }
            catch (ParserException ex)
            {
                Console.WriteLine("Compiler error! Message: {0} at {1}", ex.Message, ex.SourceLocation.ToString());
            }
            return module;
        }
    }
}