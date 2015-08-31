namespace Hassium.Parser.Ast
{
    public class ExceptionNode: AstNode
    {
        public string Value { get; private set; }

        public ExceptionNode(string value)
        {
            Value = value;
        }
    }
}

