using System;
using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumString: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("string");

        public new string Value { get; private set; }

        public static HassiumString Create(HassiumObject obj)
        {
            if (!(obj is HassiumString))
                throw new InternalException(string.Format("Cannot convert from {0} to string!", obj.Type()));
            return (HassiumString)obj;
        }

        public HassiumString(string value)
        {
            Value = value;
            Attributes.Add("contains",      new HassiumFunction(contains, 1));
            Attributes.Add("endsWith",      new HassiumFunction(endsWith, 1));
            Attributes.Add("getBytes",      new HassiumFunction(getBytes, 0));
            Attributes.Add("length",        new HassiumProperty(get_Length));
            Attributes.Add("replace",       new HassiumFunction(replace, 2));
            Attributes.Add("reverse",       new HassiumFunction(reverse, 0));
            Attributes.Add("split",         new HassiumFunction(split, 1));
            Attributes.Add("startsWith",    new HassiumFunction(startsWith, 1));
            Attributes.Add("stripChars",    new HassiumFunction(stripChars, 1));
            Attributes.Add("substring",     new HassiumFunction(substring, new int[] { 1, 2 }));
            Attributes.Add("toBool",        new HassiumFunction(toBool, 0));
            Attributes.Add("toChar",        new HassiumFunction(toChar, 0));
            Attributes.Add("toDouble",      new HassiumFunction(toDouble, 0));
            Attributes.Add("toInt",         new HassiumFunction(toInt, 0));
            Attributes.Add("toList",        new HassiumFunction(toList, 0));
            Attributes.Add("toLower",       new HassiumFunction(toLower, 0));
            Attributes.Add("toUpper",       new HassiumFunction(toUpper, 0));
            Attributes.Add("trim",          new HassiumFunction(trim, 0));
            Attributes.Add("trimLeft",      new HassiumFunction(trimLeft, 0));
            Attributes.Add("trimRight",     new HassiumFunction(trimRight, 0));
            Attributes.Add(HassiumObject.CONTAINS,              new HassiumFunction(contains, 1));
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION,     new HassiumFunction(__tostring__, 0));
            Attributes.Add(HassiumObject.ADD_FUNCTION,          new HassiumFunction(__add__, 1));
            Attributes.Add(HassiumObject.MUL_FUNCTION,          new HassiumFunction(__mul__, 1));
            Attributes.Add(HassiumObject.EQUALS_FUNCTION,       new HassiumFunction(__equals__, 1));
            Attributes.Add(HassiumObject.NOT_EQUAL_FUNCTION,    new HassiumFunction(__notequal__, 1));
            Attributes.Add(HassiumObject.INDEX_FUNCTION,        new HassiumFunction(__index__, 1));
            Attributes.Add(HassiumObject.ITER_FUNCTION,         new HassiumFunction(__iter__, 0));
            AddType(HassiumString.TypeDefinition);
        }

        private HassiumBool contains(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Value.Contains(HassiumString.Create(args[0]).Value));
        }
        private HassiumBool endsWith(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Value.EndsWith(HassiumString.Create(args[0]).Value));
        }
        private HassiumList getBytes(VirtualMachine vm, HassiumObject[] args)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(Value);
            HassiumChar[] elements = new HassiumChar[bytes.Length];
            for (int i = 0; i < elements.Length; i++)
                elements[i] = new HassiumChar((char)bytes[i]);

            return new HassiumList(elements);
        }
        private HassiumString format(VirtualMachine vm, HassiumObject[] args)
        {
            string[] argString = new string[args.Length];
            for (int i = 0; i < argString.Length; i++)
                argString[i] = args[i].ToString(vm);
            return new HassiumString(string.Format(Value, argString)); 
        }
        private HassiumInt get_Length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.Length);
        }
        private HassiumString replace(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.Replace(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value));
        }
        private HassiumString reverse(VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = Value.Length - 1; i >= 0; i--)
                sb.Append(Value[i]);
            return new HassiumString(sb.ToString());
        }
        private HassiumList split(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumChar c = HassiumChar.Create(args[0]);
            string[] strings = Value.Split(c.Value);
            HassiumObject[] elements = new HassiumObject[strings.Length];
            for (int i = 0; i < elements.Length; i++)
                elements[i] = new HassiumString(strings[i]);
            return new HassiumList(elements);
        }
        private HassiumBool startsWith(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Value.StartsWith(HassiumString.Create(args[0]).Value));
        }
        private HassiumString stripChars(VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            if (args[0] is HassiumList)
            {
                HassiumList list = (HassiumList)args[0];
                List<char> chars = new List<char>();
                for (int i = 0; i < list.Value.Count; i++)
                    chars.Add(Convert.ToChar(list.Value[i].ToString()));
                foreach (char c in Value)
                    if (!chars.Contains(c))
                        sb.Append(c);
            }
            else
            {
                char ch = HassiumChar.Create(args[0]).Value;
                foreach (char c in Value)
                    if (c != ch)
                        sb.Append(c);
            }
            return new HassiumString(sb.ToString());
        }
        private HassiumString substring(VirtualMachine vm, HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 1:
                    return new HassiumString(Value.Substring(HassiumDouble.Create(args[0]).ValueInt));
                case 2:
                    return new HassiumString(Value.Substring(HassiumDouble.Create(args[0]).ValueInt, HassiumDouble.Create(args[1]).ValueInt));
            }
            return null;
        }
        private HassiumBool toBool(VirtualMachine vm, HassiumObject[] args)
        {
            switch (Value.ToLower())
            {
                case "false":
                    return new HassiumBool(false);
                case "true":
                    return new HassiumBool(true);
                default:
                    throw new InternalException("Cannot convert string to bool!");
            }
        }
        private HassiumChar toChar(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumChar(Convert.ToChar(Value));
        }
        private HassiumDouble toDouble(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(Convert.ToDouble(Value));
        }
        private HassiumInt toInt(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Convert.ToInt32(Value));
        }
        private HassiumList toList(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject[] items = new HassiumObject[Value.Length];
            for (int i = 0; i < items.Length; i++)
                items[i] = new HassiumChar(Value[i]);
            return new HassiumList(items);
        }
        private HassiumString toLower(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.ToLower());
        }
        private HassiumString toUpper(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.ToUpper());
        }
        private HassiumString trim(VirtualMachine vm, HassiumObject[] args)
        {
            return trimRight(vm, new HassiumObject[] { trimLeft(vm, args) });
        }
        private HassiumString trimLeft(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.TrimStart());
        }
        private HassiumString trimRight(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.TrimEnd());
        }
        private HassiumString __tostring__ (VirtualMachine vm, HassiumObject[] args)
        {
            return this;
        }

        private HassiumObject __add__ (VirtualMachine vm, HassiumObject[] args)
        {
            return this + new HassiumString(args[0].ToString(vm));
        }
        private HassiumObject __mul__ (VirtualMachine vm, HassiumObject[] args)
        {
            return this * HassiumInt.Create(args[0]);
        }
        private HassiumObject __equals__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumString)
                return this == (HassiumString)obj;
            throw new InternalException("Cannot compare string to " + obj);
        }
        private HassiumObject __notequal__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumString)
                return this != (HassiumString)obj;
            throw new InternalException("Cannot compare string to " + obj);
        }
        private HassiumObject __index__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumDouble)
                return new HassiumChar(Value[((HassiumDouble)obj).ValueInt]);
            else if (obj is HassiumInt)
                return new HassiumChar(Value[(int)((HassiumInt)obj).Value]);
            throw new InternalException("Cannot index string with " + obj);
        }

        private HassiumObject __iter__ (VirtualMachine vm, HassiumObject[] args)
        {
            return toList(vm, args);
        }
        
        public static HassiumString operator + (HassiumString left, HassiumString right)
        {
            return new HassiumString(left.Value + right.Value);
        }
        public static HassiumString operator * (HassiumString left, HassiumInt right)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < right.Value; i++)
                sb.Append(left.Value);
            return new HassiumString(sb.ToString());
        }
        public static HassiumString operator * (HassiumInt left, HassiumString right)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < left.Value; i++)
                sb.Append(right.Value);
            return new HassiumString(sb.ToString());
        }
        public static HassiumBool operator == (HassiumString left, HassiumString right)
        {
            return new HassiumBool(left.Value == right.Value);
        }
        public static HassiumBool operator != (HassiumString left, HassiumString right)
        {
            return new HassiumBool(left.Value != right.Value);
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
            return Value;
        }
    }
}

