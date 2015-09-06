using System.Collections.Generic;
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class SwitchNode : AstNode
    {
        public AstNode Predicate
        {
            get
            {
                return Children[0];
            }
        }
        public List<CaseNode> Body { get; set; }
        public AstNode DefaultBody { get; set; }

        public SwitchNode(int position, AstNode predicate, List<CaseNode> body, AstNode defaultBody = null) : base(position)
        {
            Children.Add(predicate);
            Body = body;
            DefaultBody = defaultBody;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }

    public class CaseNode : AstNode
    {
        public List<AstNode> Values { get; set; } 
        public AstNode Body { get; set; }

        public CaseNode(int position, List<AstNode> predicate, AstNode body) : base(position)
        {
            Values = predicate;
            Body = body;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}
