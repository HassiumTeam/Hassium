using System.Collections.Generic;
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class FuncNode : AstNode
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public FunctionCallNode CallConstructor { get; private set; }

        public AstNode Body
        {
            get { return Children[0]; }
        }

        public FuncNode(int position, string name, List<string> parameters, AstNode body, FunctionCallNode constructor = null) : base(position)
        {
            Parameters = parameters;
            Name = name;
            CallConstructor = constructor;
            Children.Add(body);
        }

        public static explicit operator LambdaFuncNode(FuncNode funcNode)
        {
            return new LambdaFuncNode(funcNode.Position, funcNode.Parameters, funcNode.Body);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}