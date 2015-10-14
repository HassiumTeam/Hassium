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
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Conversion
{
    public class HassiumConvert : HassiumObject
    {
        public HassiumConvert()
        {
            Attributes.Add("toDouble", new InternalFunction(toDouble, 1));
            Attributes.Add("toInt", new InternalFunction(toInt, 1));
            Attributes.Add("toString", new InternalFunction(toString, 1));
            Attributes.Add("toBool", new InternalFunction(toBool, 1));
            Attributes.Add("toChar", new InternalFunction(toChar, 1));
        }

        public static HassiumObject toDouble(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
            {
                var ret = Convert.ToDouble(((HassiumString)args[0]).Value);
                return ret == System.Math.Truncate(ret) ? new HassiumInt((int)ret) : new HassiumDouble(ret);
            }
            else if (args[0] is HassiumInt)
                return new HassiumDouble(((HassiumInt)args[0]).Value);
            else if (args[0] is HassiumDouble)
                return args[0];
            else if (args[0] is HassiumByte)
                return new HassiumDouble(Convert.ToDouble(((HassiumByte)args[0]).Value));
            else if (args[0] is HassiumChar)
                return new HassiumChar(Convert.ToChar(((HassiumChar)args[0]).Value));
            else
                throw new Exception("Unknown format for Convert.toDouble()");
        }

        public static HassiumObject toInt(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return Convert.ToInt32(((HassiumString)args[0]).Value);
            else if (args[0] is HassiumDouble)
                return new HassiumInt(((HassiumDouble)args[0]).ValueInt);
            else if (args[0] is HassiumInt)
                return args[0];
            else if (args[0] is HassiumByte)
                return new HassiumInt(Convert.ToInt32(((HassiumByte)args[0]).Value));
            else if (args[0] is HassiumChar)
                return new HassiumChar(Convert.ToChar(((HassiumChar)args[0]).Value));
            else
                throw new Exception("Unknown format for Convert.toInt()");
        }

        public static HassiumObject toString(HassiumObject[] args)
        {
            return string.Join("", args.Select(x => x == null ? "null" : x.ToString()));
        }

        public static HassiumObject toBool(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumBool(Convert.ToBoolean(((HassiumString) args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumBool(Convert.ToBoolean(((HassiumDouble) args[0]).ValueInt));
            else if (args[0] is HassiumInt)
                return new HassiumBool(Convert.ToBoolean(((HassiumInt) args[0]).Value));
            else if (args[0] is HassiumChar)
                return new HassiumChar(Convert.ToChar(((HassiumChar)args[0]).Value));
            else
                throw new Exception("Unknown format for Convert.toBool");
        }

        public static HassiumObject toChar(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumChar(Convert.ToChar(((HassiumString)args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumChar(Convert.ToChar(((HassiumDouble)args[0]).ValueInt));
            else if (args[0] is HassiumInt)
                return new HassiumChar(Convert.ToChar(((HassiumInt)args[0]).Value));
            else if (args[0] is HassiumChar)
                return args[0];
            else
                throw new Exception("Unknown format for Convert.toBool");
        }
    }
}