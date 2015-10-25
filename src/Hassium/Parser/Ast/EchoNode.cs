﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    [Serializable]
    public class EchoNode : AstNode
    {
        public string Content { get; private set; }

        public EchoNode(int pos, string content) : base(pos)
        {
            Content = content;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}
