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
using System.Runtime.CompilerServices;
using System.Security.Policy;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.Interpreter;
using Hassium.Parser.Ast;

namespace Hassium
{
    public class HassiumClass : HassiumObject
    {
        public HassiumClass Extends { get; private set; }

        public HassiumMethod Constructor { get; private set; }

        public bool HasConstructor { get { return Constructor != null; } }

        public ClassNode ClassNode { get; private set; }

        public Interpreter.Interpreter Interpreter { get; private set; }

        public HassiumClass(ClassNode value, Interpreter.Interpreter interpreter)
        {
            ClassNode = value;
            Interpreter = interpreter;

            if (value.Extends != "")
            {
                Extends = (HassiumClass)( interpreter.Globals.First(entry => entry.Key == value.Extends).Value);

                foreach (KeyValuePair<string, HassiumObject> attrib in Extends.Attributes)
                {
                    if (Attributes.ContainsKey(attrib.Key))
                        Attributes.Remove(attrib.Key);

                    SetAttribute(attrib.Key, attrib.Value);
                }

                if (Extends.HasConstructor) Constructor = Extends.Constructor;
            }

            var clone = (HassiumClass)MemberwiseClone();

            foreach (var fnode in value.Children[0].Children.OfType<FuncNode>().Select(node => node))
            {
                var method = new HassiumMethod(interpreter, fnode,
                    interpreter.SymbolTable.ChildScopes[value.Name + "." + fnode.Name], clone);
                if (fnode.Name == "new")
                {
                    if(fnode.CallConstructor != null)
                    {
                        var cc = fnode.CallConstructor;
                        if(cc.Target.ToString() == "this")
                        {

                        }
                        else if(cc.Target.ToString() == "base")
                        {
                            
                        }
                    }
                    Constructor = method;
                }
                SetAttribute(fnode.Name, method);
                
            }

            foreach (var pnode in value.Children[0].Children.OfType<PropertyNode>().Select(node => node))
            {
                SetAttribute(pnode.Name, (HassiumProperty) pnode.Visit(interpreter));
            }
        }

        public HassiumClass Clone()
        {
            var res = new HassiumClass(ClassNode, Interpreter);
            foreach(var attr in Attributes)
            {
                res.Attributes[attr.Key] = attr.Value;
            }
            return res;
        }
    

        public HassiumClass Instanciate(HassiumObject[] args, int pos)
        {
            var self = this;

            if ((!HasConstructor && Extends != null && Extends.HasConstructor) || Constructor.FuncNode.CallConstructor != null)
            {
                var ctor = Extends.Constructor;
                ctor.SelfReference = self;
                ctor.Invoke(args);
                var attr = (KeyValuePair<string, HassiumObject>[])(ctor.SelfReference.Attributes.ToArray().Clone());
                for (int i = 0; i < attr.Length; i++)
                {
                    var attrib = attr[i];

                    if (Attributes.ContainsKey(attrib.Key))
                        Attributes.Remove(attrib.Key);

                    SetAttribute(attrib.Key, attrib.Value);
                }
                self = (HassiumClass) ctor.SelfReference;
                if (!HasConstructor) return self;
            }
            if (HasConstructor)
            {
                Constructor.SelfReference = self;
                Constructor.Invoke(args);
                Constructor.SelfReference.IsInstance = true;
                return (HassiumClass)Constructor.SelfReference;
            }
            else
            {
                throw new ParseException("The class " + ClassNode.Name + " doesn't contain any constructor", pos);
            }
        }
    }
}