using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class StatementNode: AstNode
    {
        public StatementNode(SourceLocation location)
        {
            this.SourceLocation = location;
        }

        public static AstNode Parse(Parser parser)
        {
            if (parser.MatchToken(TokenType.Identifier, "func"))
                return FuncNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "class"))
                return ClassNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "if"))
                return ConditionalNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "while"))
                return WhileNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "until"))
                return WhileNode.Parse(parser, true);
            else if (parser.MatchToken(TokenType.Identifier, "for"))
                return ForNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "foreach"))
                return ForeachNode.Parse(parser);
            else if (parser.AcceptToken(TokenType.Identifier, "break"))
                return new BreakNode(parser.Location);
            else if (parser.AcceptToken(TokenType.Identifier, "continue"))
                return new ContinueNode(parser.Location);
            else if (parser.MatchToken(TokenType.Identifier, "return"))
                return ReturnNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "use"))
                return UseNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "enum"))
                return EnumNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "switch"))
                return SwitchNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "try"))
                return TryCatchNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "raise"))
                return RaiseNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier) && parser.GetToken(1).TokenType == TokenType.LeftBrace)
                return PropertyNode.Parse(parser);
            else
                return ExpressionStatementNode.Parse(parser);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }
}