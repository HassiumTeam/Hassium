using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class WithNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public AstNode Body { get; private set; }
		public AstNode Target { get; private set; }

		public string Assign { get; private set; }

        public WithNode(AstNode target, AstNode body, string assign = null)
        {
            Body = body;
			Target = target;

			Assign = assign;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Body.Visit(visitor);
            Target.Visit(visitor);
        }
    }
}
