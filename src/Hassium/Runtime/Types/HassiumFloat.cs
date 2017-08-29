using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumFloat : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new FloatTypeDef();
        
        public double Float { get; private set; }

        public HassiumFloat(double val)
        {
            AddType(Number);
            AddType(TypeDefinition);
            Float = val;
        }

        public override HassiumObject Add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.add(vm, this, location, args);
        }

        public override HassiumObject Divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.divide(vm, this, location, args);
        }

        public override HassiumBool EqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.equalto(vm, this, location, args);
        }

        public override HassiumObject GreaterThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.greaterthan(vm, this, location, args);
        }

        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.greaterthanorequal(vm, this, location, args);
        }

        public override HassiumObject IntegerDivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.integerdivision(vm, this, location, args);
        }

        public override HassiumObject LesserThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.lesserthan(vm, this, location, args);
        }

        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.lesserthanorequal(vm, this, location, args);
        }

        public override HassiumObject Multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.multiply(vm, this, location, args);
        }

        public override HassiumObject Negate(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.negate(vm, this, location, args);
        }

        public override HassiumBool NotEqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.notequalto(vm, this, location, args);
        }

        public override HassiumObject Power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.power(vm, this, location, args);
        }

        public override HassiumObject Subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.subtract(vm, this, location, args);
        }

        public override HassiumFloat ToFloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        public override HassiumInt ToInt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.toint(vm, this, location, args);
        }

        public override HassiumString ToString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return FloatTypeDef.tostring(vm, this, location, args);
        }

        public class FloatTypeDef : HassiumTypeDefinition
        {
            public FloatTypeDef() : base("float")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
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
            }

            [DocStr(
                "@desc Implements the + operator, adding this float to the specified number.",
                "@param num The number to add.",
                "@returns This float plus the number."
                )]
            [FunctionAttribute("func __add__ (num : number) : float")]
            public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumFloat(Float + args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the / operator, dividing this float by the specified number.",
                "@param num The number to divide by.",
                "@returns This float divided by the number."
                )]
            [FunctionAttribute("func __divide__ (num : number) : float")]
            public static HassiumObject divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumFloat(Float / args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the == operator to determine if both numbers are equal.",
                "@param num The number to compare.",
                "@returns true if both numbers are equal, otherwise false."
                )]
            [FunctionAttribute("func __equals__ (num : number) : float")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumBool(Float == args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the > operator to determine if this float is greater than the specified number.",
                "@param num The number to compare.",
                "@returns true if this float is greater than the number, otherwise false."
                )]
            [FunctionAttribute("func __greater__ (num : number) : bool")]
            public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumBool(Float > args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the >= operator to determine if this float is greater than or equal to the specified number.",
                "@param num The number to compare.",
                "2returns true if this float is greater than or equal to the number, otherwise false."
                )]
            [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
            public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumBool(Float >= args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the // operator to divide this float by the specified number and return the closest integer value.",
                "@param num The number to divide by.",
                "@returns This float divided by the number to the nearest whole int."
                )]
            [FunctionAttribute("func __intdivision__ (num : number) : int")]
            public static HassiumObject integerdivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumInt((long)Float / args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the < operator to determine if this float is lesser than the specified number.",
                "@param num The number to compare.",
                "@returns true if this float is lesser than the number, otherwise false."
                )]
            [FunctionAttribute("func __lesser__ (num : number) : bool")]
            public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumBool(Float < args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the <= operator to determine if this float is lesser than or equal to the specified number.",
                "@param num The number to compare.",
                "@returns true if this float is lesser than or equal to the number, otherwise false."
                )]
            [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
            public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumBool(Float <= args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the * operator, multiplying this float by the specified number.",
                "@param num The number to multiply by.",
                "@returns This float multiplied by the number."
                )]
            [FunctionAttribute("func __multiply__ (num : number) : float")]
            public static HassiumObject multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumFloat(Float * args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the unary - operator, to return this float times -1.",
                "@returns This float multiplied by -1."
                )]
            [FunctionAttribute("func __negate__ () : float")]
            public static HassiumObject negate(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumFloat(-Float);
            }

            [DocStr(
                "@desc Implements the != operator to determine if this float is not equal to the specified float.",
                "@param num The number to compare.",
                "@returns true if this float is not equal to the number, otherwise false."
                )]
            [FunctionAttribute("func __notequal__ (num : number) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumBool(Float != args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Implements the ** operator to raise this float to the specified power.",
                "@param pow The power to raise to.",
                "@returns This float to the power of the number."
                )]
            [FunctionAttribute("func __power__ (pow : number) : float")]
            public static HassiumObject power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumFloat(System.Math.Pow(Float, args[0].ToFloat(vm, args[0], location).Float));
            }

            [DocStr(
                "@desc Implements the - binary operator to subtract the specified number from this float.",
                "@param num The number to subtract.",
                "@returns This float minux the specified number."
                )]
            [FunctionAttribute("func __subtract__ (num : number) : float")]
            public static HassiumObject subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumFloat(Float - args[0].ToFloat(vm, args[0], location).Float);
            }

            [DocStr(
                "@desc Returns this float.",
                "@returns This float."
                )]
            [FunctionAttribute("func tofloat () : float")]
            public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return self as HassiumFloat;
            }

            [DocStr(
                "@desc Converts this float to an integer and returns it.",
                "@returns This float as int."
                )]
            [FunctionAttribute("func toint () : int")]
            public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumInt((long)Float);
            }

            [DocStr(
                "@desc Converts this float to a string and returns it.",
                "@returns This float as string."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Float = (self as HassiumFloat).Float;
                return new HassiumString(Float.ToString());
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
