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
    public class HassiumDouble : HassiumObject, IConvertible
    {
        public double Value { get; set; }

        public int ValueInt
        {
            get { return Convert.ToInt32(Value); }
            set { Value = (double) value; }
        }

        public HassiumDouble(double value)
        {
            Attributes.Add("toString", new InternalFunction(tostring, 0));
            Attributes.Add("compare", new InternalFunction(compare, 1));
            Attributes.Add("isBetween", new InternalFunction(isBetween, new[] {2, 3}));
            Value = value;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public HassiumObject tostring(HassiumObject[] args)
        {
            return new HassiumString(ToString());
        }

        public HassiumObject isBetween(HassiumObject[] args)
        {
            if (args.Length == 3)
                if (args[2].HBool().Value) return Value >= args[0].HDouble().Value && Value <= args[1].HDouble().Value;
            return Value > args[0].HDouble().Value && Value < args[1].HDouble().Value;
        }


        public HassiumObject compare(HassiumObject[] args)
        {
            return new HassiumDouble(Value.CompareTo(args[0].HDouble().Value));
        }

        public static implicit operator HassiumString(HassiumDouble str)
        {
            return new HassiumString(str.Value.ToString());
        }

        #region IConvertible stuff

        public TypeCode GetTypeCode()
        {
            return TypeCode.Double;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Value == 1.0;
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
            return Value;
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