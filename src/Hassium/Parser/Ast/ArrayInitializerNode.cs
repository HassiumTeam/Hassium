using System.Collections.Generic;
using Hassium.Interpreter;

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

        public ArrayInitializerNode(int position, Dictionary<object, object> items) : base(position)
        {
            _value = items;
        }

        public ArrayInitializerNode(int position) : this(position, new Dictionary<object, object>())
        {
        }

        public void AddItem(object item)
        {
            _value.Add(_value.Count, item);
        }

        public void AddItem(object key, object item)
        {
            _value.Add(key, item);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}