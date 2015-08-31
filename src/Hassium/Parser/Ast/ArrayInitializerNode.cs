using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Parser.Ast
{
    public class ArrayInitializerNode : AstNode
    {
        private readonly Dictionary<object, object> _value;

        public Dictionary<object, object> Value
        {
            get { return _value; }
        }

        public bool IsDictionary { get; set; }

        public ArrayInitializerNode(Dictionary<object, object> items)
        {
            _value = items;
        }

        public ArrayInitializerNode()
        {
            _value = new Dictionary<object, object>();
        }

        public void AddItem(object item)
        {
            _value.Add(_value.Count, item);
        }

        public void AddItem(object key, object item)
        {
            _value.Add(key, item);
        }

        public static ArrayInitializerNode Parse(Parser parser)
        {
            var ret = new ArrayInitializerNode();
            parser.ExpectToken(TokenType.Bracket, "[");
            ret.IsDictionary = false;

            while (!parser.MatchToken(TokenType.Bracket, "]"))
            {              
                var ct1 = ExpressionNode.Parse(parser);
                if(parser.AcceptToken(TokenType.Identifier, ":"))
                {
                    ret.IsDictionary = true;
                    ret.AddItem(ct1, ExpressionNode.Parse(parser));
                }
                else
                {
                    ret.AddItem(ct1);
                }
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
