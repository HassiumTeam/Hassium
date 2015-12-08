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

using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumStreamReader : HassiumObject
    {
        public StreamReader Value { get; set; }

        public HassiumStreamReader(HassiumObject value)
        {
            if (value is HassiumStream)
                Value = new StreamReader(((HassiumStream)value).Value);
            else if (value is HassiumSslStream)
                Value = new StreamReader(((HassiumSslStream)value).Value);
            else
                Value = new StreamReader(value.ToString());

            Attributes.Add("readLine", new InternalFunction(readLine, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("endOfStream", new InternalFunction(endOfStream, 0));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("peek", new InternalFunction(peek, 0));
            Attributes.Add("read", new InternalFunction(read, 0));
            Attributes.Add("readToEnd", new InternalFunction(readToEnd, 0));
        }

        private HassiumObject readLine(HassiumObject[] args)
        {
            return Value.ReadLine();
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();
            return null;
        }

        private HassiumObject peek(HassiumObject[] args)
        {
            return new HassiumChar(((char) Value.Peek()));
        }

        private HassiumObject endOfStream(HassiumObject[] args)
        {
            return new HassiumBool(Value.EndOfStream);
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumChar(((char) Value.Read()));
        }

        private HassiumObject readToEnd(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadToEnd());
        }
    }
}