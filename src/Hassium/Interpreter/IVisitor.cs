using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Parser.Ast;

namespace Hassium.Interpreter
{
    public interface IVisitor
    {
        object Accept(Expression expr);
        object Accept(ArgListNode node);
        object Accept(ArrayGetNode node);
        object Accept(ArrayIndexerNode node);
        object Accept(ArrayInitializerNode node);
        object Accept(BinOpNode node);
        object Accept(BreakNode node);
        object Accept(ClassNode node);
        object Accept(CodeBlock node);
        object Accept(ConditionalOpNode node);
        object Accept(ContinueNode node);
        object Accept(ForEachNode node);
        object Accept(ForNode node);
        object Accept(FuncNode node);
        object Accept(FunctionCallNode node);
        object Accept(IdentifierNode node);
        object Accept(IfNode node);
        object Accept(InstanceNode node);
        object Accept(LambdaFuncNode node);
        object Accept(MemberAccessNode node);
        object Accept(MentalNode node);
        object Accept(NumberNode node);
        object Accept(ReturnNode node);
        object Accept(StatementNode node);
        object Accept(StringNode node);
        object Accept(ThreadNode node);
        object Accept(TryNode node);
        object Accept(UnaryOpNode node);
        object Accept(WhileNode node);
    }
}
