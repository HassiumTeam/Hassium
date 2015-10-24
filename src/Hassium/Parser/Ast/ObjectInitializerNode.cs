using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ObjectInitializerNode : AstNode
    {
        private readonly Dictionary<string, AstNode> _value;

        public Dictionary<string, AstNode> Value
        {
            get { return _value; }
        }

        public bool IsDictionary { get; set; }

        public ObjectInitializerNode(int position, Dictionary<AstNode, AstNode> items) : base(position)
        {
            _value = items.ToDictionary(x => ((IdentifierNode)x.Key).Identifier, x => x.Value);
            items.All(x =>
            {
                Children.Add(x.Key);
                Children.Add(x.Value);
                return true;
            });
        }

        public ObjectInitializerNode(int position) : this(position, new Dictionary<AstNode, AstNode>())
        {
        }


        public void AddItem(IdentifierNode key, AstNode item)
        {
            _value.Add(key.Identifier, item);
            Children.Add(key);
            Children.Add(item);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}
