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
using System.Linq;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumStream : HassiumObject
    {
        public Stream Value { get; protected set; }


        public HassiumStream(Stream value)
        {
            Value = value;
            Attributes.Add("length", new InternalFunction(x => Value.Length, 0, true));
            Attributes.Add("position", new InternalFunction(x => Value.Position, 0, true));
            Attributes.Add("canRead", new InternalFunction(x => Value.CanRead, 0, true));
            Attributes.Add("canWrite", new InternalFunction(x => Value.CanWrite, 0, true));
            Attributes.Add("canSeek", new InternalFunction(x => Value.CanSeek, 0, true));
            Attributes.Add("flush", new InternalFunction(Flush, 0));
            Attributes.Add("close", new InternalFunction(Close, 0));
            Attributes.Add("read", new InternalFunction(Read, 0));
            Attributes.Add("seek", new InternalFunction(Seek, new[] {0, 1}));
            Attributes.Add("write", new InternalFunction(Write, 1));
            Attributes.Add("readLine", new InternalFunction(ReadLine, 0));
        }

        public HassiumObject Flush(HassiumObject[] args)
        {
            Value.Flush();
            return null;
        }

        public HassiumObject Close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        public HassiumObject Read(HassiumObject[] args)
        {
            return Value.ReadByte();
        }

        public HassiumObject ReadLine(HassiumObject[] args)
        {
            var builder = new StringBuilder();
            var c = 0;
            while ((c = Value.ReadByte()) != '\n' && c != '\r' && c != -1)
            {
                builder.Append((char)c);
            }
            return builder.ToString();
        }

        public HassiumObject Seek(HassiumObject[] args)
        {
            return Value.Seek(args.Length == 1 ? args[0].HInt().Value : 0, SeekOrigin.Begin);
        }

        public HassiumObject Write(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
            {
                var buffer = args[0].ToString().ToCharArray().Select(x => (byte) x).ToArray();
                Value.Write(buffer, 0, buffer.Length);
            }
            else Value.WriteByte((byte) args[0].HDouble().ValueInt);
            return null;
        }
    }
}