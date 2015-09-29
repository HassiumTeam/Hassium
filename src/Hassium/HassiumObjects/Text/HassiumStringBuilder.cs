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

using System.Text;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumStringBuilder : HassiumObject
    {
        public StringBuilder Value { get; set; }

        public HassiumStringBuilder(StringBuilder value)
        {
            Value = value;
            Attributes.Add("append", new InternalFunction(append, 1));
            Attributes.Add("appendLine", new InternalFunction(appendLine, 1));
            Attributes.Add("clear", new InternalFunction(clear, 0));
            Attributes.Add("insert", new InternalFunction(insert, 2));
            Attributes.Add("remove", new InternalFunction(remove, 2));
            Attributes.Add("replace", new InternalFunction(replace, 2));
            Attributes.Add("toString", new InternalFunction(toString, 0));
            Attributes.Add("length", new InternalFunction(x => Value.Length, 0, true));
        }

        private HassiumObject append(HassiumObject[] args)
        {
            Value.Append(args[0].ToString());
            return null;
        }

        private HassiumObject appendLine(HassiumObject[] args)
        {
            Value.AppendLine(args[0].ToString());
            return null;
        }

        private HassiumObject clear(HassiumObject[] args)
        {
            Value.Clear();
            return null;
        }

        private HassiumObject insert(HassiumObject[] args)
        {
            Value.Insert(args[0].HInt().Value, args[1].ToString());
            return null;
        }

        private HassiumObject remove(HassiumObject[] args)
        {
            Value.Remove(args[0].HInt().Value, args[1].HInt().Value);
            return null;
        }

        private HassiumObject replace(HassiumObject[] args)
        {
            Value.Replace(args[0].ToString(), args[1].ToString());
            return null;
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return Value.ToString();
        }
    }
}