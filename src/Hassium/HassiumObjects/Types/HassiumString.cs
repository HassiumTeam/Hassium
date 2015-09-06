using System;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

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
            if (args.Length >= 2)
                return new HassiumString(Value.Substring(((HassiumInt)args[0]).Value, ((HassiumInt)args[1]).Value));
            else
                return new HassiumString(Value.Substring(((HassiumInt)args[0]).Value));
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
            return new HassiumArray(Value.Split(Convert.ToChar(args[0].ToString())));
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

