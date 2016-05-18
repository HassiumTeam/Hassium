using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ArrayDeclarationNode: AstNode
    {
        public ArrayDeclarationNode(SourceLocation location)
        {
            this.SourceLocation = location;
        }

        public static ArrayDeclarationNode Parse(Parser parser)
        {
            ArrayDeclarationNode ret = new ArrayDeclarationNode(parser.Location);
            parser.ExpectToken(TokenType.LeftSquare);

            while(!parser.AcceptToken(TokenType.RightSquare))
            {
                var item = ExpressionNode.Parse(parser);
                if (item is BinaryOperationNode)
                {
                    BinaryOperationNode binop = item as BinaryOperationNode;
                    if (binop.BinaryOperation == BinaryOperation.Slice)
                        ret.Children.Add(new KeyValuePairNode(binop.Left, binop.Right, parser.Location));
                    else
                        ret.Children.Add(item);
                }
                else
                    ret.Children.Add(item);
                parser.AcceptToken(TokenType.Comma);
            }
            return ret;
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

