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
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumChar : HassiumObject, IConvertible
    {
        protected bool Equals(HassiumChar other)
        {
            return Value == other.Value;
        }

        protected bool Equals(HassiumString other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((HassiumChar) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public char Value { get; private set; }

        public HassiumChar(char value)
        {
            Value = value;
            Attributes.Add("toInt", new InternalFunction(toInt, 0));
            Attributes.Add("toDouble", new InternalFunction(toDouble, 0));
            Attributes.Add("toByte", new InternalFunction(toByte, 0));
            Attributes.Add("toBool", new InternalFunction(toBool, 0));
        }

        private HassiumObject toInt(HassiumObject[] args)
        {
            return new HassiumInt(Convert.ToInt32(Value));
        }

        private HassiumObject toDouble(HassiumObject[] args)
        {
            return new HassiumDouble(Convert.ToDouble(Value));
        }

        private HassiumObject toBool(HassiumObject[] args)
        {
            return new HassiumBool(Convert.ToBoolean(Value));
        }

        private HassiumObject toByte(HassiumObject[] args)
        {
            byte[] text = BitConverter.GetBytes(Value);
            HassiumByte[] bytes = new HassiumByte[text.Length];
            for (int x = 0; x < text.Length; x++)
                bytes[x] = new HassiumByte(text[x]);

            return new HassiumArray(bytes);
        }

        public static bool operator ==(HassiumChar a, HassiumChar b)
        {
            return a.Value == b.Value;
        }

        public static bool operator ==(HassiumChar a, HassiumString b)
        {
            return b.Value.Length == 1 && a.Value == b.Value[0];
        }

        public static bool operator !=(HassiumChar a, HassiumString b)
        {
            return !(a == b);
        }

        public static bool operator !=(HassiumChar a, HassiumChar b)
        {
            return a.Value != b.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator HassiumString(HassiumChar c)
        {
            return new HassiumString(c.ToString());
        }

        #region IConvertible stuff

        public TypeCode GetTypeCode()
        {
            return TypeCode.Char;
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
            return Value;
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return Value.ToString();
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