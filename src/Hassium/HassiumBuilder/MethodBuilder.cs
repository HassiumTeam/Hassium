using System;
using System.Collections.Generic;

using Hassium.Compiler.Parser.Ast;

namespace Hassium.HassiumBuilder
{
    public class MethodBuilder
    {
        public FuncNode Function { get; private set; }
        public AstNode FunctionBody { get; private set; }

        public MethodBuilder(string name, List<FuncParameter> parameters, string returnType = "")
        {
            FunctionBody = new CodeBlockNode();
            Function = new FuncNode(ModuleBuilder.SourceLocation, name, parameters, FunctionBody, returnType);
        }

        public void AddAstNode(AstNode ast)
        {
            FunctionBody.Children.Add(ast);
        }
    }
}

