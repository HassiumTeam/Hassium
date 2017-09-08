using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Parser.Ast
{
    public class DictionaryDeclarationNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public List<AstNode> Keys { get; private set; }
        public List<AstNode> Values { get; private set; }

        public DictionaryDeclarationNode(SourceLocation location, List<AstNode> keys, List<AstNode> values)
        {
            SourceLocation = location;

            Keys = keys;
            Values = values;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            for (int i = 0; i < Keys.Count; i++)
            {
                Keys[i].Visit(visitor);
                Values[i].Visit(visitor);
            }
        }
    }
}
