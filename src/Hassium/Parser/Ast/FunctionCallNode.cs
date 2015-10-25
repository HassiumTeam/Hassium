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
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    [Serializable]
    public class FunctionCallNode : AstNode
    {
        public AstNode Target
        {
            get { return Children[0]; }
        }

        public AstNode Arguments
        {
            get { return Children[1]; }
        }

        public bool CheckForNull
        {
            get { return Target is MemberAccessNode && ((MemberAccessNode) Target).CheckForNull ;
            }
        }

        public FunctionCallNode(int position, AstNode target, AstNode arguments) : base(position)
        {
            Children.Add(target);
            Children.Add(arguments);
        }

        public override object Visit(IVisitor visitor)
        {
            try
            {
                return visitor.Accept(this);
            }
            catch(Exception e)
            {
                if (e is NullReferenceException && (CheckForNull || (Target is MemberAccessNode && ((MemberAccessNode)Target).CheckForNull)
                     || (Target is FunctionCallNode && ((FunctionCallNode)Target).CheckForNull)))
                {
                    return null;
                }
                else throw;
            }
        }
    }
}