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
using System.Web;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumString : HassiumObject, IConvertible
    {
        protected bool Equals(HassiumString other)
        {
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public string Value { get; private set; }

        public HassiumString(string value)
        {
            Value = value;
            Attributes.Add("toArray", new InternalFunction(toArray, 0));
            Attributes.Add("toLower", new InternalFunction(tolower, 0));
            Attributes.Add("toUpper", new InternalFunction(toupper, 0));
            Attributes.Add("begins", new InternalFunction(begins, 1));
            Attributes.Add("ends", new InternalFunction(ends, 1));
            Attributes.Add("getAt", new InternalFunction(getat, 1));
            Attributes.Add("substring", new InternalFunction(substr, new[] {1, 2}));
            Attributes.Add("concat", new InternalFunction(concat, 1));
            Attributes.Add("contains", new InternalFunction(contains, 1));
            Attributes.Add("split", new InternalFunction(split, 1));
            Attributes.Add("replace", new InternalFunction(replace, 2));
            Attributes.Add("index", new InternalFunction(index, 1));
            Attributes.Add("isWhiteSpace", new InternalFunction(isWhiteSpace, 0));
            Attributes.Add("lastIndex", new InternalFunction(lastindex, 1));
            Attributes.Add("padLeft", new InternalFunction(padleft, 1));
            Attributes.Add("padRight", new InternalFunction(padright, 1));
            Attributes.Add("trim", new InternalFunction(trim, 0));
            Attributes.Add("trimLeft", new InternalFunction(trimleft, 0));
            Attributes.Add("trimRight", new InternalFunction(trimright, 0));
            Attributes.Add("toString", new InternalFunction(tostring, 0));
            Attributes.Add("urlEncode", new InternalFunction(urlEncode, 0));
            Attributes.Add("urlDecode", new InternalFunction(urlDecode, 0));
            Attributes.Add("length", new InternalFunction(x => Value.Length, 0, true));
        }

        public static implicit operator HassiumArray(HassiumString s)
        {
            return new HassiumArray(s.Value);
        }

        public static implicit operator HassiumString(HassiumChar c)
        {
            return new HassiumString(c.ToString());
        }

        private HassiumObject toArray(HassiumObject[] args)
        {
            return Value.ToCharArray().Select(x => new HassiumChar(x)).ToArray();
        }

        private HassiumObject tolower(HassiumObject[] args)
        {
            return Value.ToLower();
        }

        private HassiumObject toupper(HassiumObject[] args)
        {
            return Value.ToUpper();
        }

        private HassiumObject begins(HassiumObject[] args)
        {
            return Value.StartsWith(args[0].ToString());
        }

        private HassiumObject ends(HassiumObject[] args)
        {
            return Value.EndsWith(args[0].ToString());
        }

        private HassiumObject getat(HassiumObject[] args)
        {
            return Value[args[0].HInt().Value].ToString();
        }

        private HassiumObject substr(HassiumObject[] args)
        {
            return args.Length == 2
                ? Value.Substring(args[0].HInt().Value, args[1].HInt().Value)
                : Value.Substring(args[0].HInt().Value);
        }

        private HassiumObject concat(HassiumObject[] args)
        {
            return Value + args[0];
        }

        private HassiumObject contains(HassiumObject[] args)
        {
            return Value.Contains(args[0].ToString());
        }

        private HassiumObject split(HassiumObject[] args)
        {
            return new HassiumArray(Value.Split(args[0].HChar()));
        }

        private HassiumObject index(HassiumObject[] args)
        {
            return Value.IndexOf(args[0].ToString());
        }

        private HassiumObject lastindex(HassiumObject[] args)
        {
            return Value.LastIndexOf(args[0].ToString(), StringComparison.Ordinal);
        }

        private HassiumObject padleft(HassiumObject[] args)
        {
            return Value.PadLeft(args[0].HInt().Value);
        }

        private HassiumObject padright(HassiumObject[] args)
        {
            return Value.PadRight(args[0].HInt().Value);
        }

        private HassiumObject replace(HassiumObject[] args)
        {
            return Value.Replace(args[0].ToString(), args[1].ToString());
        }

        private HassiumObject trim(HassiumObject[] args)
        {
            return Value.Trim();
        }

        private HassiumObject trimleft(HassiumObject[] args)
        {
            return Value.TrimStart();
        }

        private HassiumObject trimright(HassiumObject[] args)
        {
            return Value.TrimEnd();
        }

        private HassiumObject tostring(HassiumObject[] args)
        {
            return Value;
        }

        private HassiumObject isWhiteSpace(HassiumObject[] args)
        {
            return char.IsWhiteSpace(Convert.ToChar(Value));
        }

        private HassiumObject urlEncode(HassiumObject[] args)
        {
            return HttpUtility.UrlEncode(Value);
        }

        private HassiumObject urlDecode(HassiumObject[] args)
        {
            return HttpUtility.UrlDecode(Value);
        }

        public static implicit operator HassiumDouble(HassiumString str)
        {
            return new HassiumDouble(Convert.ToDouble(str.Value));
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.ToString() == Value;
        }

        #region IConvertible stuff

        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Value);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return Value;
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        public override string ToString()
        {
            return Value;
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value);
        }

        #endregion
    }
}