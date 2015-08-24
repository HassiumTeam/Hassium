using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Parser.Ast
{
    public class ArrayInitializerNode : AstNode
    {
        private readonly List<object> _value;

        public object[] Value
        {
            get { return Children.ToArray(); }
        }

        public ArrayInitializerNode(List<object> items)
        {
            _value = items;
        }

        public ArrayInitializerNode()
        {
            _value = new List<object>();
        }

        public void AddItem(object item)
        {
            _value.Add(item);
        }

        public static ArrayInitializerNode Parse(Parser parser)
        {
            var ret = new ArrayInitializerNode();
            parser.ExpectToken(TokenType.Bracket, "[");

            while (!parser.MatchToken(TokenType.Bracket, "]"))
            {              
                ret.Children.Add(ExpressionNode.Parse(parser));
                if (!parser.AcceptToken(TokenType.Comma))
                {
                    break;
                }
            }
            parser.ExpectToken(TokenType.Bracket, "]");

            return ret;
        }
    }
}
