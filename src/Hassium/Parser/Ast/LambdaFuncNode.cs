using System.Collections.Generic;
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class LambdaFuncNode : AstNode
    {
        public List<string> Parameters { get; private set; }

        public AstNode Body
        {
            get { return Children[0]; }
        }

        public LambdaFuncNode(int position, List<string> paramaters, AstNode body) : base(position)
        {
            Parameters = paramaters;
            Children.Add(body);
        }

        public static explicit operator FuncNode(LambdaFuncNode funcNode)
        {
            return new FuncNode(funcNode.Position, "", funcNode.Parameters, funcNode.Body);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}