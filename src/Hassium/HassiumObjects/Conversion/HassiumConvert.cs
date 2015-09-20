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
            Attributes.Add("toNumber", new InternalFunction(toNumber, 1));
            Attributes.Add("toString", new InternalFunction(toString, 1));
            Attributes.Add("toBool", new InternalFunction(toBool, 1));
        }

        public static HassiumObject toNumber(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
            {
                var ret = Convert.ToDouble(((HassiumString) args[0]).Value);
                return ret == System.Math.Truncate(ret) ? new HassiumInt((int) ret) : new HassiumDouble(ret);
            }
            else if (args[0] is HassiumInt)
            {
                return new HassiumDouble(((HassiumInt) args[0]).Value);
            }
            else if (args[0] is HassiumDouble)
            {
                return new HassiumInt(((HassiumDouble) args[0]).ValueInt);
            }
            else
            {
                throw new Exception("Unknown format for Convert.toNumber");
            }
        }

        public static HassiumObject toString(HassiumObject[] args)
        {
            return string.Join("", args.Select(x => x.ToString()));
        }

        public static HassiumObject toBool(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumBool(Convert.ToBoolean(((HassiumString) args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumBool(Convert.ToBoolean(((HassiumDouble) args[0]).ValueInt));
            else if (args[0] is HassiumInt)
                return new HassiumBool(Convert.ToBoolean(((HassiumInt) args[0]).Value));
            else
                throw new Exception("Unknown format for Convert.toBool");
        }
    }
}