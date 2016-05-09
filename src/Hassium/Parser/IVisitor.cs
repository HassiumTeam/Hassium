using System;

namespace Hassium.Parser
{
    public interface IVisitor
    {
        void Accept(ArgListNode node);
        void Accept(ArrayAccessNode node);
        void Accept(ArrayDeclarationNode node);
        void Accept(AttributeAccessNode node);
        void Accept(BinaryOperationNode node);
        void Accept(BoolNode node);
        void Accept(BreakNode node);
        void Accept(CaseNode node);
        void Accept(CharNode node);
        void Accept(ContinueNode node);
        void Accept(ClassNode node);
        void Accept(ConditionalNode node);
        void Accept(DoubleNode node);
        void Accept(CodeBlockNode node);
        void Accept(EnumNode node);
        void Accept(ExpressionNode node);
        void Accept(ExpressionStatementNode node);
        void Accept(ForNode node);
        void Accept(ForeachNode node);
        void Accept(FuncNode node);
        void Accept(FunctionCallNode node);
        void Accept(IdentifierNode node);
        void Accept(Int64Node node);
        void Accept(KeyValuePairNode node);
        void Accept(LambdaNode node);
        void Accept(NewNode node);
        void Accept(PropertyNode node);
        void Accept(RaiseNode node);
        void Accept(ReturnNode node);
        void Accept(StatementNode node);
        void Accept(StringNode node);
        void Accept(SwitchNode node);
        void Accept(TerenaryOperationNode node);
        void Accept(TryCatchNode node);
        void Accept(TupleNode node);
        void Accept(ThisNode node);
        void Accept(UseNode node);
        void Accept(UnaryOperationNode node);
        void Accept(WhileNode node);
    }
}

