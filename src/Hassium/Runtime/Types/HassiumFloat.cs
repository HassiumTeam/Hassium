using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumFloat : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("float");

        public Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { ADD, new HassiumFunction(add, 1)  },
            { DIVIDE, new HassiumFunction(divide, 1)  },
            { EQUALTO, new HassiumFunction(equalto, 1)  },
            { GREATERTHAN, new HassiumFunction(greaterthan, 1)  },
            { GREATERTHANOREQUAL, new HassiumFunction(greaterthanorequal, 1)  },
            { INTEGERDIVISION, new HassiumFunction(integerdivision, 1)  },
            { LESSERTHAN, new HassiumFunction(lesserthan, 1)  },
            { LESSERTHANOREQUAL, new HassiumFunction(lesserthanorequal, 1)  },
            { MULTIPLY, new HassiumFunction(multiply, 1)  },
            { NEGATE, new HassiumFunction(negate, 0)  },
            { NOTEQUALTO, new HassiumFunction(notequalto, 1)  },
            { POWER, new HassiumFunction(power, 1)  },
            { SUBTRACT, new HassiumFunction(subtract, 1)  },
            { TOFLOAT, new HassiumFunction(tofloat, 0)  },
            { TOINT, new HassiumFunction(toint, 0)  },
            { TOSTRING, new HassiumFunction(tostring, 0)  },
        };

        public double Float { get; private set; }

        public HassiumFloat(double val)
        {
            AddType(Number);
            AddType(TypeDefinition);
            Float = val;
        }

        [FunctionAttribute("func __add__ (num : number) : float")]
        public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumFloat(Float + args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __divide__ (num : number) : float")]
        public static HassiumObject divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumFloat(Float / args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __equals__ (num : number) : float")]
        public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumBool(Float == args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __greater__ (num : number) : bool")]
        public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumBool(Float > args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
        public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumBool(Float >= args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __intdivision__ (num : number) : int")]
        public static HassiumObject integerdivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumInt((long)Float / args[0].ToInt(vm, args[0], location).Int);
        }

        [FunctionAttribute("func __lesser__ (num : number) : bool")]
        public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumBool(Float < args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
        public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumBool(Float <= args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __multiply__ (num : number) : float")]
        public static HassiumObject multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumFloat(Float * args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __negate__ () : float")]
        public static HassiumObject negate(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumFloat(-Float);
        }

        [FunctionAttribute("func __notequal__ (f : float) : bool")]
        public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumBool(Float != args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func __power__ (pow : number) : float")]
        public static HassiumObject power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumFloat(System.Math.Pow(Float, args[0].ToFloat(vm, args[0], location).Float));
        }

        [FunctionAttribute("func __subtract__ (num : number) : float")]
        public static HassiumObject subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumFloat(Float - args[0].ToFloat(vm, args[0], location).Float);
        }

        [FunctionAttribute("func tofloat () : float")]
        public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return self as HassiumFloat;
        }

        [FunctionAttribute("func toint () : int")]
        public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumInt((long)Float);
        }

        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Float = (self as HassiumFloat).Float;
            return new HassiumString(Float.ToString());
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
