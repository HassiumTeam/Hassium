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
using Hassium.HassiumObjects.Conversion;

namespace Hassium.Functions
{
    public class ConsoleFunctions : ILibrary
    {
        /// <summary>
        ///     Prints the specified string to the console.
        /// </summary>
        /// <param name="args">The string to print.</param>
        /// <returns>Nothing</returns>
        [IntFunc("print", -1)]
        public static HassiumObject Print(HassiumObject[] args)
        {
            Console.Write(string.Join("", args.Select(x => x == null ? "null" : x.ToString())));
            return null;
        }

        /// <summary>
        ///     Prints the specified string to the console (see <c>Print</c>) followed by a newline.
        /// </summary>
        /// <param name="args">The string to print.</param>
        /// <returns>Nothing</returns>
        [IntFunc("println", -1)]
        public static HassiumObject PrintLn(HassiumObject[] args)
        {
            Console.WriteLine(string.Join("", args.Select(x => x == null ? "null" : x.ToString())));
            return null;
        }

        /// <summary>
        ///     Prints the specified array to the console followed by a newline.
        /// </summary>
        /// <param name="args">The array to print.</param>
        /// <returns>Nothing</returns>
        [IntFunc("printarr", 1)]
        public static HassiumObject PrintArr(HassiumObject[] args)
        {
            Console.WriteLine(HassiumConvert.toString(new[] {args[0]}).ToString());
            return null;
        }

        /// <summary>
        ///     Prompt the user to type something and return the input.
        /// </summary>
        /// <param name="args">No parameters.</param>
        /// <returns>Nothing.</returns>
        [IntFunc("input", new[] {0, 1, 2})]
        public static HassiumObject Input(HassiumObject[] args)
        {
            if (args.Length > 0 && args[0].HBool().Value)
                return Console.ReadKey(args.Length == 2 && args[1].HBool().Value).KeyChar;
            return Console.ReadLine();
        }

        /// <summary>
        ///     Clears the console.
        /// </summary>
        /// <param name="args">No parameters.</param>
        /// <returns>Nothing.</returns>
        [IntFunc("cls", 0)]
        public static HassiumObject Cls(HassiumObject[] args)
        {
            Console.Clear();
            return null;
        }

        /// <summary>
        ///     Waits for the user to press any key.
        /// </summary>
        /// <param name="args">No parameters.</param>
        /// <returns>Nothing.</returns>
        [IntFunc("pause", 0)]
        public static HassiumObject Pause(HassiumObject[] args)
        {
            Console.ReadKey(true);
            return null;
        }
    }
}