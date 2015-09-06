using System;
using Hassium;
using System.Collections.Generic;
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
        public HassiumClass(ClassNode value, Interpreter.Interpreter interpreter)
        {
            if (value.Extends != "")
            {
                foreach (KeyValuePair<string, HassiumObject> entry in interpreter.Globals)
                {
                    if (entry.Key.StartsWith(value.Extends))
                    {
                        foreach (KeyValuePair<string, HassiumObject> attrib in ((HassiumClass)entry.Value).Attributes)
                            SetAttribute(attrib.Key, attrib.Value);
                    }
                }
            }

            foreach (AstNode node in value.Children[0].Children)
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

