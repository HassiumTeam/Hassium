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
using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumBinaryReader : HassiumObject
    {
        public BinaryReader Value { get; private set; }

        public HassiumBinaryReader(BinaryReader value)
        {
            Value = value;
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("peekChar", new InternalFunction(peekChar, 0));
            Attributes.Add("read", new InternalFunction(read, 0));
            Attributes.Add("readBoolean", new InternalFunction(readBoolean, 0));
            Attributes.Add("readByte", new InternalFunction(readByte, 0));
            Attributes.Add("readString", new InternalFunction(readString, 0));
            Attributes.Add("readChars", new InternalFunction(readChars, 1));
            Attributes.Add("toString", new InternalFunction(toString, 0));
        }

        public HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        public HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();
            return null;
        }

        public HassiumObject peekChar(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(Value.PeekChar()));
        }

        public HassiumObject read(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(Value.Read()));
        }

        public HassiumObject readBoolean(HassiumObject[] args)
        {
            return new HassiumBool(Value.ReadBoolean());
        }

        public HassiumObject readByte(HassiumObject[] args)
        {
            return new HassiumByte(Value.ReadByte());
        }

        public HassiumObject readChars(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadChars(((HassiumInt) args[0])).ToString());
        }

        public HassiumObject readString(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadString());
        }

        public HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}