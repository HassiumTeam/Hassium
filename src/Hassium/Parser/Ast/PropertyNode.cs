using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class PropertyNode : AstNode
    {
        public FuncNode GetNode { get; private set; }
        public FuncNode SetNode { get; private set; }
        public string Name { get; private set; }

        public PropertyNode(int position, string name, FuncNode get, FuncNode set) : base(position)
        {
            Name = name;
            Children.Add(get);
            Children.Add(set);
            GetNode = get;
            SetNode = set;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}
