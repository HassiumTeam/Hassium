﻿using System;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumNumber: HassiumObject, IConvertible
    {
        public double Value { get; set; }

        public int ValueInt { get { return Convert.ToInt32(Value); } set { Value = (double) value; } }

        public HassiumNumber(double value)
        {
            Attributes.Add("toString", new InternalFunction(tostring));
            Attributes.Add("compare", new InternalFunction(compare));
            Attributes.Add("isBetween", new InternalFunction(isBetween));
            Attributes.Add("isLetterOrDigit", new InternalFunction(isLetterOrDigit));
            Attributes.Add("isWhiteSpace", new InternalFunction(isWhiteSpace));
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
            if(args.Length == 3) if(args[2].HBool().Value) return Value >= args[0].HNum().Value && Value <= args[1].HNum().Value;
            return Value > args[0].HNum().Value && Value < args[1].HNum().Value;
        }


        public HassiumObject compare(HassiumObject[] args)
        {
            return new HassiumNumber(Value.CompareTo(args[0].HNum().Value));
        }

        public HassiumObject isWhiteSpace(HassiumObject[] args)
        {
            return new HassiumBool(char.IsWhiteSpace(Convert.ToChar(((HassiumNumber)args[0]).ValueInt)));
        }

        public HassiumObject isLetterOrDigit(HassiumObject[] args)
        {
            return new HassiumBool(char.IsLetterOrDigit(Convert.ToChar(((HassiumNumber)args[0]).ValueInt)));
        }

        public static implicit operator HassiumString(HassiumNumber str)
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

