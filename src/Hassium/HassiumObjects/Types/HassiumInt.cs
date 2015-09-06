using System;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumInt: HassiumObject, IConvertible
    {
        public int Value { get; set; }

        public HassiumInt(int value)
        {
            Attributes.Add("toString", new InternalFunction(tostring));
            Attributes.Add("compare", new InternalFunction(compare));
            Attributes.Add("isBetween", new InternalFunction(isBetween));
            Value = value;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public HassiumObject tostring(HassiumObject[] args)
        {
            return ToString();
        }

        public HassiumObject isBetween(HassiumObject[] args)
        {
            if(args.Length == 3) if(args[2].HBool().Value) return Value >= args[0].HDouble().Value && Value <= args[1].HDouble().Value;
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

