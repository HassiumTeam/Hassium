using System;
using System.Collections.Generic;
using System.Text;

namespace Hassium.Compiler.Parser.Ast
{
    public class FuncNode: AstNode
    {
        public string Name { get; private set; }
        public List<FuncParameter> Parameters { get; private set; }
        public AstNode Body { get { return Children[0]; } }
        public string ReturnType { get; private set; }
        public bool EnforcesReturn { get { return ReturnType != string.Empty; } }

        public FuncNode(SourceLocation location, string name, List<FuncParameter> parameters, AstNode body, string returnType = "")
        {
            this.SourceLocation = location;
            Name = name;
            Parameters = parameters;
            Children.Add(body);
            ReturnType = returnType;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }

        public string GetSourceRepresentation()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("func {0} (", Name);
            if (Parameters.Count > 0)
            {
                sb.Append(Parameters[0].GetSourceRepresentation());
                for (int i = 1; i < Parameters.Count; i++)
                    sb.AppendFormat(", {0}", Parameters[i].GetSourceRepresentation());
            }
            sb.Append(")");
            return sb.ToString();
        }
    }

    public class FuncParameter
    {
        public bool IsEnforced { get { return Type != string.Empty; } }
        public bool IsVariadic { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }

        public FuncParameter(string name, bool isVariadic = false)
        {
            Name = name;
            Type = string.Empty;
            IsVariadic = isVariadic;
        }
        public FuncParameter(string name, string type)
        {
            Name = name;
            Type = type;
            IsVariadic = false;
        }

        public string GetSourceRepresentation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            if (IsEnforced)
                sb.AppendFormat(" : {0}", Type);
            return sb.ToString();
        }
    }
}