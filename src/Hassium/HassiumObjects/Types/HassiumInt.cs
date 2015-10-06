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
using System.Security.Cryptography.X509Certificates;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumInt : HassiumObject, IConvertible
    {
        protected bool Equals(HassiumInt other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((HassiumInt) obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public int Value { get; set; }

        public HassiumInt(int value)
        {
            Attributes.Add("compare", new InternalFunction(compare, 1));
            Attributes.Add("isBetween", new InternalFunction(isBetween, new[] {2, 3}));
            Attributes.Add("toInt", new InternalFunction(toInt, 0));
            Attributes.Add("toDouble", new InternalFunction(toDouble, 0));
            Attributes.Add("toByte", new InternalFunction(toByte, 0));
            Attributes.Add("toBool", new InternalFunction(toBool, 0));
            Attributes.Add("max", new InternalFunction(max, 1));
            Attributes.Add("min", new InternalFunction(min, 1));
            Value = value;
        }

        public HassiumObject max(HassiumObject[] args)
        {
            return System.Math.Min(Value, args[0].HInt().Value);
        }

        public HassiumObject min(HassiumObject[] args)
        {
            return System.Math.Max(Value, args[0].HInt().Value);
        }

        public static bool operator ==(HassiumInt a, HassiumInt b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(HassiumInt a, HassiumInt b)
        {
            return a.Value != b.Value;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
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

        public HassiumObject isBetween(HassiumObject[] args)
        {
            if (args.Length == 3)
                if (args[2].HBool().Value) return Value >= args[0].HDouble().Value && Value <= args[1].HDouble().Value;
            return Value > args[0].HDouble().Value && Value < args[1].HDouble().Value;
        }


        public HassiumObject compare(HassiumObject[] args)
        {
            return Value.CompareTo(args[0].HDouble().Value);
        }

        public static implicit operator HassiumString(HassiumInt str)
        {
            return new HassiumString(str.Value.ToString());
        }

        public static implicit operator HassiumDouble(HassiumInt val)
        {
            return new HassiumDouble(val.Value);
        }

        #region IConvertible stuff

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Value == 1;
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
            return Value;
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

        string IConvertible.ToString(IFormatProvider provider)
        {
            return Convert.ToString(Value);
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