using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.Interpreter;
using Hassium.Parser.Ast;

namespace Hassium.Parser
{
    public abstract class AstNode
    {
        public List<AstNode> Children
        {
            get;
            private set;
        }

        public int Position { get; set; }

        public bool CanBeIndexed
        {
            get
            {
                return this is IdentifierNode || this is MemberAccessNode || this is FunctionCallNode || this is StringNode ||
                       this is InstanceNode || this is ArrayInitializerNode || this is ArrayGetNode;
            }
        }

        public bool ReturnsValue
        {
            get
            {
                return this is IdentifierNode || this is MemberAccessNode || this is FunctionCallNode || this is StringNode ||
                       this is InstanceNode || this is ArrayInitializerNode || this is ArrayGetNode || this is BinOpNode ||
                       this is ConditionalOpNode || this is LambdaFuncNode || this is NumberNode || this is UnaryOpNode;
            }
        }

        public bool Any(Func<AstNode, bool> fc)
        {
            return Children.Any(node => node.Children.Count > 0 && (fc(node) || node.Any(fc)));
        }

        public abstract object Visit(IVisitor visitor);

        public void VisitChild(IVisitor visitor)
        {
            Children.All(x =>
            {
                x.Visit(visitor);
                return true;
            });
        }

        public bool CanBeModified
        {
            get { return this is IdentifierNode || this is MemberAccessNode; }
        }

        protected AstNode () : this(-1)
        {                   
        }

        protected AstNode(int position)
        {
            Children = new List<AstNode>();
            Position = position;
        }
    }
}

