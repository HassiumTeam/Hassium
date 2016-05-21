using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ExtendNode: AstNode
    {
        public AstNode Target { get; private set; }
        public ClassNode Class { get { return (ClassNode)Children[0]; } }

        public ExtendNode(AstNode target, ClassNode clazz, SourceLocation location)
        {
            Target = target;
            Children.Add(clazz);
            this.SourceLocation = location;
        }

        public static ExtendNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "extend");
            AstNode target = ExpressionNode.Parse(parser);
            ClassNode clazz = new ClassNode("", StatementNode.Parse(parser), new System.Collections.Generic.List<string>(), parser.Location);
            return new ExtendNode(target, clazz, parser.Location);
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

