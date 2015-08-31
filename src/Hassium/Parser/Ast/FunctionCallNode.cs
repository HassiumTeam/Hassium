namespace Hassium.Parser.Ast
{
    public class FunctionCallNode: AstNode
    {
        public AstNode Target
        {
            get
            {
                return Children[0];
            }
        }
        public AstNode Arguments
        {
            get
            {
                return Children[1];
            }
        }

        public FunctionCallNode(AstNode target, ArgListNode arguments)
        {
            Children.Add(target);
            Children.Add(arguments);
        }
    }
}

