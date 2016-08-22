using System;

using Hassium.Compiler.Parser;
using Hassium.Compiler.Parser.Ast;

namespace Hassium.Compiler.SemanticAnalysis
{
    public class SemanticAnalyzer : IVisitor
    {
        private AstNode code;
        private SymbolTable result;

        public SymbolTable Analyze(AstNode ast)
        {
            code = ast;
            result = new SymbolTable();
            result.PushScope();
            code.VisitChildren(this);
            return result;
        }

        public void Accept(ArgumentListNode node) {}
        public void Accept(AttributeAccessNode node) {}
        public void Accept(BinaryOperationNode node) {}
        public void Accept(BreakNode node) {}
        public void Accept(CharNode node) {}
        public void Accept(ClassNode node) 
        {
            node.VisitChildren(this);
        }
        public void Accept(CodeBlockNode node)
        {
            result.PushScope();
            node.VisitChildren(this);
            result.PopScope();
        }
        public void Accept(ContinueNode node) {}
        public void Accept(DictionaryDeclarationNode node) {}
        public void Accept(EnumNode node) {}
        public void Accept(ExpressionStatementNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(ExtendNode node) {}
        public void Accept(FloatNode node) {}
        public void Accept(ForNode node) {}
        public void Accept(ForeachNode node) {}
        public void Accept(FuncNode node)
        {
            node.VisitChildren(this);
        }
        public void Accept(FunctionCallNode node) {}
        public void Accept(IdentifierNode node) {}
        public void Accept(IfNode node) {}
        public void Accept(IntegerNode node) {}
        public void Accept(KeyValuePairNode node) {}
        public void Accept(LambdaNode node) {}
        public void Accept(ListAccessNode node) {}
        public void Accept(ListDeclarationNode node) {}
        public void Accept(PropertyNode node) {}
        public void Accept(RaiseNode node) {}
        public void Accept(ReturnNode node) {}
        public void Accept(StatementNode node) {}
        public void Accept(StringNode node) {}
        public void Accept(SwitchNode node) {}
        public void Accept(TernaryOperationNode node) {}
        public void Accept(TraitNode node) {}
        public void Accept(TryCatchNode node) {}
        public void Accept(TupleNode node) {}
        public void Accept(UnaryOperationNode node) {}
        public void Accept(UseNode node) {}
        public void Accept(WhileNode node) {}
    }
}

