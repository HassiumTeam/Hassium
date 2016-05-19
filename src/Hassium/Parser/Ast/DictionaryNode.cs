using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class DictionaryNode: AstNode
    {
        public DictionaryNode(SourceLocation location)
        {
            this.SourceLocation = location;
        }

        public static DictionaryNode Parse(Parser parser)
        {
            DictionaryNode dict = new DictionaryNode(parser.Location);
            while (!parser.AcceptToken(TokenType.RightBrace))
            {
                BinaryOperationNode binop = ExpressionNode.Parse(parser) as BinaryOperationNode;
                if (binop.BinaryOperation == BinaryOperation.Slice)
                    dict.Children.Add(new KeyValuePairNode(binop.Left, binop.Right, binop.SourceLocation));
                else
                    throw new ParserException("Unknown node encountered in dictionary " + binop.BinaryOperation, parser.Location);
                parser.AcceptToken(TokenType.Comma);
            }

            return dict;
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

