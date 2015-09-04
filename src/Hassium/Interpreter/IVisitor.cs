using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Parser.Ast;

namespace Hassium.Interpreter
{
    public interface IVisitor
    {
        void Accept(Expression expr);
        void Accept(ArgListNode node);
        void Accept(ArrayGetNode node);
        void Accept(ArrayIndexerNode node);
        void Accept(ArrayInitializerNode node);
        void Accept(BinOpNode node);
        void Accept(BreakNode node);
        void Accept(ClassNode node);
        void Accept(CodeBlock node);
        void Accept(ConditionalOpNode node);
        void Accept(ContinueNode node);
        void Accept(ForEachNode node);
        void Accept(ForNode node);
        void Accept(FuncNode node);
        void Accept(FunctionCallNode node);
        void Accept(IdentifierNode node);
        void Accept(IfNode node);
        void Accept(InstanceNode node);
        void Accept(LambdaFuncNode node);
        void Accept(MemberAccessNode node);
        void Accept(MentalNode node);
        void Accept(NumberNode node);
        void Accept(ReturnNode node);
        void Accept(StatementNode node);
        void Accept(StringNode node);
        void Accept(ThreadNode node);
        void Accept(TryNode node);
        void Accept(UnaryOpNode node);
        void Accept(WhileNode node);
    }
}
