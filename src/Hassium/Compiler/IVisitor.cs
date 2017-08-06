using Hassium.Compiler.Parser.Ast;

namespace Hassium.Compiler
{
    public interface IVisitor
    {
        void Accept(ArgumentListNode node);
        void Accept(AttributeAccessNode node);
        void Accept(BinaryOperationNode node);
        void Accept(BreakNode node);
        void Accept(CharNode node);
        void Accept(ClassDeclarationNode node);
        void Accept(CodeBlockNode node);
        void Accept(ContinueNode node);
        void Accept(DictionaryDeclarationNode node);
        void Accept(DoWhileNode node);
        void Accept(EnforcedAssignmentNode node);
        void Accept(EnumNode node);
        void Accept(ExpressionStatementNode node);
        void Accept(FloatNode node);
        void Accept(ForNode node);
        void Accept(ForeachNode node);
        void Accept(FunctionCallNode node);
        void Accept(FunctionDeclarationNode node);
        void Accept(IdentifierNode node);
        void Accept(IfNode node);
        void Accept(IntegerNode node);
        void Accept(IterableAccessNode node);
        void Accept(LambdaNode node);
        void Accept(ListDeclarationNode node);
        void Accept(MultipleAssignmentNode node);
        void Accept(RaiseNode node);
        void Accept(ReturnNode node);
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
