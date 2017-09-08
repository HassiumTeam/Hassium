using Hassium.Runtime;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Compiler.Parser.Ast
{
    public class FunctionDeclarationNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public string Name { get; private set; }
        public List<FunctionParameter> Parameters { get; private set; }
        public AstNode Body { get; private set; }
        public AstNode EnforcedReturnType { get; private set; }

        public DocStrAttribute DocStr { get; set; }

        public FunctionDeclarationNode(SourceLocation location, string name, List<FunctionParameter> parameters, AstNode body)
        {
            SourceLocation = location;

            Name = name;
            Parameters = parameters;
            Body = body;
            EnforcedReturnType = null;

            DocStr = null;
        }
        public FunctionDeclarationNode(SourceLocation location, string name, List<FunctionParameter> parameters, AstNode enforcedReturnType, AstNode body)
        {
            SourceLocation = location;

            Name = name;
            Parameters = parameters;
            Body = body;
            EnforcedReturnType = enforcedReturnType;

            DocStr = null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("func {0} (", Name);
            foreach (var param in Parameters)
            {
                switch (param.FunctionParameterType)
                {
                    case FunctionParameterType.Enforced:
                        sb.AppendFormat("{0} : ", param.Name);
                        sb.AppendFormat("{0}, ", param.Type is AttributeAccessNode ? (param.Type as AttributeAccessNode).Right : (param.Type as IdentifierNode).Identifier);
                        break;
                    case FunctionParameterType.Normal:
                        sb.AppendFormat("{0}, ", param.Name);
                        break;
                    case FunctionParameterType.Variadic:
                        sb.AppendFormat("params {0}, ", param.Name);
                        break;
                }
            }
            if (Parameters.Count > 0)
                sb.Append("\b\b");
            sb.Append(")");

            if (EnforcedReturnType != null)
                sb.AppendFormat(" : {0}", EnforcedReturnType is AttributeAccessNode ? (EnforcedReturnType as AttributeAccessNode).Right : (EnforcedReturnType as IdentifierNode).Identifier);

            return sb.ToString();
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Body.Visit(visitor);
            EnforcedReturnType.Visit(visitor);
        }
    }
}
