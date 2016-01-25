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
using Hassium.HassiumObjects;
using Hassium.Semantics;

namespace Hassium.Interpreter
{
    /// <summary>
    /// Class for the StackFrame.
    /// </summary>
    public class StackFrame
    {
        /// <summary>
        /// Implements 'this'.
        /// </summary>
        public HassiumObject Self
        {
            get { return Locals.ContainsKey("this") ? Locals["this"] : null; }
        }
        /// <summary>
        /// The LocalScope the stack frame is in.
        /// </summary>
        public LocalScope Scope { get; private set; }

        /// <summary>
        /// Dictionary containing the local variables.
        /// </summary>
        public Dictionary<string, HassiumObject> Locals { get; private set; }

        /// <summary>
        /// Dictionary containing the labels.
        /// </summary>
        public Dictionary<string, int> Labels { get; private set; } 

        /// <summary>
        /// The return value.
        /// </summary>
        public HassiumObject ReturnValue { get; set; }

        /// <summary>
        /// Initializes a new StackFrame using the scope and optional self.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="self"></param>
        public StackFrame(LocalScope scope, HassiumObject self = null)
        {
            Scope = scope;
            Locals = new Dictionary<string, HassiumObject>();
            Labels = new Dictionary<string, int>();
            if (self != null)
                Locals["this"] = self;
        }
    }
}