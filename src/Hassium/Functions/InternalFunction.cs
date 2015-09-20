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
using System.Linq;
using Hassium.HassiumObjects;

namespace Hassium.Functions
{
    public delegate HassiumObject HassiumFunctionDelegate(params HassiumObject[] arguments);

    public class InternalFunction : HassiumObject
    {
        private HassiumFunctionDelegate target;

        public InternalFunction(HassiumFunctionDelegate target, int args, bool prop = false, bool constr = false)
        {
            this.target = target;
            IsProperty = prop;
            IsConstructor = constr;
            Arguments = new[] {args};
        }

        public InternalFunction(HassiumFunctionDelegate target, int[] args, bool prop = false, bool constr = false)
        {
            this.target = target;
            IsProperty = prop;
            IsConstructor = constr;
            Arguments = args;
        }

        public bool IsProperty { get; set; }

        public bool IsConstructor { get; set; }

        public int[] Arguments { get; set; }


        public override string ToString()
        {
            return string.Format("[InternalFunction: {0}`{1}]", target.Method.Name,
                target.Method.GetParameters().Count());
        }

        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            if (!Arguments.Contains(args.Length) && Arguments[0] != -1)
                throw new Exception("Function " + target.Method.Name + " has " + Arguments.Max() +
                                    " arguments, but is invoked with " + args.Length);
            return target(args);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class IntFunc : Attribute
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool Constructor { get; set; }
        public int[] Arguments { get; set; }

        public IntFunc(string name, int args) : this(name, args, "")
        {
        }

        public IntFunc(string name, int[] args) : this(name, args, "")
        {
        }

        public IntFunc(string name, int args, string alias)
        {
            Name = name;
            Alias = alias;
            Constructor = false;
            Arguments = new[] {args};
        }

        public IntFunc(string name, int[] args, string alias)
        {
            Name = name;
            Alias = alias;
            Constructor = false;
            Arguments = args;
        }

        public IntFunc(string name, bool constr, int args)
        {
            Name = name;
            Alias = "";
            Constructor = constr;
            Arguments = new[] {args};
        }

        public IntFunc(string name, bool constr, int[] args)
        {
            Name = name;
            Alias = "";
            Constructor = constr;
            Arguments = args;
        }
    }
}