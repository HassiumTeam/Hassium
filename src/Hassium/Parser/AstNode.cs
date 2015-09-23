// Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//  * Redistributions of source code must retain the above copyright notice, this list
//    of conditions and the following disclaimer.
// 
//  * Redistributions in binary form must reproduce the above copyright notice, this
//    list of conditions and the following disclaimer in the documentation and/or
//    other materials provided with the distribution.
// Neither the name of the copyright holder nor the names of its contributors may be
// used to endorse or promote products derived from this software without specific
// prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
// DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.Interpreter;
using Hassium.Parser.Ast;

namespace Hassium.Parser
{
    public abstract class AstNode
    {
        public List<AstNode> Children { get; private set; }

        public int Position { get; set; }

        public bool CanBeIndexed
        {
            get
            {
                return this is IdentifierNode || this is MemberAccessNode || this is FunctionCallNode ||
                       this is StringNode ||
                       this is InstanceNode || this is ArrayInitializerNode || this is ArrayGetNode;
            }
        }

        public bool ReturnsValue
        {
            get
            {
                return this is IdentifierNode || this is MemberAccessNode || this is FunctionCallNode ||
                       this is StringNode ||
                       this is InstanceNode || this is ArrayInitializerNode || this is ArrayGetNode || this is BinOpNode ||
                       this is ConditionalOpNode || this is LambdaFuncNode || this is NumberNode || this is UnaryOpNode ||
                       (this is CodeBlock && Children.Any(x => x.ReturnsValue));
            }
        }

        public bool Any(Func<AstNode, bool> fc)
        {
            return Children.Any(node => node.Children.Count > 0 && (fc(node) || node.Any(fc)));
        }

        public abstract object Visit(IVisitor visitor);

        public void VisitChild(IVisitor visitor)
        {
            Children.All(x =>
            {
                x.Visit(visitor);
                return true;
            });
        }

        public bool CanBeModified
        {
            get { return this is IdentifierNode || this is MemberAccessNode || this is ArrayGetNode; }
        }

        protected AstNode() : this(-1)
        {
        }

        protected AstNode(int position)
        {
            Children = new List<AstNode>();
            Position = position;
        }
    }
}