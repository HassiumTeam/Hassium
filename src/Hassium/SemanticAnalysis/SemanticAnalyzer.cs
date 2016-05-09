using System;

using Hassium.Parser;

namespace Hassium.SemanticAnalysis
{
    public class SemanticAnalyzer : IVisitor
    {
        private AstNode code;
        private SymbolTable result;

        public SymbolTable Analyze(AstNode ast)
        {
            code = ast;
            result = new SymbolTable();
            result.EnterScope();
            code.VisitChildren(this);
            return result;
        }

        public void Accept(ArgListNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(ArrayAccessNode node) {}
        public void Accept(ArrayDeclarationNode node) {}
        public void Accept(AttributeAccessNode node) {}
        public void Accept(BinaryOperationNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(BoolNode node) {}
        public void Accept(BreakNode node) {}
        public void Accept(CaseNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(ClassNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(CodeBlockNode node)
        {
            result.EnterScope();
            node.VisitChildren(this);
            result.PopScope();
        }
        public void Accept(ContinueNode node) {}
        public void Accept(DoubleNode node) {}
        public void Accept(FuncNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(UnaryOperationNode node) {}
        public void Accept(IdentifierNode node) {}
        public void Accept(Int64Node node) {}
        public void Accept(NewNode node) {}
        public void Accept(CharNode node) {}
        public void Accept(ConditionalNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(ForNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(ForeachNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(FunctionCallNode node) {}
        public void Accept(ExpressionNode node) {}
        public void Accept(ExpressionStatementNode node) {}
        public void Accept(EnumNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(KeyValuePairNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(LambdaNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(PropertyNode node) {}
        public void Accept(ReturnNode node) {}
        public void Accept(StatementNode node) {}
        public void Accept(StringNode node) {}
        public void Accept(SwitchNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(TerenaryOperationNode node) {}
        public void Accept(ThisNode node) {}
        public void Accept(TryCatchNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(TupleNode node) {}
        public void Accept(UseNode node) {}
        public void Accept(WhileNode node)
        {
            node.VisitChildren(this);
        }
    }
}

