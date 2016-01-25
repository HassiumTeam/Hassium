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
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Interpreter;
using Hassium.HassiumObjects.Random;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;

namespace Hassium.Functions
{
    /// <summary>
    /// Class containing global constructors.
    /// </summary>
    public class Constructors : ILibrary
    {
        /// <summary>
        /// Returns a blank HassiumObject.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumObject</returns>
        [IntFunc("Object", true, 0)]
        public static HassiumObject Object(HassiumObject[] args)
        {
            return new HassiumObject();
        }

        /// <summary>
        /// Returns a new HassiumDate.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumDate.</returns>
        [IntFunc("Date", true, new []{0,1})]
        public static HassiumObject Date(HassiumObject[] args)
        {
            if(args.Length == 1) return new HassiumDate(new DateTime(1970, 1, 1).AddSeconds(args[0].HDouble()));
            return new HassiumDate(DateTime.Now);
        }

        /// <summary>
        /// Returns a new array.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumArray</returns>
        [IntFunc("Array", true, 1)]
        public static HassiumObject Array(HassiumObject[] args)
        {
            return args.Length == 0
                ? new HassiumArray(new List<HassiumObject>())
                : new HassiumArray(new HassiumObject[args[0].HInt().Value]);
        }

        /// <summary>
        /// Returns a new HassiumRandom.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumRandom</returns>
        [IntFunc("Random", true, new[] {0, 1})]
        public static HassiumObject Random(HassiumObject[] args)
        {
            return args.Length > 0
                ? new HassiumRandom(new Random(args[0].HInt().Value))
                : new HassiumRandom(new Random());
        }

        /// <summary>
        /// Returns a new Event.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumEvent</returns>
        [IntFunc("Event", true, -1)]
        public static HassiumObject Event(HassiumObject[] args)
        {
            var ret = new HassiumEvent();
            if (args.Length > 0)
            {
                args.All(x =>
                {
                    if (x is HassiumMethod) ret.AddHandler((HassiumMethod) x);
                    return true;
                });
            }
            return ret;
        }

        /// <summary>
        /// Returns a new HassiumStopWatch.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumStopWatch</returns>
        [IntFunc("StopWatch", true, 0)]
        public static HassiumObject StopWatch(HassiumObject[] args)
        {
            return new HassiumStopWatch();
        }

        /// <summary>
        /// Returns a new HassiumString.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumString</returns>
        [IntFunc("String", true, new []{0,1})]
        public static HassiumObject String(HassiumObject[] args)
        {
            string initialValue = "";

            if (args.Length >= 1)
                initialValue = args[0].ToString();

            return new HassiumString(initialValue);
        }

        /// <summary>
        /// Returns a new HassiumChar.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumChar</returns>
        [IntFunc("Char", true, new[] { 0, 1 })]
        public static HassiumObject Char(HassiumObject[] args)
        {
            char initialValue = ' ';

            if (args.Length >= 1)
                initialValue = args[0].HChar();

            return new HassiumChar(initialValue);
        }

        /// <summary>
        /// Returns a new HassiumInt.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumInt</returns>
        [IntFunc("Int", true, new[] { 0, 1 })]
        public static HassiumObject Int(HassiumObject[] args)
        {
            int initialValue = 0;

            if (args.Length >= 1)
                initialValue = args[0].HInt();

            return new HassiumInt(initialValue);
        }

        /// <summary>
        /// Returns a new HassiumDouble.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>HassiumDouble</returns>
        [IntFunc("Double", true, new[] { 0, 1 })]
        public static HassiumObject Double(HassiumObject[] args)
        {
            double initialValue = 0;

            if (args.Length >= 1)
                initialValue = args[0].HDouble();

            return new HassiumDouble(initialValue);
        }
    }
}