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

using System.Collections.Generic;
using System.Linq;
using Hassium.Parser;
using Hassium.Parser.Ast;

namespace Hassium.Semantics
{
    public class SemanticAnalyser
    {
        public AstNode Code { get; private set; }

        private SymbolTable result = new SymbolTable();
        private LocalScope currentLocalScope;

        public SemanticAnalyser(AstNode code)
        {
            Code = code;
        }

        public SymbolTable Analyse()
        {
            checkout(Code);

            return result;
        }

        private List<AstNode> flatten(List<AstNode> node)
        {
            List<AstNode> ch = new List<AstNode>();
            if (node.Count > 0 && node[0] is CodeBlock) node = ((CodeBlock) node[0]).Children;
            foreach (AstNode cur in node)
            {
                if (cur.Children.Count > 0)
                {
                    ch.AddRange(flatten(cur.Children));
                }
                ch.Add(cur);
            }
            return ch;
        }

        private void checkout(AstNode theNode)
        {
            if (theNode == null || theNode.Children == null) return;

            foreach (AstNode node in flatten(theNode.Children))
            {
                if (node is LambdaFuncNode)
                {
                    LambdaFuncNode fnode = ((LambdaFuncNode) node);
                    currentLocalScope = new LocalScope();
                    result.ChildScopes["lambda_" + fnode.GetHashCode()] = currentLocalScope;
                    currentLocalScope.Symbols.AddRange(fnode.Parameters);
                    analyseLocalCode(fnode.Body);
                }
            }

            foreach (AstNode node in theNode.Children)
            {
                if (node is BinOpNode)
                {
                    BinOpNode bnode = ((BinOpNode) node);
                    if (((BinOpNode) node).BinOp == BinaryOperation.Assignment)
                    {
                        if (!result.Symbols.Contains(bnode.Left.ToString()))
                        {
                            result.Symbols.Add(bnode.Left.ToString());
                        }
                        checkout(node);
                    }
                }
                else
                    checkout(node);
            }

            foreach (AstNode node in theNode.Children)
            {
                if (node is FuncNode)
                {
                    FuncNode fnode = ((FuncNode) node);
                    currentLocalScope = new LocalScope();
                    result.ChildScopes[fnode.Name + "`" + fnode.Parameters.Count] = currentLocalScope;
                    currentLocalScope.Symbols.AddRange(fnode.Parameters);
                    analyseLocalCode(fnode.Body);
                }
                else if (node is ClassNode)
                {
                    var cnode = ((ClassNode) node);

                    foreach (var fnode in cnode.Children[0].Children.OfType<FuncNode>().Select(pnode => pnode))
                    {
                        currentLocalScope = new LocalScope();
                        result.ChildScopes[cnode.Name + "." + fnode.Name] = currentLocalScope;
                        currentLocalScope.Symbols.AddRange(fnode.Parameters);
                        analyseLocalCode(fnode.Body);
                    }
                }
            }

            if (theNode.Children.Count > 0) foreach (AstNode x in theNode.Children) checkout(x);
        }

        private void analyseLocalCode(AstNode theNode)
        {
            foreach (AstNode node in theNode.Children)
            {
                if (node is BinOpNode)
                {
                    BinOpNode bnode = ((BinOpNode)node);
                    if (bnode.BinOp == BinaryOperation.Assignment)
                    {
                        if (!result.Symbols.Contains(bnode.Left.ToString()) &&
                            !currentLocalScope.Symbols.Contains(bnode.Left.ToString()))
                        {
                            currentLocalScope.Symbols.Add(bnode.Left.ToString());
                        }
                    }
                }
                else if (node is LabelNode)
                    currentLocalScope.Symbols.Add("label " + ((LabelNode)node).Name);
                else
                    analyseLocalCode(node);
            }
        }
    }
}