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
            this.Attributes.Add("ends", new InternalFunction(ends));
            this.Attributes.Add("getat", new InternalFunction(getat));
            this.Attributes.Add("substr", new InternalFunction(substr));
            this.Attributes.Add("concat", new InternalFunction(concat));
            this.Attributes.Add("contains", new InternalFunction(contains));
            this.Attributes.Add("split", new InternalFunction(split));
            this.Attributes.Add("replace", new InternalFunction(replace));
            this.Attributes.Add("index", new InternalFunction(index));
            this.Attributes.Add("lastindex", new InternalFunction(lastindex));
            this.Attributes.Add("padleft", new InternalFunction(padleft));
            this.Attributes.Add("padright", new InternalFunction(padright));
            this.Attributes.Add("trim", new InternalFunction(trim));
            this.Attributes.Add("trimleft", new InternalFunction(trimleft));
            this.Attributes.Add("trimright", new InternalFunction(trimright));
            this.Attributes.Add("tostring", new InternalFunction(tostring));
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

        private HassiumObject ends(HassiumArray args)
        {
            return new HassiumBool(((HassiumString)args[0]).Value.EndsWith(((HassiumString)args[1]).Value));
        }

        private HassiumObject getat(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value[Convert.ToInt32(((HassiumNumber)args[1]).Value)].ToString());
        }

        private HassiumObject substr(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.Substring(Convert.ToInt32(((HassiumNumber)args[1]).Value), Convert.ToInt32(((HassiumNumber)args[2]).Value)));
        }

        private HassiumObject concat(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value + ((HassiumString)args[1]).Value);
        }

        private HassiumObject contains(HassiumArray args)
        {
            return new HassiumBool(((HassiumString)args[0]).Value.Contains(((HassiumString)args[1]).Value));
        }

        private HassiumObject split(HassiumArray args)
        {
            return new HassiumArray(((HassiumString)args[0]).Value.Split(Convert.ToChar(((HassiumString)args[1]).Value)));
        }

        private HassiumObject index(HassiumArray args)
        {
            return new HassiumNumber(((HassiumString)args[0]).Value.IndexOf(((HassiumString)args[1]).Value));
        }

        private HassiumObject lastindex(HassiumArray args)
        {
            return new HassiumNumber(((HassiumString)args[0]).Value.LastIndexOf(((HassiumString)args[1]).Value));
        }

        private HassiumObject padleft(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.PadLeft(Convert.ToInt32(((HassiumNumber)args[1]).Value)));
        }

        private HassiumObject padright(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.PadRight(Convert.ToInt32(((HassiumNumber)args[1]).Value)));
        }

        private HassiumObject replace(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.Replace(((HassiumString)args[1]).Value, ((HassiumString)args[2]).Value));
        }

        private HassiumObject trim(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.Trim());
        }

        private HassiumObject trimleft(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.TrimStart());
        }

        private HassiumObject trimright(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.TrimEnd());
        }

        private HassiumObject tostring(HassiumArray args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.ToString());
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

