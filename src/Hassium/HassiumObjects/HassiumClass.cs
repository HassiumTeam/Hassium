using System;
using Hassium;
using Hassium.Semantics;
using Hassium.Parser;
using Hassium.Parser.Ast;
using Hassium.Interpreter;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium
{
    public class HassiumClass: HassiumObject
    {
        public HassiumClass(ClassNode value, Interpreter.Interpreter interpreter, LocalScope scope)
        {
            foreach (AstNode node in value.Children)
            {
                if (node is FuncNode)
                {
                    var fnode = ((FuncNode)node);
                    this.Attributes.Add(fnode.Name, new HassiumFunction(interpreter, fnode, scope));
                }
            }
        }
    }
}

