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
        public HassiumClass(AstNode value, Interpreter.Interpreter interpreter)
        {
            foreach (AstNode node in value.Children)
            {
                if (node is FuncNode)
                {
                    var fnode = ((FuncNode)node);
                    SetAttribute(fnode.Name, new HassiumFunction(interpreter, fnode, interpreter.SymbolTable.ChildScopes[((ClassNode)value).Name + "." + fnode.Name]));
                }
            }
        }
    }
}

