using System;

namespace Hassium
{
    public class HassiumString: HassiumObject, IConvertible
    {
        public string Value { get; private set; }

        public HassiumString(string value)
        {
            this.Value = value;
            this.Attributes.Add("tolower", new InternalFunction(tolower));
            this.Attributes.Add("toupper", new InternalFunction(toupper));
            this.Attributes.Add("begins", new InternalFunction(begins));
        }

        private HassiumObject tolower(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.ToLower());
        }

        private HassiumObject toupper(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.ToUpper());
        }

        private HassiumObject begins(HassiumArray args)
        {
            return new HassiumBool(((HassiumString)args[0]).Value.StartsWith(((HassiumString)args[1]).Value));
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

