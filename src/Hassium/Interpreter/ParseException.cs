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
using Hassium.Parser;

namespace Hassium.Interpreter
{
    /// <summary>
    /// Class for parsing exceptions.
    /// </summary>
    public class ParseException : Exception
    {
        /// <summary>
        /// The node the exception is on.
        /// </summary>
        public AstNode Node { get; private set; }
        /// <summary>
        /// The position in the code the exception is on.
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Initializes a new ParseException using the message and node.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="node"></param>
        public ParseException(string message, AstNode node) : this(message, node.Position)
        {
            Node = node;
        }

        /// <summary>
        /// Initializes a new ParseException using the message and position.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="position"></param>
        public ParseException(string message, int position) : base(message)
        {
            Position = position;
        }
    }
}