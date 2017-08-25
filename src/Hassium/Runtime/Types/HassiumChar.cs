using Hassium.Compiler;

using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumChar : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new CharTypeDef();

        public char Char { get; private set; }

        public HassiumChar(char val)
        {
            AddType(Number);
            AddType(TypeDefinition);
            Char = val;
        }

        public override HassiumObject Add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)(Char + (char)args[0].ToInt(vm, args[0], location).Int));
        }

        public override HassiumObject BitshiftLeft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char << (int)args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject BitshiftRight(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char >> (int)args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject BitwiseAnd(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char & args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject BitwiseNot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(~Char);
        }

        public override HassiumObject BitwiseOr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char | args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject Divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char / args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumBool EqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Char == args[0].ToChar(vm, args[0], location).Char);
        }

        public override HassiumObject GreaterThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char > args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char >= args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject IntegerDivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char / args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject LesserThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char < args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char <= args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject Modulus(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char % args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject Multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char * args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumBool NotEqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Char != args[0].ToChar(vm, args[0], location).Char);
        }

        public override HassiumObject Power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Pow(Convert.ToDouble(Char), args[0].ToFloat(vm, args[0], location).Float));
        }

        public override HassiumObject Subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)(Char - (char)args[0].ToInt(vm, args[0], location).Int));
        }

        public override HassiumChar ToChar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        public override HassiumFloat ToFloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat((double)Char);
        }

        public override HassiumInt ToInt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((long)Char);
        }

        public override HassiumString ToString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Char.ToString());
        }

        public override HassiumObject Xor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)((byte)Char ^ (byte)args[0].ToChar(vm, args[0], location).Char));
        }

        public class CharTypeDef : HassiumTypeDefinition
        {
            public CharTypeDef() : base("char")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
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
                    { "iscontrol", new HassiumFunction(iscontrol, 0)  },
                    { "isdigit", new HassiumFunction(isdigit, 0)  },
                    { "isletter", new HassiumFunction(isletter, 0)  },
                    { "isletterordigit", new HassiumFunction(isletterordigit, 0)  },
                    { "islower", new HassiumFunction(islower, 0)  },
                    { "issymbol", new HassiumFunction(issymbol, 0)  },
                    { "isupper", new HassiumFunction(isupper, 0)  },
                    { "iswhitespace", new HassiumFunction(iswhitespace, 0)  },
                    { LESSERTHAN, new HassiumFunction(lesserthan, 1)  },
                    { LESSERTHANOREQUAL, new HassiumFunction(lesserthanorequal, 1)  },
                    { MODULUS, new HassiumFunction(modulus, 1)  },
                    { MULTIPLY, new HassiumFunction(multiply, 1)  },
                    { NOTEQUALTO, new HassiumFunction(notequalto, 1)  },
                    { "setbit", new HassiumFunction(setbit, 2)  },
                    { SUBTRACT, new HassiumFunction(subtract, 1)  },
                    { TOCHAR, new HassiumFunction(tochar, 0)  },
                    { TOFLOAT, new HassiumFunction(tofloat, 0)  },
                    { TOINT, new HassiumFunction(toint, 0)  },
                    { "tolower", new HassiumFunction(tolower, 0)  },
                    { TOSTRING, new HassiumFunction(tostring, 0)  },
                    { "toupper", new HassiumFunction(toupper, 0)  },
                    { XOR, new HassiumFunction(xor, 1)  }
                };
            }

            [FunctionAttribute("func __add__ (c : char) : char")]
            public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)(Char + (char)args[0].ToInt(vm, args[0], location).Int));
            }

            [FunctionAttribute("func __bitshiftleft__ (c : char) : int")]
            public static HassiumObject bitshiftleft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char << (int)args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __bitshiftright__ (c : char) : int")]
            public static HassiumObject bitshiftright(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char >> (int)args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __bitswiseand__ (c : char) : int")]
            public static HassiumObject bitwiseand(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char & args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __bitwisenot__ (c : char) : int")]
            public static HassiumObject bitwisenot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(~Char);
            }

            [FunctionAttribute("func __bitwiseor__ (c : char) : int")]
            public static HassiumObject bitwiseor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char | args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __divide__ (c : char) : int")]
            public static HassiumObject divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char / args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __equals__ (c : char) : bool")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(Char == args[0].ToChar(vm, args[0], location).Char);
            }

            [FunctionAttribute("func getbit (index : int) : bool")]
            public static HassiumBool getbit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(((byte)Char & (1 << (int)args[0].ToInt(vm, args[0], location).Int - 1)) != 0);
            }

            [FunctionAttribute("func __greater__ (i : int) : bool")]
            public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char > args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __greaterorequal__ (i : int) : bool")]
            public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char >= args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __intdivision__ (i : int) : int")]
            public static HassiumObject integerdivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char / args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func iscontrol () : bool")]
            public static HassiumBool iscontrol(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsControl(Char));
            }

            [FunctionAttribute("func isdigit () : bool")]
            public static HassiumBool isdigit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsDigit(Char));
            }

            [FunctionAttribute("func isletter () : bool")]
            public static HassiumBool isletter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsLetter(Char));
            }

            [FunctionAttribute("func isletterordigit () : bool")]
            public static HassiumBool isletterordigit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsLetterOrDigit(Char));
            }

            [FunctionAttribute("func islower () : bool")]
            public static HassiumBool islower(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char >= 97 && (int)Char <= 122);
            }

            [FunctionAttribute("func issymbol () : bool")]
            public static HassiumBool issymbol(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsSymbol(Char));
            }

            [FunctionAttribute("func isupper () : bool")]
            public static HassiumBool isupper(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char >= 65 && (int)Char <= 90);
            }

            [FunctionAttribute("func iswhitespace () : bool")]
            public static HassiumBool iswhitespace(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsWhiteSpace(Char));
            }

            [FunctionAttribute("func __lesser__ (i : int) : bool")]
            public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char < args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __lesserorequal__ (i : int) : bool")]
            public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char <= args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __modulus__ (i : int) : int")]
            public static HassiumObject modulus(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char % args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __multiply__ (i : int) : int")]
            public static HassiumObject multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char * args[0].ToInt(vm, args[0], location).Int);
            }

            [FunctionAttribute("func __notequal__ (c : char) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(Char != args[0].ToChar(vm, args[0], location).Char);
            }

            [FunctionAttribute("func __power__ () : float")]
            public static HassiumObject power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumFloat(System.Math.Pow(Convert.ToDouble(Char), args[0].ToFloat(vm, args[0], location).Float));
            }

            [FunctionAttribute("func setbit (index : int, val : bool) : char")]
            public static HassiumChar setbit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                int index = (int)args[0].ToInt(vm, args[0], location).Int;
                bool val = args[1].ToBool(vm, args[1], location).Bool;
                if (val)
                    return new HassiumChar((char)(Char | 1 << index));
                else
                    return new HassiumChar((char)(Char & ~(1 << index)));
            }

            [FunctionAttribute("func __subtract__ (i : int) : char")]
            public static HassiumObject subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)(Char - (char)args[0].ToInt(vm, args[0], location).Int));
            }

            [FunctionAttribute("func tochar () : char")]
            public static HassiumChar tochar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return self as HassiumChar;
            }

            [FunctionAttribute("func tofloat () : float")]
            public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumFloat((double)Char);
            }

            [FunctionAttribute("func toint () : int")]
            public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt((long)Char);
            }

            [FunctionAttribute("func tolower () : char")]
            public static HassiumChar tolower(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar(Char.ToLower(Char));
            }

            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumString(Char.ToString());
            }

            [FunctionAttribute("func toupper () : char")]
            public static HassiumChar toupper(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar(Char.ToUpper(Char));
            }

            [FunctionAttribute("func __xor__ (c : char) : char")]
            public static HassiumObject xor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)((byte)Char ^ (byte)args[0].ToChar(vm, args[0], location).Char));
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
