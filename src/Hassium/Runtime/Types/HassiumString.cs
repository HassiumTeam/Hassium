using Hassium.Compiler;

using System;

namespace Hassium.Runtime.Types
{
    public class HassiumString : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("string");

        public string String { get; private set; }

        public HassiumString(string val)
        {
            AddType(TypeDefinition);
            String = val;

            AddAttribute(ADD, Add, 1);
            AddAttribute(EQUALTO, EqualTo, 1);
            AddAttribute("format", format, -1);
            AddAttribute(GREATERTHAN, GreaterThan, 1);
            AddAttribute(GREATERTHANOREQUAL, GreaterThanOrEqual, 1);
            AddAttribute(INDEX, Index, 1);
            AddAttribute(ITER, Iter, 0);
            AddAttribute("length", new HassiumProperty(get_length));
            AddAttribute(LESSERTHAN, LesserThan, 1);
            AddAttribute(LESSERTHANOREQUAL, LesserThanOrEqual, 1);
            AddAttribute(NOTEQUALTO, NotEqualTo, 1);
            AddAttribute(TOFLOAT, ToFloat, 0);
            AddAttribute(TOINT, ToInt, 0);
            AddAttribute(TOLIST, ToList, 0);
            AddAttribute("tolower", tolower, 0);
            AddAttribute(TOSTRING, ToString, 0);
            AddAttribute("toupper", toupper, 0);
        }

        public override HassiumObject Add(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(String + args[0].ToString(vm, location).String);
        }

        [FunctionAttribute("func __equals__ (str : string) : bool")]
        public override HassiumBool EqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(String == args[0].ToString(vm, location).String);
        }

        [FunctionAttribute("func format (params fargs) : string")]
        public HassiumString format(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string[] strargs = new string[args.Length];
            for (int i = 0; i < strargs.Length; i++)
                strargs[i] = args[i].ToString(vm, location).String;
            return new HassiumString(string.Format(String, strargs));
        }

        [FunctionAttribute("func __greater__ (str : string) : bool")]
        public override HassiumObject GreaterThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(string.Compare(String, args[0].ToString(vm, location).String) == 1);
        }

        [FunctionAttribute("func __greaterorequal__ (str : string) : bool")]
        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(string.Compare(String, args[0].ToString(vm, location).String) >= 0);
        }

        [FunctionAttribute("func __index__ (index : int) : char")]
        public override HassiumObject Index(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar(String[(int)args[0].ToInt(vm, location).Int]);
        }

        [FunctionAttribute("func __iter__ () : list")]
        public override HassiumObject Iter(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (var c in String)
                list.add(vm, location, new HassiumChar(c));
            return list;
        }

        [FunctionAttribute("length { get; }")]
        public HassiumInt get_length(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(String == null ? -1 : String.Length);
        }

        [FunctionAttribute("func __lesser__ (str : string) : bool")]
        public override HassiumObject LesserThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(string.Compare(String, args[0].ToString(vm, location).String) == -1);
        }

        [FunctionAttribute("func __lesserorequal__ (str : string) : bool")]
        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(string.Compare(String, args[0].ToString(vm, location).String) <= 0);
        }

        [FunctionAttribute("func __notequal__ (str : string) : bool")]
        public override HassiumBool NotEqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(String != args[0].ToString(vm, location).String);
        }

        [FunctionAttribute("func tofloat () : float")]
        public override HassiumFloat ToFloat(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Convert.ToDouble(String));
        }

        [FunctionAttribute("func toint () : int")]
        public override HassiumInt ToInt(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            try
            {
                return new HassiumInt(Convert.ToInt64(String));
            }
            catch
            {
                vm.RaiseException(HassiumConversionFailedException._new(vm, location, this, HassiumInt.TypeDefinition));
                return new HassiumInt(-1);
            }
        }

        [FunctionAttribute("func tolist () : list")]
        public override HassiumList ToList(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Iter(vm, location, args) as HassiumList;
        }

        [FunctionAttribute("func tolower () : string")]
        public HassiumString tolower(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(String.ToLower());
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func toupper () : string")]
        public HassiumString toupper(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(String.ToUpper());
        }
    }
}
