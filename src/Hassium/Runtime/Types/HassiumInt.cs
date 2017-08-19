using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumInt : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("int");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { ADD, new HassiumFunction(add, 1)  },
            { BITSHIFTLEFT, new HassiumFunction(bitshiftleft, 1)  },
            { BITSHIFTRIGHT, new HassiumFunction(bitshiftright, 1)  },
            { BITWISEAND, new HassiumFunction(bitwiseand, 1)  },
            { BITWISENOT, new HassiumFunction(bitwisenot, 0)  },
            { BITWISEOR, new HassiumFunction(bitwiseor, 1)  },
            { DIVIDE, new HassiumFunction(divide, 1)  },
            { EQUALTO, new HassiumFunction(equalto, 1)  },
            { "getbit", new HassiumFunction(getbit, 1)  },
            { GREATERTHAN, new HassiumFunction(greaterthan, 1)  },
            { GREATERTHANOREQUAL, new HassiumFunction(greaterthanorequal, 1)  },
            { INTEGERDIVISION, new HassiumFunction(integerdivision, 1)  },
            { LESSERTHAN, new HassiumFunction(lesserthan, 1)  },
            { LESSERTHANOREQUAL, new HassiumFunction(lesserthanorequal, 1)  },
            { MODULUS, new HassiumFunction(modulus, 1) },
            { MULTIPLY, new HassiumFunction(multiply, 1)  },
            { NEGATE, new HassiumFunction(negate, 0)  },
            { NOTEQUALTO, new HassiumFunction(notequalto, 1)  },
            { POWER, new HassiumFunction(power, 1)  },
            { "setbit", new HassiumFunction(setbit, 2)  },
            { SUBTRACT, new HassiumFunction(subtract, 1)  },
            { TOCHAR, new HassiumFunction(tochar, 0)  },
            { TOFLOAT, new HassiumFunction(tofloat, 0)  },
            { TOINT, new HassiumFunction(toint, 0)  },
            { TOSTRING, new HassiumFunction(tostring, 0)  },
            { XOR, new HassiumFunction(xor, 1)  },

        };

        public long Int { get; private set; }

        public HassiumInt(long val)
        {
            AddType(Number);
            AddType(TypeDefinition);
            Int = val;
        }

        public override HassiumObject Add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return add(vm, this, location, args);
        }

        [FunctionAttribute("func __add__ (num : number) : number")]
        public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int + (args[0] as HassiumInt).Int);
            var charArg = args[0] as HassiumChar;
            if (charArg != null)
                return new HassiumInt(Int + (args[0] as HassiumChar).Char);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int + (args[0] as HassiumFloat).Float);
            var strArg = args[0] as HassiumString;
            if (strArg != null)
                return new HassiumString(Int.ToString() + (args[0] as HassiumString).String);
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __bitshiftleft__ (i : int) : int")]
        public static HassiumObject bitshiftleft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt((int)Int << (int)args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __bitshiftright__ (i : int) : int")]
        public static HassiumObject bitshiftright(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt((int)Int >> (int)args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __bitwiseand__ (i : int) : int")]
        public static HassiumObject bitwiseand(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt(Int & args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __bitwisenot__ () : int")]
        public static HassiumObject bitwisenot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt(~Int);
        }

        [FunctionAttribute("func __bitwiseor__ (i : int) : int")]
        public static HassiumObject bitwiseor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt(Int | args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __divide__ (num : number) : number")]
        public static HassiumObject divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int / (args[0] as HassiumInt).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int / (args[0] as HassiumFloat).Float);
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return self;
        }

        [FunctionAttribute("func __equals__ ")]
        public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumBool(Int == args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func getbit (index : int) : bool")]
        public static HassiumBool getbit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumBool((Int & (1 << (int)args[0].ToInt(vm, args[0], location).Int - 1)) != 0);
        }

        public override HassiumObject GreaterThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return greaterthan(vm, this, location, args);
        }

        [FunctionAttribute("func __greater__ (num : number) : bool")]
        public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumBool(Int > args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return greaterthanorequal(vm, this, location, args);
        }

        [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
        public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumBool(Int >= args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __intdivision__ (num : number) : int")]
        public static HassiumObject integerdivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt(Int / args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject LesserThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return lesserthan(vm, this, location, args);
        }

        [FunctionAttribute("func __lesser__ (num : number) : bool")]
        public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumBool(Int < args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
        public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumBool(Int <= args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __modulus__ (i : int) : int")]
        public static HassiumObject modulus(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt(Int % args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __multiply__ (num : number) : number")]
        public static HassiumObject multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int * (args[0] as HassiumInt).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int * (args[0] as HassiumFloat).Float);
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __negate__ () : int")]
        public static HassiumObject negate(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt(-Int);
        }

        [FunctionAttribute("func __notequal__ (i : int) : bool")]
        public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumBool(Int != args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __power__ (pow : number) : int")]
        public static HassiumObject power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt((long)System.Math.Pow((double)Int, (double)args[0].ToInt(vm, args[0], location).Int));
        }

        [FunctionAttribute("func setbit (index : int, val : bool) : int")]
        public static HassiumInt setbit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            int index = (int)args[0].ToInt(vm, args[0], location).Int;
            bool val = args[1].ToBool(vm, args[1], location).Bool;
            if (val)
                return new HassiumInt((int)Int | 1 << index);
            else
                return new HassiumInt(Int & ~(1 << index));
        }

        public override HassiumObject Subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return subtract(vm, this, location, args);
        }

        [FunctionAttribute("func __subtract__ (num : number) : number")]
        public static HassiumObject subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int - (args[0] as HassiumInt).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int - (args[0] as HassiumFloat).Float);
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func tobool () : bool")]
        public static HassiumBool tobool(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumBool(Int == 1);
        }

        [FunctionAttribute("func tochar () : char")]
        public static HassiumChar tochar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumChar((char)Int);
        }

        [FunctionAttribute("func tofloat () : float")]
        public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumFloat(Int);
        }

        [FunctionAttribute("func toint () : int")]
        public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return self as HassiumInt;
        }

        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumString(Int.ToString());
        }

        [FunctionAttribute("func __xor__ (i : int) : int")]
        public static HassiumObject xor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            return new HassiumInt(Int ^ args[0].ToInt(vm, args[0], location).Int);
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
