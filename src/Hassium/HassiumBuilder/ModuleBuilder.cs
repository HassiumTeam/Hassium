using System;
using System.Collections.Generic;

using Hassium.Compiler;
using Hassium.Compiler.CodeGen;
using Hassium.Compiler.Parser.Ast;
using Hassium.Compiler.SemanticAnalysis;
using Hassium.Runtime.Objects;

namespace Hassium.HassiumBuilder
{
    public class ModuleBuilder
    {
        public static SourceLocation SourceLocation = new SourceLocation(0, 0);

        public AstNode AstNode { get; private set; }

        public List<MethodBuilder> Methods { get; private set; }

        public ModuleBuilder()
        {
            AstNode = new CodeBlockNode();
            Methods = new List<MethodBuilder>();
        }

        public MethodBuilder AddMethod(string name, List<FuncParameter> parameters, string returnType = "")
        {
            var ret = new MethodBuilder(name, parameters, returnType);
            Methods.Add(ret);
            return ret;
        }

        public HassiumModule Build()
        {
            foreach (MethodBuilder method in Methods)
                AstNode.Children.Add(method.Function);
            return new Compiler.CodeGen.Compiler().Compile(AstNode, new SemanticAnalyzer().Analyze(AstNode));
        }
    }
}

