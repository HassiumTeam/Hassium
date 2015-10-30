using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    [Serializable]
    public class BulkAssignNode : AstNode
    {
        public List<BinOpNode> Nodes { get; private set; }
        public BulkAssignNode(int position, IEnumerable<BinOpNode> binaryOps) : base(position)
        {
            Nodes = binaryOps.ToList();
        }
        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}