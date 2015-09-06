using System;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumString: HassiumObject, IConvertible
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
            Attributes.Add("toLower", new InternalFunction(tolower));
            Attributes.Add("toUpper", new InternalFunction(toupper));
            Attributes.Add("begins", new InternalFunction(begins));
            Attributes.Add("ends", new InternalFunction(ends));
            Attributes.Add("getAt", new InternalFunction(getat));
            Attributes.Add("substring", new InternalFunction(substr));
            Attributes.Add("concat", new InternalFunction(concat));
            Attributes.Add("contains", new InternalFunction(contains));
            Attributes.Add("split", new InternalFunction(split));
            Attributes.Add("replace", new InternalFunction(replace));
            Attributes.Add("index", new InternalFunction(index));
            Attributes.Add("isWhiteSpace", new InternalFunction(isWhiteSpace));
            Attributes.Add("lastIndex", new InternalFunction(lastindex));
            Attributes.Add("padLeft", new InternalFunction(padleft));
            Attributes.Add("padRight", new InternalFunction(padright));
            Attributes.Add("trim", new InternalFunction(trim));
            Attributes.Add("trimLeft", new InternalFunction(trimleft));
            Attributes.Add("trimRight", new InternalFunction(trimright));
            Attributes.Add("toString", new InternalFunction(tostring));
            Attributes.Add("length", new InternalFunction(x => Value.Length, true));
        }
          
        private HassiumObject tolower(HassiumObject[] args)
        {
            return new HassiumString(Value.ToLower());
        }

        private HassiumObject toupper(HassiumObject[] args)
        {
            return new HassiumString(Value.ToUpper());
        }

        private HassiumObject begins(HassiumObject[] args)
        {
            return new HassiumBool(Value.StartsWith(((HassiumString)args[0]).Value));
        }

        private HassiumObject ends(HassiumObject[] args)
        {
            return new HassiumBool(Value.EndsWith(((HassiumString)args[0]).Value));
        }

        private HassiumObject getat(HassiumObject[] args)
        {
            return new HassiumString(Value[Convert.ToInt32(((HassiumNumber)args[0]).Value)].ToString());
        }

        private HassiumObject substr(HassiumObject[] args)
        {
            return new HassiumString(Value.Substring(Convert.ToInt32(((HassiumNumber)args[0]).Value), Convert.ToInt32(((HassiumNumber)args[1]).Value)));
        }

        private HassiumObject concat(HassiumObject[] args)
        {
            return new HassiumString(Value + ((HassiumString)args[0]).Value);
        }

        private HassiumObject contains(HassiumObject[] args)
        {
            return new HassiumBool(Value.Contains(((HassiumString)args[0]).Value));
        }

        private HassiumObject split(HassiumObject[] args)
        {
            return new HassiumArray(Value.Split(Convert.ToChar(((HassiumString)args[0]).Value)));
        }

        private HassiumObject index(HassiumObject[] args)
        {
            return new HassiumNumber(Value.IndexOf(((HassiumString)args[0]).Value));
        }

        private HassiumObject lastindex(HassiumObject[] args)
        {
            return new HassiumNumber(Value.LastIndexOf(((HassiumString)args[0]).Value, StringComparison.Ordinal));
        }

        private HassiumObject padleft(HassiumObject[] args)
        {
            return new HassiumString(Value.PadLeft(Convert.ToInt32(((HassiumNumber)args[0]).Value)));
        }

        private HassiumObject padright(HassiumObject[] args)
        {
            return new HassiumString(Value.PadRight(Convert.ToInt32(((HassiumNumber)args[0]).Value)));
        }

        private HassiumObject replace(HassiumObject[] args)
        {
            return new HassiumString(Value.Replace(((HassiumString)args[0]).Value, ((HassiumString)args[1]).Value));
        }

        private HassiumObject trim(HassiumObject[] args)
        {
            return new HassiumString(Value.Trim());
        }

        private HassiumObject trimleft(HassiumObject[] args)
        {
            return new HassiumString(Value.TrimStart());
        }

        private HassiumObject trimright(HassiumObject[] args)
        {
            return new HassiumString(Value.TrimEnd());
        }

        private HassiumObject tostring(HassiumObject[] args)
        {
            return new HassiumString(Value);
        }

        private HassiumObject isWhiteSpace(HassiumObject[] args)
        {
            return new HassiumBool(char.IsWhiteSpace(Convert.ToChar(Value)));
        }

        public static implicit operator HassiumNumber(HassiumString str)
        {
            return new HassiumNumber(Convert.ToDouble(str.Value));
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

