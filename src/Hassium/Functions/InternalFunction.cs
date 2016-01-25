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
    /// <summary>
    /// Delegate for HassiumFunctions.
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns>Return from function.</returns>
    public delegate HassiumObject HassiumFunctionDelegate(params HassiumObject[] arguments);

    /// <summary>
    /// Class that defines a Hassium function.
    /// </summary>
    public class InternalFunction : HassiumObject
    {
        private HassiumFunctionDelegate target;

        /// <summary>
        /// Determines if the function is static.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Initializes a new InternalFunction using the target args, prop, constr, and stati.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <param name="prop"></param>
        /// <param name="constr"></param>
        /// <param name="stati"></param>
        public InternalFunction(HassiumFunctionDelegate target, int args, bool prop = false, bool constr = false, bool stati = false) : this(target, new []{args}, prop, constr, stati)
        {
        }

        /// <summary>
        /// Initializes a new InternalFunction using the target args, prop, constr, and stati.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <param name="prop"></param>
        /// <param name="constr"></param>
        /// <param name="stati"></param>
        public InternalFunction(HassiumFunctionDelegate target, int[] args, bool prop = false, bool constr = false, bool stati = false)
        {
            this.target = target;
            IsProperty = prop;
            IsConstructor = constr;
            Arguments = args;
            IsStatic = stati;
        }

        /// <summary>
        /// Determines if function is a property.
        /// </summary>
        public bool IsProperty { get; set; }

        /// <summary>
        /// Determines if function is a constructor.
        /// </summary>
        public bool IsConstructor { get; set; }

        /// <summary>
        /// The number of arguments for the function.
        /// </summary>
        public int[] Arguments { get; set; }

        /// <summary>
        /// Returns a string representation of the function.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[InternalFunction: {0}`{1}{2}]", target.Method.Name,
                target.Method.GetParameters().Length, IsStatic ? " Static" : "");
        }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            if (!Arguments.Contains(args.Length) && Arguments[0] != -1)
                throw new Exception("Function " + target.Method.Name + " has " + Arguments.Max() +
                                    " arguments, but is invoked with " + args.Length);
            return target(args);
        }
    }

    /// <summary>
    /// Class for internal function attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IntFunc : Attribute
    {
        /// <summary>
        /// Function name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Function alias.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Determines if the function is a constructor.
        /// </summary>
        public bool Constructor { get; set; }

        /// <summary>
        /// The number of arguments for the function.
        /// </summary>
        public int[] Arguments { get; set; }

        /// <summary>
        /// Initializes a new IntFunc with the name and args.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public IntFunc(string name, int args) : this(name, args, "")
        {
        }

        /// <summary>
        /// Initializes a new IntFunc with the name and args.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public IntFunc(string name, int[] args) : this(name, args, "")
        {
        }

        /// <summary>
        /// Initializes a new IntFunc with the name, args, and alias.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <param name="alias"></param>
        public IntFunc(string name, int args, string alias)
        {
            Name = name;
            Alias = alias;
            Constructor = false;
            Arguments = new[] {args};
        }

        /// <summary>
        /// Initializes a new IntFunc with the name, args, and alias.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <param name="alias"></param>
        public IntFunc(string name, int[] args, string alias)
        {
            Name = name;
            Alias = alias;
            Constructor = false;
            Arguments = args;
        }

        /// <summary>
        /// Initializes a new IntFunc with the name, constr, and args.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="constr"></param>
        /// <param name="args"></param>
        public IntFunc(string name, bool constr, int args)
        {
            Name = name;
            Alias = "";
            Constructor = constr;
            Arguments = new[] {args};
        }

        /// <summary>
        /// Initializes a new IntFunc with the name, constr, and args.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="constr"></param>
        /// <param name="args"></param>
        public IntFunc(string name, bool constr, int[] args)
        {
            Name = name;
            Alias = "";
            Constructor = constr;
            Arguments = args;
        }
    }
}