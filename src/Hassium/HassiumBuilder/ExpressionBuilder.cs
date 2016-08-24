using System;
using System.Collections.Generic;

using Hassium.Compiler.Scanner;
using Hassium.Compiler.Parser;
using Hassium.Compiler.Parser.Ast;

namespace Hassium.HassiumBuilder
{
    public class ExpressionBuilder
    {
        public static BinaryOperationNode CreateBinaryOperation(BinaryOperation operation, AstNode left, AstNode right)
        {
            return new BinaryOperationNode(ModuleBuilder.SourceLocation, operation, left, right);
        }
        public static AstNode CreateConstantLoad(object constant)
        {
            AstNode ret;
            if (constant is char)
                ret = new CharNode(ModuleBuilder.SourceLocation, (char)constant);
            else if (constant is int)
                ret = new IntegerNode(ModuleBuilder.SourceLocation, (int)constant);
            else if (constant is double)
                ret = new FloatNode(ModuleBuilder.SourceLocation, (double)constant);
            else
                ret = new StringNode(ModuleBuilder.SourceLocation, constant.ToString());
            return ret;
        }
        public static DictionaryDeclarationNode CreateDictionaryDeclaration(Dictionary<AstNode, AstNode> expressions)
        {
            var ret = new DictionaryDeclarationNode(ModuleBuilder.SourceLocation);
            foreach (var pair in expressions)
                ret.Children.Add(new KeyValuePairNode(ModuleBuilder.SourceLocation, pair.Key, pair.Value));
            return ret;
        }
        public static FunctionCallNode CreateFunctionCall(AstNode call, params AstNode[] parameters)
        {
            List<AstNode> parameterList = new List<AstNode>();
            foreach (var parameter in parameters)
                parameterList.Add(parameter);
            return new FunctionCallNode(ModuleBuilder.SourceLocation, call, new ArgumentListNode(ModuleBuilder.SourceLocation, parameterList));
        }
        public static FunctionCallNode CreateFunctionCall(string call, params AstNode[] parameters)
        {
            return CreateFunctionCall(parseExpression(call), parameters);
        }
        public static FunctionCallNode CreateFunctionCall(string call, params string[] parameters)
        {
            List<AstNode> parameterList = new List<AstNode>();
            foreach (string param in parameters)
                parameterList.Add(parseExpression(param));
            return CreateFunctionCall(call, parameterList.ToArray());
        }

        public static AstNode parseExpression(string expr)
        {
            return new Parser().Parse(new Lexer().Scan(expr));
        }
    }

    public enum ExpressionType
    {
        BinaryOperation,
        CharConst,
        DictionaryDeclaration,
        FloatConst,
        FunctionCall,
        GlobalDeclaration,
        IdentifierLoad,
        IntConst,
        Lambda,
        ListDeclaration,
        ListLoad,
        StringConst,
        TupleDeclaration
    }
}

