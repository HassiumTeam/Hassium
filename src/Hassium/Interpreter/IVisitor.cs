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

using Hassium.Parser.Ast;

namespace Hassium.Interpreter
{
    public interface IVisitor
    {
        object Accept(Expression expr);
        object Accept(ArgListNode node);
        object Accept(ArrayGetNode node);
        object Accept(ArrayIndexerNode node);
        object Accept(ArrayInitializerNode node);
        object Accept(BinOpNode node);
        object Accept(BreakNode node);
        object Accept(CaseNode node);
        object Accept(ClassNode node);
        object Accept(CodeBlock node);
        object Accept(ConditionalOpNode node);
        object Accept(ContinueNode node);
        object Accept(ForEachNode node);
        object Accept(ForNode node);
        object Accept(FuncNode node);
        object Accept(FunctionCallNode node);
        object Accept(IdentifierNode node);
        object Accept(IfNode node);
        object Accept(InstanceNode node);
        object Accept(LambdaFuncNode node);
        object Accept(MemberAccessNode node);
        object Accept(IncDecNode node);
        object Accept(NumberNode node);
        object Accept(PropertyNode node);
        object Accept(ReturnNode node);
        object Accept(StatementNode node);
        object Accept(StringNode node);
        object Accept(SwitchNode node);
        object Accept(ThreadNode node);
        object Accept(TryNode node);
        object Accept(UnaryOpNode node);
        object Accept(UncheckedNode node);
        object Accept(WhileNode node);
        object Accept(UseNode node);
        object Accept(DoNode node);
        object Accept(LabelNode node);
        object Accept(GotoNode node);
        object Accept(CharNode node);
        object Accept(EnumNode node);
        object Accept(TupleNode node);
    }
}