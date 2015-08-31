namespace Hassium.Parser.Ast
{
    public class StringNode: AstNode
    {
        public string Value { get; private set; }

        public StringNode(string value)
        {
            Value = value;
        }
    }
}

