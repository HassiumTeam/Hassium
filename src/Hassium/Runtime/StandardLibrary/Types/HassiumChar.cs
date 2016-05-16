using System;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumChar: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("char");
        public new char Value { get; private set; }

        public static HassiumChar Create(HassiumObject obj)
        {
            if (!(obj is HassiumChar))
                throw new InternalException(string.Format("Cannot convert from {0} to char!", obj.Type()));
            return (HassiumChar)obj;
        }

        public HassiumChar(char value)
        {
            Value = value;
            Attributes.Add("isDigit",           new HassiumFunction(isDigit, 0));
            Attributes.Add("isLetter",          new HassiumFunction(isLetter, 0));
            Attributes.Add("isLetterOrDigit",   new HassiumFunction(isLetterOrDigit, 0));
            Attributes.Add("isLower",           new HassiumFunction(isLower, 0));
            Attributes.Add("isUpper",           new HassiumFunction(isUpper, 0));
            Attributes.Add("isWhitespace",      new HassiumFunction(isWhitespace, 0));
            Attributes.Add("toBool",            new HassiumFunction(toBool, 0));
            Attributes.Add("toChar",            new HassiumFunction(toChar, 0));
            Attributes.Add("toDouble",          new HassiumFunction(toDouble, 0));
            Attributes.Add("toInt",             new HassiumFunction(toInt, 0));
            Attributes.Add(HassiumObject.ADD_FUNCTION,      new HassiumFunction(__add__, 1));
            Attributes.Add(HassiumObject.SUB_FUNCTION,      new HassiumFunction(__sub__, 1));
            Attributes.Add(HassiumObject.MUL_FUNCTION,      new HassiumFunction(__mul__, 1));
            Attributes.Add(HassiumObject.DIV_FUNCTION,      new HassiumFunction(__div__, 1));
            Attributes.Add(HassiumObject.MOD_FUNCTION,      new HassiumFunction(__mod__, 1));
            Attributes.Add(HassiumObject.XOR_FUNCTION,      new HassiumFunction(__xor__, 1));
            Attributes.Add(HassiumObject.OR_FUNCTION,       new HassiumFunction(__or__, 1));
            Attributes.Add(HassiumObject.XAND_FUNCTION,     new HassiumFunction(__xand__, 1));
            Attributes.Add(HassiumObject.EQUALS_FUNCTION,   new HassiumFunction(__equals__, 1));
            Attributes.Add(HassiumObject.NOT_EQUAL_FUNCTION,new HassiumFunction(__notequals__, 1));
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION, new HassiumFunction(__tostring__, 0));
            AddType(HassiumChar.TypeDefinition);
        }

        private HassiumBool isDigit(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(char.IsDigit(Value));
        }
        private HassiumBool isLetter(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(char.IsLetter(Value));
        }
        private HassiumBool isLower(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(char.IsLower(Value));
        }
        private HassiumBool isLetterOrDigit(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(char.IsLetterOrDigit(Value));
        }
        private HassiumBool isUpper(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(char.IsUpper(Value));
        }
        private HassiumBool isWhitespace(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(char.IsWhiteSpace(Value));
        }
        private HassiumBool toBool(VirtualMachine vm, HassiumObject[] args)
        {
            switch ((int)Value)
            {
                case 0:
                    return new HassiumBool(false);
                case 1:
                    return new HassiumBool(true);
                default:
                    throw new InternalException("Cannot convert char to boolean!");
            }
        }
        private HassiumChar toChar(VirtualMachine vm, HassiumObject[] args)
        {
            return this;
        }
        private HassiumDouble toDouble(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(Convert.ToDouble(Convert.ToInt32(Value)));
        }
        private HassiumInt toInt(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Convert.ToInt64(Value));
        }

        private HassiumObject __add__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this + (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this + (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __sub__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this - (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this - (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __mul__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this * (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this * (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __div__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this / (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this / (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __mod__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this % (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this % (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __xor__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this ^ (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this ^ (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __or__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this | (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this | (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __xand__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this & (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this & (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __equals__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this == (HassiumChar)obj;
            else if (obj is HassiumInt)
                return new HassiumBool(Value == ((HassiumInt)obj).Value);
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __notequals__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this != (HassiumChar)obj;
            else if (obj is HassiumInt)
                return new HassiumBool(Value != ((HassiumInt)obj).Value);
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __greater__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this > (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this > (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __lesser__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this < (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this < (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __greaterorequal__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this >= (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this >= (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name);
        }
        private HassiumObject __lesserorequal__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumChar)
                return this <= (HassiumChar)obj;
            else if (obj is HassiumInt)
                return this <= (HassiumInt)obj;
            throw new InternalException("Cannot operate char on " + obj.GetType().Name.GetType().Name);
        }
        private HassiumString __tostring__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }

        public static HassiumChar operator + (HassiumChar left, HassiumChar right)
        {
            return new HassiumChar((char)(left.Value + right.Value));
        }
        public static HassiumChar operator + (HassiumChar left, HassiumInt right)
        {
            return new HassiumChar((char)(left.Value + right.Value));
        }
        public static HassiumChar operator - (HassiumChar left, HassiumChar right)
        {
            return new HassiumChar((char)(left.Value - right.Value));
        }
        public static HassiumChar operator - (HassiumChar left, HassiumInt right)
        {
            return new HassiumChar((char)(left.Value - right.Value));
        }
        public static HassiumChar operator * (HassiumChar left, HassiumChar right)
        {
            return new HassiumChar((char)(left.Value * right.Value));
        }
        public static HassiumChar operator * (HassiumChar left, HassiumInt right)
        {
            return new HassiumChar((char)(left.Value * right.Value));
        }
        public static HassiumChar operator / (HassiumChar left, HassiumChar right)
        {
            return new HassiumChar((char)(left.Value / right.Value));
        }
        public static HassiumChar operator / (HassiumChar left, HassiumInt right)
        {
            return new HassiumChar((char)(left.Value / right.Value));
        }
        public static HassiumChar operator % (HassiumChar left, HassiumChar right)
        {
            return new HassiumChar((char)(left.Value % right.Value));
        }
        public static HassiumChar operator % (HassiumChar left, HassiumInt right)
        {
            return new HassiumChar((char)(left.Value % right.Value));
        }
        public static HassiumChar operator ^ (HassiumChar left, HassiumChar right)
        {
            return new HassiumChar((char)(left.Value ^ right.Value));
        }
        public static HassiumChar operator ^ (HassiumChar left, HassiumInt right)
        {
            return new HassiumChar((char)(left.Value ^ right.Value));
        }
        public static HassiumChar operator | (HassiumChar left, HassiumChar right)
        {
            return new HassiumChar((char)(left.Value | right.Value));
        }
        public static HassiumChar operator | (HassiumChar left, HassiumInt right)
        {
            return new HassiumChar((char)(left.Value | right.Value));
        }
        public static HassiumChar operator & (HassiumChar left, HassiumChar right)
        {
            return new HassiumChar((char)(left.Value & right.Value));
        }
        public static HassiumChar operator & (HassiumChar left, HassiumInt right)
        {
            return new HassiumChar((char)(left.Value & right.Value));
        }
        public static HassiumBool operator == (HassiumChar left, HassiumChar right)
        {
            return new HassiumBool(left.Value == right.Value);
        }
        public static HassiumBool operator == (HassiumChar left, HassiumInt right)
        {
            return new HassiumBool((int)left.Value == right.Value);
        }
        public static HassiumBool operator != (HassiumChar left, HassiumChar right)
        {
            return new HassiumBool(left.Value != right.Value);
        }
        public static HassiumBool operator != (HassiumChar left, HassiumInt right)
        {
            return new HassiumBool((int)left.Value != right.Value);
        }
        public static HassiumBool operator > (HassiumChar left, HassiumChar right)
        {
            return new HassiumBool(left.Value > right.Value);
        }
        public static HassiumBool operator > (HassiumChar left, HassiumInt right)
        {
            return new HassiumBool((int)left.Value > right.Value);
        }
        public static HassiumBool operator < (HassiumChar left, HassiumChar right)
        {
            return new HassiumBool(left.Value < right.Value);
        }
        public static HassiumBool operator < (HassiumChar left, HassiumInt right)
        {
            return new HassiumBool((int)left.Value < right.Value);
        }
        public static HassiumBool operator >= (HassiumChar left, HassiumChar right)
        {
            return new HassiumBool(left.Value >= right.Value);
        }
        public static HassiumBool operator >= (HassiumChar left, HassiumInt right)
        {
            return new HassiumBool((int)left.Value >= right.Value);
        }
        public static HassiumBool operator <= (HassiumChar left, HassiumChar right)
        {
            return new HassiumBool(left.Value <= right.Value);
        }
        public static HassiumBool operator <= (HassiumChar left, HassiumInt right)
        {
            return new HassiumBool((int)left.Value <= right.Value);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

