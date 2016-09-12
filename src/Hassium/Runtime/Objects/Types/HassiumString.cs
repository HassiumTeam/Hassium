using System;
using System.Text;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumString: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("string");

        public string String { get; private set; }

        public HassiumString(string val)
        {
            AddType(TypeDefinition);
            String = val;

            AddAttribute("contains",    contains,   1);
            AddAttribute("endsWith",    endsWith,   1);
            AddAttribute("format",      format,    -1);
            AddAttribute("getBytes",    ToList,     0);
            AddAttribute("indexOf",     indexOf,    1);
            AddAttribute("insert",      insert,     2);
            AddAttribute("lastIndexOf", lastIndexOf,1);
            AddAttribute("length",      new HassiumProperty(get_length));
            AddAttribute("remove",      remove,     1);
            AddAttribute("replace",     replace,    2);
            AddAttribute("reverse",     reverse,    0);
            AddAttribute("split",       split,      1);
            AddAttribute("startsWith",  startsWith, 1);
            AddAttribute("substring",   substring,  1, 2);
            AddAttribute("toLower",     toLower,    0);
            AddAttribute("toUpper",     toUpper,    0);
            AddAttribute("trim",        trim,       0);
            AddAttribute("trimLeft",    trimLeft,   0);
            AddAttribute("trimRight",   trimRight,  0);

            AddAttribute(HassiumObject.TOBOOL,  ToBool,     0);
            AddAttribute(HassiumObject.TOCHAR,  ToChar,     0);
            AddAttribute(HassiumObject.TOFLOAT, ToFloat,    0);
            AddAttribute(HassiumObject.TOINT,   ToInt,      0);
            AddAttribute(HassiumObject.TOLIST,  ToList,     0);
            AddAttribute(HassiumObject.TOSTRING,ToString,   0);
            AddAttribute(HassiumObject.TOTUPLE, ToTuple,    0);
            AddAttribute(HassiumObject.ITER,    Iter,       0);
        }

        public HassiumBool contains(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(String.Contains(args[0].ToString(vm).String));
        }
        public HassiumBool endsWith(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(String.EndsWith(args[0].ToString(vm).String));
        }
        public HassiumString format(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumObject[] formatArgs = new HassiumObject[args.Length + 1];
            formatArgs[0] = this;
            for (int i = 0; i < args.Length; i++)
                formatArgs[i + 1] = args[i];
            return GlobalFunctions.format(vm, formatArgs);
        }
        public HassiumInt indexOf(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(String.IndexOf(args[0].ToChar(vm).Char));
        }
        public HassiumString insert(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String.Insert((int)args[0].ToInt(vm).Int, args[1].ToString(vm).String));
        }
        public HassiumInt lastIndexOf(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(String.LastIndexOf(args[0].ToChar(vm).Char));
        }
        public HassiumInt get_length(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(String.Length);
        }
        public HassiumString remove(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String.Remove((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int));
        }
        public HassiumString replace(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String.Replace(args[0].ToString(vm).String, args[1].ToString(vm).String));
        }
        public HassiumString reverse(VirtualMachine vm, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = String.Length - 1; i >= 0; i--)
                sb.Append(String[i]);
            return new HassiumString(sb.ToString());
        }
        public HassiumList split(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            foreach (string s in String.Split(args[0].ToChar(vm).Char))
                result.add(vm, new HassiumString(s));
            return result;
        }
        public HassiumBool startsWith(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(String.StartsWith(args[0].ToString(vm).String));
        }
        public HassiumString substring(VirtualMachine vm, params HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 1:
                    return new HassiumString(String.Substring((int)args[0].ToInt(vm).Int));
                case 2:
                    return new HassiumString(String.Substring((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int));
                default:
                    return null;
            }
        }
        public HassiumString toLower(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String.ToLower());
        }
        public HassiumString toUpper(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String.ToUpper());
        }
        public HassiumString trim(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String.Trim());
        }
        public HassiumString trimLeft(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String.TrimStart());
        }
        public HassiumString trimRight(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String.TrimEnd());
        }

        public override HassiumObject Add(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(String + args[0].ToString(vm, args).String);
        }
        public override HassiumObject EqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(String == args[0].ToString(vm, args).String);
        }
        public override HassiumObject Multiply(VirtualMachine vm, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            int times = (int)args[0].ToInt(vm, args).Int;
            for (int i = 0; i < times; i++)
                sb.Append(String);
            return new HassiumString(sb.ToString());
        }
        public override HassiumObject NotEqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            return EqualTo(vm, args).LogicalNot(vm, args);
        }
        public override HassiumBool ToBool(VirtualMachine vm, params HassiumObject[] args)
        {
            switch (String.ToLower().Trim())
            {
                case "true":
                    return new HassiumBool(true);
                case "false":
                    return new HassiumBool(false);
                default:
                    throw new InternalException(vm, InternalException.CONVERSION_ERROR, Type(), HassiumBool.TypeDefinition);
            }
        }
        public override HassiumObject Index(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar(String[(int)args[0].ToInt(vm).Int]);
        }
        public override HassiumChar ToChar(VirtualMachine vm, params HassiumObject[] args)
        {
            if (String.Trim().Length != 1)
                throw new InternalException(vm, InternalException.CONVERSION_ERROR, Type(), HassiumChar.TypeDefinition);
            return new HassiumChar(String[0]);
        }
        public override HassiumFloat ToFloat(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat(Convert.ToDouble(String));
        }
        public override HassiumInt ToInt(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Convert.ToInt64(String));
        }
        public override HassiumList ToList(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumChar[] chars = new HassiumChar[String.Length];
            for (int i = 0; i < chars.Length; i++)
                chars[i] = new HassiumChar(String[i]);
            return new HassiumList(chars);
        }
        public override HassiumString ToString(VirtualMachine vm, params HassiumObject[] args)
        {
            return this;
        }
        public override HassiumTuple ToTuple(VirtualMachine vm, params HassiumObject[] args)
        {
            return ToList(vm, args).ToTuple(vm, args);
        }
        public override HassiumObject Iter(VirtualMachine vm, params HassiumObject[] args)
        {
            return ToList(vm, args);
        }
    }
}

