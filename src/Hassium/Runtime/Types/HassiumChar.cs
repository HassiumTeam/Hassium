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

        [DocStr(
            "@desc A class representing a byte-sized character.",
            "@returns char."
            )]
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
                    { INVOKE, new HassiumFunction(_new, 1) },
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

            [DocStr(
                "@desc Constructs a new char object using the specified value.",
                "@param val The value.",
                "@returns The new char object."
            )]
            [FunctionAttribute("func new (val : object) : char")]
            public static HassiumChar _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                if (args[0] is HassiumChar)
                    return args[0] as HassiumChar;
                if (args[0] is HassiumFloat)
                    return new HassiumChar((char)(int)(args[0] as HassiumFloat).Float);
                if (args[0] is HassiumInt)
                    return new HassiumChar((char)(args[0] as HassiumInt).Int);
                if (args[0] is HassiumString)
                    return new HassiumChar((args[0] as HassiumString).String[0]);

                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], HassiumChar.TypeDefinition));
                return new HassiumChar('\0');
            }

            [DocStr(
                "@desc Implements the + operator to add to this char.",
                "@param c The char to add.",
                "@returns This char plus the char."
                )]
            [FunctionAttribute("func __add__ (c : char) : char")]
            public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)(Char + (char)args[0].ToInt(vm, args[0], location).Int));
            }

            [DocStr(
                "@desc Implements the << operator to perform a left bitshift of this char for the specified anmount of positions.",
                "@param num The number to shift left by.",
                "@returns The new char that has been shifted left."
                )]
            [FunctionAttribute("func __bitshiftleft__ (num : number) : char")]
            public static HassiumObject bitshiftleft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)(Char << (int)args[0].ToInt(vm, args[0], location).Int));
            }

            [DocStr(
                "@desc Implements the >> operator to perform a right bitshift of this char for the specified amount of positions.",
                "@param num The number to shift right by.",
                "@returns This new char that has been shifted right."
                )]
            [FunctionAttribute("func __bitshiftright__ (num : number) : char")]
            public static HassiumObject bitshiftright(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)(Char >> (int)args[0].ToInt(vm, args[0], location).Int));
            }

            [DocStr(
                "@desc Implements the & operator to perform a bitwise and operation on this char with the specified char.",
                "@param c The char to perform the and by.",
                "@returns A new char containing only the bits that were 1 between both chars."
                )]
            [FunctionAttribute("func __bitswiseand__ (c : char) : char")]
            public static HassiumObject bitwiseand(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)(Char & args[0].ToInt(vm, args[0], location).Int));
            }

            [DocStr(
                "@desc Implements the ~ operator to perform a bitwise not operation on this char.",
                "@returns A new char with all of the bytes in this char flipped."
                )]
            [FunctionAttribute("func __bitwisenot__ () : char")]
            public static HassiumObject bitwisenot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)~Char);
            }

            [DocStr(
                "@desc Implements the | operator to perform a bitwise or operation on this char using the specified char.",
                "@param c The char to perform the or by.",
                "@returns A new char containing bits that were 1 in either of the chars."
                )]
            [FunctionAttribute("func __bitwiseor__ (c : char) : char")]
            public static HassiumObject bitwiseor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)(Char | args[0].ToInt(vm, args[0], location).Int));
            }

            [DocStr(
                "@desc Implements the / operator, returning this char divided by the specified number.",
                "@param num The number to divide by.",
                "@returns This char divided by the number."
                )]
            [FunctionAttribute("func __divide__ (num : number) : int")]
            public static HassiumObject divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char / args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the == operator, returning a boolean indicating if both chars are equal.",
                "@param c The char to compare.",
                "@returns true if both chars have the same value, otherwise false."
                )]
            [FunctionAttribute("func __equals__ (c : char) : bool")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(Char == args[0].ToChar(vm, args[0], location).Char);
            }

            [DocStr(
                "@desc Gets the bit at the specified 0-based index in this char and returns it's value.",
                "@param index The 0-based index to get the bit at.",
                "@returns true if the bit is 0, otherwise false."
                )]
            [FunctionAttribute("func getbit (index : int) : bool")]
            public static HassiumBool getbit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(((byte)Char & (1 << (int)args[0].ToInt(vm, args[0], location).Int - 1)) != 0);
            }

            [DocStr(
                "@desc Implements the > operator to determine if this char is greater than the specified number.",
                "@param num The number to compare.",
                "@returns true if this char is greater than the specified number, otherwise false."
                )]
            [FunctionAttribute("func __greater__ (num : number) : bool")]
            public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char > args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the >= operator to determine if this char is greater than or equal to the specified number.",
                "@param num The number to compare.",
                "@returns true if this char is greater than or equal to the specified number, otherwise false."
                )]
            [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
            public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char >= args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the // operator to divide this char by the specified number and return the closest integer value.",
                "@param num The number to divide by.",
                "@returns This char divided by the number to the nearest whole int."
                )]
            [FunctionAttribute("func __intdivision__ (num : number) : int")]
            public static HassiumObject integerdivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char / args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Returns a boolean indicating if this char is a control char.",
                "@returns true if this is a control char, otherwise false."
                )]
            [FunctionAttribute("func iscontrol () : bool")]
            public static HassiumBool iscontrol(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsControl(Char));
            }

            [DocStr(
                "@desc Returns a boolean indicating if this char is a digit (0-9).",
                "@returns true if this is a digit, otherwise false."
                )]
            [FunctionAttribute("func isdigit () : bool")]
            public static HassiumBool isdigit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsDigit(Char));
            }
            
            [DocStr(
                "@desc Returns a boolean indicating if this char is a letter (a-z, A-Z).",
                "@returns true if this is a letter, otherwise false."
                )]
            [FunctionAttribute("func isletter () : bool")]
            public static HassiumBool isletter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsLetter(Char));
            }

            [DocStr(
                "@desc Returns a boolean indicating if this char is a letter or digit (a-z, A-Z, 0-9).",
                "@returns true if this is a letter or digit, otherwise false."
                )]
            [FunctionAttribute("func isletterordigit () : bool")]
            public static HassiumBool isletterordigit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsLetterOrDigit(Char));
            }

            [DocStr(
                "@desc Returns a boolean indicating if this char is a letter that is lowercase (a-z).",
                "@returns true if this is a lowercase letter, otherwise false."
                )]
            [FunctionAttribute("func islower () : bool")]
            public static HassiumBool islower(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char >= 97 && (int)Char <= 122);
            }

            [DocStr(
                "@desc Returns a boolean indicating if this char is a symbol.",
                "@returns true if this a symbol, otherwise false."
                )]
            [FunctionAttribute("func issymbol () : bool")]
            public static HassiumBool issymbol(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsSymbol(Char));
            }

            [DocStr(
                "@desc Returns a boolean indicating if this char is a letter that is uppercase (A-Z).",
                "@returns true if this is an uppercase letter, otherwise false."
                )]
            [FunctionAttribute("func isupper () : bool")]
            public static HassiumBool isupper(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char >= 65 && (int)Char <= 90);
            }

            [DocStr(
                "@desc Returns a boolean indicating if this char is a whitespace character.",
                "@returns true if this is whitespace, otherwise false."
                )]
            [FunctionAttribute("func iswhitespace () : bool")]
            public static HassiumBool iswhitespace(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(char.IsWhiteSpace(Char));
            }

            [DocStr(
                "@desc Implements the < operator to determine if this char is lesser than the specified number.",
                "@param num The number to compare.",
                "@returns true if this is lesser than the number, otherwise false."
                )]
            [FunctionAttribute("func __lesser__ (num : number) : bool")]
            public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char < args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the <= operator to determine if this char is lesser than or equal to the specified number.",
                "@returns true if this is lesser than or equal to the number, otherwise false."
                )]
            [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
            public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool((int)Char <= args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the % operator to calculate the modulus of this char and the specified number.",
                "@param num The number to modulus by.",
                "@returns The modulus of this char and the number."
                )]
            [FunctionAttribute("func __modulus__ (num : number) : int")]
            public static HassiumObject modulus(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char % args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the * operator to multiply this char by the specified number.",
                "@param num The number to multiply by.",
                "@returns This char multiplied by the number."
                )]
            [FunctionAttribute("func __multiply__ (num : number) : int")]
            public static HassiumObject multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt(Char * args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the != operator to determine if this char is not equal to the specified char.",
                "@param c The char to compare.",
                "@returns true if the chars are not equal, otherwise false."
                )]
            [FunctionAttribute("func __notequal__ (c : char) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumBool(Char != args[0].ToChar(vm, args[0], location).Char);
            }

            [DocStr(
                "@desc Raises this char to the specified power.",
                "@param pow The power to raise this char to.",
                "@returns This char to the specified power as float."
                )]
            [FunctionAttribute("func __power__ (pow : number) : float")]
            public static HassiumObject power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumFloat(System.Math.Pow(Convert.ToDouble(Char), args[0].ToFloat(vm, args[0], location).Float));
            }

            [DocStr(
                "@desc Sets the bit at the specified index to the specicied value.",
                "@param index The zero-based index to be set.",
                "@param val The bool value to set to, true for 1 and false for 0.",
                "@returns A new char with the value set."
                )]
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

            [DocStr(
                "@desc Implements the - operator to subtract the specified number from this char.",
                "@param num The number to subtract.",
                "@returns This char minus the number."
                )]
            [FunctionAttribute("func __subtract__ (num : number) : char")]
            public static HassiumObject subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar((char)(Char - (char)args[0].ToInt(vm, args[0], location).Int));
            }

            [DocStr(
                "@desc Returns this char.",
                "@returns This char."
                )]
            [FunctionAttribute("func tochar () : char")]
            public static HassiumChar tochar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return self as HassiumChar;
            }

            [DocStr(
                "@desc Converts this char to a float and returns it.",
                "@returns The float value of this char."
                )]
            [FunctionAttribute("func tofloat () : float")]
            public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumFloat((double)Char);
            }
            
            [DocStr(
                "@desc Converts this char to an integer and returns it.",
                "@returns The int value of this char."
                )]
            [FunctionAttribute("func toint () : int")]
            public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumInt((long)Char);
            }

            [DocStr(
                "@desc Returns the lowercase value of this char.",
                "@returns The lowercase value."
                )]
            [FunctionAttribute("func tolower () : char")]
            public static HassiumChar tolower(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar(Char.ToLower(Char));
            }

            [DocStr(
                "@desc Converts this char to a string and returns it.",
                "@returns The string value of this char."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumString(Char.ToString());
            }

            [DocStr(
                "@desc Returns the uppercase value of this char.",
                "@returns The uppercase value."
                )]
            [FunctionAttribute("func toupper () : char")]
            public static HassiumChar toupper(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Char = (self as HassiumChar).Char;
                return new HassiumChar(Char.ToUpper(Char));
            }

            [DocStr(
                "@desc Implements the ^ operator to perform an xor operation on this char using the specified char.",
                "@param c The char to perform the xor by.",
                "@returns A new char containing bits that were opposing in each char."
                )]
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
