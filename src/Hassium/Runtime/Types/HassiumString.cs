using Hassium.Compiler;

using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumString : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("string");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { ADD, new HassiumFunction(add, 1)  },
            { EQUALTO, new HassiumFunction(equalto, 1)  },
            { "format", new HassiumFunction(format, -1)  },
            { GREATERTHAN, new HassiumFunction(greaterthan, 1)  },
            { GREATERTHANOREQUAL, new HassiumFunction(greaterthanorequal, 1)  },
            { INDEX, new HassiumFunction(index, 1)  },
            { ITER, new HassiumFunction(iter, 0)  },
            { "length", new HassiumProperty(get_length)  },
            { LESSERTHAN, new HassiumFunction(lesserthan, 1)  },
            { LESSERTHANOREQUAL, new HassiumFunction(lesserthanorequal, 1)  },
            { NOTEQUALTO, new HassiumFunction(notequalto, 1)  },
            { TOFLOAT, new HassiumFunction(tofloat, 0)  },
            { TOINT, new HassiumFunction(toint, 0)  },
            { TOLIST, new HassiumFunction(tolist, 0)  },
            { "tolower", new HassiumFunction(tolower, 0)  },
            { TOSTRING, new HassiumFunction(tostring, 0)  },
            { "toupper", new HassiumFunction(toupper, 0)  },
        };

        public string String { get; private set; }

        public HassiumString(string val)
        {
            AddType(TypeDefinition);
            String = val;
        }

        public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumString(String + args[0].ToString(vm, args[0], location).String);
        }

        [FunctionAttribute("func __equals__ (str : string) : bool")]
        public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumBool(String == args[0].ToString(vm, args[0], location).String);
        }

        [FunctionAttribute("func format (params fargs) : string")]
        public static HassiumString format(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;

            string[] strargs = new string[args.Length];
            for (int i = 0; i < strargs.Length; i++)
                strargs[i] = args[i].ToString(vm, args[i], location).String;
            return new HassiumString(string.Format(String, strargs));
        }

        [FunctionAttribute("func __greater__ (str : string) : bool")]
        public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumBool(string.Compare(String, args[0].ToString(vm, args[0], location).String) == 1);
        }

        [FunctionAttribute("func __greaterorequal__ (str : string) : bool")]
        public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumBool(string.Compare(String, args[0].ToString(vm, args[0], location).String) >= 0);
        }

        [FunctionAttribute("func __index__ (index : int) : char")]
        public static HassiumObject index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumChar(String[(int)args[0].ToInt(vm, args[0], location).Int]);
        }

        [FunctionAttribute("func __iter__ () : list")]
        public static HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (var c in String)
                HassiumList.add(vm, list, location, new HassiumChar(c));
            return list;
        }

        [FunctionAttribute("length { get; }")]
        public static HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumInt(String == null ? -1 : String.Length);
        }

        [FunctionAttribute("func __lesser__ (str : string) : bool")]
        public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumBool(string.Compare(String, args[0].ToString(vm, args[0], location).String) == -1);
        }

        [FunctionAttribute("func __lesserorequal__ (str : string) : bool")]
        public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumBool(string.Compare(String, args[0].ToString(vm, args[0], location).String) <= 0);
        }

        [FunctionAttribute("func __notequal__ (str : string) : bool")]
        public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumBool(String != args[0].ToString(vm, args[0], location).String);
        }

        [FunctionAttribute("func tofloat () : float")]
        public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumFloat(Convert.ToDouble(String));
        }

        [FunctionAttribute("func toint () : int")]
        public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            try
            {
                return new HassiumInt(Convert.ToInt64(String));
            }
            catch
            {
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, tostring(vm, self, location), HassiumInt.TypeDefinition));
                return new HassiumInt(-1);
            }
        }

        [FunctionAttribute("func tolist () : list")]
        public static HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return iter(vm, self, location, args) as HassiumList;
        }

        [FunctionAttribute("func tolower () : string")]
        public static HassiumString tolower(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumString(String.ToLower());
        }

        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return self as HassiumString;
        }

        [FunctionAttribute("func toupper () : string")]
        public static HassiumString toupper(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var String = (self as HassiumString).String;
            return new HassiumString(String.ToUpper());
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || Attribs.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (Attribs[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in Attribs)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
