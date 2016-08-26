using System;

using Hassium.Compiler.Parser.Ast;

namespace Hassium.Compiler.Parser
{
    public interface IVisitor
    {
        void Accept(ArgumentListNode node);
        void Accept(AttributeAccessNode node);
        void Accept(BinaryOperationNode node);
        void Accept(BreakNode node);
        void Accept(CharNode node);
        void Accept(ClassNode node);
        void Accept(CodeBlockNode node);
        void Accept(ContinueNode node);
        void Accept(DictionaryDeclarationNode node);
        void Accept(EnforcedAssignmentNode node);
        void Accept(ExpressionStatementNode node);
        void Accept(EnumNode node);
        void Accept(ExtendNode node);
        void Accept(FloatNode node);
        void Accept(ForeachNode node);
        void Accept(ForNode node);
        void Accept(FuncNode node);
        void Accept(FunctionCallNode node);
        void Accept(GlobalNode node);
        void Accept(IdentifierNode node);
        void Accept(IfNode node);
        void Accept(KeyValuePairNode node);
        void Accept(IntegerNode node);
        void Accept(LambdaNode node);
        void Accept(ListAccessNode node);
        void Accept(ListDeclarationNode node);
        void Accept(PropertyNode node);
        void Accept(RaiseNode node);
        void Accept(ReturnNode node);
        void Accept(StatementNode node);
        void Accept(StringNode node);
        void Accept(SwitchNode node);
        void Accept(TernaryOperationNode node);
        void Accept(ThreadNode node);
        void Accept(TraitNode node);
        void Accept(TryCatchNode node);
        void Accept(TupleNode node);
        void Accept(UnaryOperationNode node);
        void Accept(UseNode node);
        void Accept(WhileNode node);
    }
}
   