using System.Collections.Generic;
using System.Linq;
using Hassium.HassiumObjects;
using Hassium.Interpreter;
using Hassium.Parser.Ast;

namespace Hassium
{
    public class HassiumClass: HassiumObject
    {
        public HassiumClass(ClassNode value, Interpreter.Interpreter interpreter)
        {
            if (value.Extends != "")
            {
                foreach (
                    KeyValuePair<string, HassiumObject> attrib in
                        interpreter.Globals.Where(entry => entry.Key.StartsWith(value.Extends))
                            .SelectMany(entry => ((HassiumClass) entry.Value).Attributes))
                {
                    if (Attributes.ContainsKey(attrib.Key))
                        Attributes.Remove(attrib.Key);

                    SetAttribute(attrib.Key, attrib.Value);
                }
            }

            foreach (var fnode in value.Children[0].Children.OfType<FuncNode>().Select(node => node))
            {
                SetAttribute(fnode.Name,
                    new HassiumMethod(interpreter, fnode,
                        interpreter.SymbolTable.ChildScopes[((ClassNode) value).Name + "." + fnode.Name], this));
            }

            foreach(var pnode in value.Children[0].Children.OfType<PropertyNode>().Select(node => node))
            {
                SetAttribute(pnode.Name, (HassiumProperty)pnode.Visit(interpreter));
            }
        }
    }
}

