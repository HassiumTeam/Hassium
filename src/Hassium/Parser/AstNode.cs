using System.Collections.Generic;
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
                return this is IdentifierNode || this is MemberAccess || this is FunctionCallNode || this is StringNode ||
                       this is InstanceNode || this is ArrayInitializerNode || this is ArrayGetNode;
            }
        }

        public bool ReturnsValue
        {
            get
            {
                return this is IdentifierNode || this is MemberAccess || this is FunctionCallNode || this is StringNode ||
                       this is InstanceNode || this is ArrayInitializerNode || this is ArrayGetNode || this is BinOpNode ||
                       this is ConditionalOpNode || this is LambdaFuncNode || this is NumberNode || this is UnaryOpNode;
            }
        }

        public bool CanBeModified
        {
            get { return this is IdentifierNode || this is MemberAccess; }
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

