﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Parser.Ast
{
    public class ConditionalOpNode : AstNode
    {
        public AstNode Predicate
        {
            get
            {
                return this.Children[0];
            }
        }
        public AstNode Body
        {
            get
            {
                return this.Children[1];
            }
        }
        public AstNode ElseBody
        {
            get
            {
                return this.Children[2];
            }
        }

        public ConditionalOpNode(int position, AstNode predicate, AstNode body) : this(position, predicate, body, new CodeBlock(position))
        {
        }

        public ConditionalOpNode(int position, AstNode predicate, AstNode body, AstNode elseBody) : base(position)
        {
            this.Children.Add(predicate);
            this.Children.Add(body);
            this.Children.Add(elseBody);
        }
    }
}
