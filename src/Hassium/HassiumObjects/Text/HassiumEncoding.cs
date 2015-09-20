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

using System.Linq;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumEncoding : HassiumObject
    {
        public Encoding Value { get; private set; }

        public HassiumEncoding(HassiumString type)
        {
            switch (type.Value.ToUpper())
            {
                case "UTF8":
                    Value = Encoding.UTF8;
                    break;
                case "UTF7":
                    Value = Encoding.UTF7;
                    break;
                case "UTF32":
                    Value = Encoding.UTF32;
                    break;
                case "UNICODE":
                    Value = Encoding.Unicode;
                    break;
                default:
                    Value = Encoding.ASCII;
                    break;
            }
            Attributes.Add("bodyName", new InternalFunction(bodyName, 0, true));
            Attributes.Add("headerName", new InternalFunction(headerName, 0, true));
            Attributes.Add("getChar", new InternalFunction(getChar, 1));
            Attributes.Add("getByte", new InternalFunction(getByte, 1));
            Attributes.Add("getBytes", new InternalFunction(getBytes, 1));
        }

        public HassiumEncoding(Encoding type)
        {
            Value = type;
            Attributes.Add("bodyName", new InternalFunction(bodyName, 0, true));
            Attributes.Add("headerName", new InternalFunction(headerName, 0, true));
        }

        private HassiumObject bodyName(HassiumObject[] args)
        {
            return new HassiumString(Value.BodyName);
        }

        private HassiumObject headerName(HassiumObject[] args)
        {
            return new HassiumString(Value.HeaderName);
        }

        private HassiumObject getChar(HassiumObject[] args)
        {
            return Value.GetChars(new[] {(byte) args[0].HInt().Value})[0].ToString();
        }

        private HassiumObject getBytes(HassiumObject[] args)
        {
            return Value.GetBytes(args[0].HString().Value).Select(x => new HassiumInt(x)).ToArray();
        }

        private HassiumObject getByte(HassiumObject[] args)
        {
            return (int) (Value.GetBytes(args[0].HString().Value)[0]);
        }
    }
}