using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumInt : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new IntTypeDef(); 

        public long Int { get; private set; }

        public HassiumInt(long val)
        {
            AddType(Number);
            AddType(TypeDefinition);
            Int = val;
        }

        public override HassiumObject Add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
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
            vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
            return Null;
        }

        public override HassiumObject BitshiftLeft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((int)Int << (int)args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject BitshiftRight(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((int)Int >> (int)args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject BitwiseAnd(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int & args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject BitwiseNot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(~Int);
        }

        public override HassiumObject BitwiseOr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int | args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject Divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int / (args[0] as HassiumInt).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int / (args[0] as HassiumFloat).Float);
            vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
            return this;
        }

        public override HassiumBool EqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int == args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject GreaterThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int > args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int >= args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject IntegerDivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int / args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject LesserThan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int < args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int <= args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject Modulus(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int % args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject Multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int * (args[0] as HassiumInt).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int * (args[0] as HassiumFloat).Float);
            vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
            return Null;
        }

        public override HassiumObject Negate(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(-Int);
        }

        public override HassiumBool NotEqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int != args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumObject Subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Int = (self as HassiumInt).Int;
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int - (args[0] as HassiumInt).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int - (args[0] as HassiumFloat).Float);
            vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
            return Null;
        }

        public override HassiumChar ToChar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)Int);
        }

        public override HassiumBool ToBool(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int == 1);
        }

        public override HassiumFloat ToFloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Int);
        }

        public override HassiumObject Xor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int ^ args[0].ToInt(vm, args[0], location).Int);
        }

        public override HassiumInt ToInt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        public override HassiumString ToString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Int.ToString());
        }

        [DocStr(
            "@desc A class representing a 64-bit integer.",
            "@returns int."
            )]
        public class IntTypeDef : HassiumTypeDefinition
        {
            public IntTypeDef() : base("int")
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
                    { INVOKE, new HassiumFunction(_new, 1)  },
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
                    { XOR, new HassiumFunction(xor, 1)  }
                };
            }

            [DocStr(
                "@desc Constructs a new int object using the specified value.",
                "@param val The value.",
                "@returns The new int object."
                )]
            [FunctionAttribute("func new (val : object) : int")]
            public static HassiumInt _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return args[0] as HassiumInt;
                return new HassiumInt(System.Convert.ToInt64(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Implements the + operator, adding this int to the specified number.",
                "@param num The number to add.",
                "@returns This int plus the number."
                )]
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
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Implements the << operator, shifting the bits in this int by the specified number of positions.",
                "@param num The number to shift by.",
                "@returns A new int with the bits shifted left."
                )]
            [FunctionAttribute("func __bitshiftleft__ (num : number) : int")]
            public static HassiumObject bitshiftleft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt((int)Int << (int)args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the >> oerator, shifting the bits in this int by the specified number of positions.",
                "@param num The number to shift by.",
                "@returns A new int with the bits shifted right."
                )]
            [FunctionAttribute("func __bitshiftright__ (num : number) : int")]
            public static HassiumObject bitshiftright(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt((int)Int >> (int)args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the & operator to perform a bitwise and operation on this int with the specified number.",
                "@param num The number to perform the and by.",
                "@returns A new int containing only the bits that were 1 between both numbers."
                )]
            [FunctionAttribute("func __bitwiseand__ (num : number) : int")]
            public static HassiumObject bitwiseand(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt(Int & args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the ~ operator to perform a bitwise not operation on this int.",
                "@returns A new int with all of the bytes in this int flipped."
                )]
            [FunctionAttribute("func __bitwisenot__ () : int")]
            public static HassiumObject bitwisenot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt(~Int);
            }

            [DocStr(
                "@desc Implements the | operator to perform a bitwise or operation on this int using the specified int.",
                "@param i The int to perform the or by.",
                "@returns A new int containing bits that were 1 in either of the ints."
                )]
            [FunctionAttribute("func __bitwiseor__ (i : int) : int")]
            public static HassiumObject bitwiseor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt(Int | args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the / operator to divide this int by the specified number.",
                "@param num The number to divide by.",
                "@returns This int divided by the number."
                )]
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
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return self;
            }

            [DocStr(
                "@desc Implements the == operator to determine if this int is equal to the specified number.",
                "@param num The number to compare.",
                "@returns true if the numbers are equal, otherwise false."
                )]
            [FunctionAttribute("func __equals__ (num : number) : bool")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumBool(Int == args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Gets the bit at the specified 0-based index in this int and returns it's value.",
                "@param index The 0-based index to get the bit at.",
                "@returns true if the bit is 0, otherwise false."
                )]
            [FunctionAttribute("func getbit (index : int) : bool")]
            public static HassiumBool getbit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumBool((Int & (1 << (int)args[0].ToInt(vm, args[0], location).Int - 1)) != 0);
            }

            [DocStr(
                "@desc Implements the > operator to determine if this int is greater than the specified number.",
                "@param num The number to compare.",
                "@returns true if this int is greater than the number, otherwise false."
                )]
            [FunctionAttribute("func __greater__ (num : number) : bool")]
            public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumBool(Int > args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the >= operator to determine if this int is greater than or equal to the specified number.",
                "@param num The number to compare.",
                "@returns true if this int is greater than or equal to the number, otherwise false."
                )]
            [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
            public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumBool(Int >= args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the // operator to divide this int by the specified number and return the closest integer value.",
                "@param num The number to divide by.",
                "@returns This int divided by the number to the nearest whole int."
                )]
            [FunctionAttribute("func __intdivision__ (num : number) : int")]
            public static HassiumObject integerdivision(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt(Int / args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the < operator to determine if this int is lesser than the specified number.",
                "@param num The number to compare.",
                "@returns true if this int is lesser than the number, otherwise false."
                )]
            [FunctionAttribute("func __lesser__ (num : number) : bool")]
            public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumBool(Int < args[0].ToInt(vm, args[0], location).Int);
            }
            [DocStr(
                "@desc Implements the <= operator to determine if this int is lesser than or equal to the specified number.",
                "@param num The number to compare.",
                "@returns true if this int is lesser than or equal to the number, otherwise false."
                )]
            [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
            public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumBool(Int <= args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the % operator to calculate the modulus of this int and the specified number.",
                "@param num The number to modulus by.",
                "@returns The modulus of this int and the number."
                )]
            [FunctionAttribute("func __modulus__ (i : int) : int")]
            public static HassiumObject modulus(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt(Int % args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the * operator to multiply this int by the specified number.",
                "@param num The number to multiply by.",
                "@returns This int times the number."
                )]
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
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Implements the unary - operator to return this int times -1.",
                "returns This int multiplied by -1."
                )]
            [FunctionAttribute("func __negate__ () : int")]
            public static HassiumObject negate(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt(-Int);
            }

            [DocStr(
                "@desc Implements the != operator to determine if this int is not equal to the specified int.",
                "@param i The int to compare.",
                "@returns true if the ints are not equal, otherwise false."
                )]
            [FunctionAttribute("func __notequal__ (i : int) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumBool(Int != args[0].ToInt(vm, args[0], location).Int);
            }

            [DocStr(
                "@desc Implements the ** operator to raise this int to the specified power.",
                "@param pow The power to raise this int to.",
                "@returns This int to the power of the number."
                )]
            [FunctionAttribute("func __power__ (pow : number) : int")]
            public static HassiumObject power(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt((long)System.Math.Pow((double)Int, (double)args[0].ToInt(vm, args[0], location).Int));
            }

            [DocStr(
                "@desc Sets the bit at the specified index to the specicied value.",
                "@param index The zero-based index to be set.",
                "@param val The bool value to set to, true for 1 and false for 0.",
                "@returns A new int with the value set."
                )]
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

            [DocStr(
                "@desc Implements the binary - operator to subtract the specified number from this int.",
                "@param num The number to subtract.",
                "@reutrns This int minux the number."
                )]
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
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Converts this int to a boolean and returns it.",
                "@returns true if this is 1, otherwise false."
                )]
            [FunctionAttribute("func tobool () : bool")]
            public static HassiumBool tobool(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumBool(Int == 1);
            }

            [DocStr(
                "@desc Converts this int to a char and returns it.",
                "@returns The char value of this int."
                )]
            [FunctionAttribute("func tochar () : char")]
            public static HassiumChar tochar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumChar((char)Int);
            }

            [DocStr(
                "@desc Converts this int to a float and returns it.",
                "@returns This int as float."
                )]
            [FunctionAttribute("func tofloat () : float")]
            public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumFloat(Int);
            }

            [DocStr(
                "@desc Returns this int.",
                "@returns This int."
                )]
            [FunctionAttribute("func toint () : int")]
            public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return self as HassiumInt;
            }

            [DocStr(
                "@desc Converts this int to a string and returns it.",
                "@returns This int as string."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumString(Int.ToString());
            }

            [DocStr(
                "@desc Implements the ^ operator to perform an xor operation on this int using the specified int.",
                "@param i The int to perform the xor by.",
                "@returns A new int containing bits that were opposing in each int."
                )]
            [FunctionAttribute("func __xor__ (i : int) : int")]
            public static HassiumObject xor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Int = (self as HassiumInt).Int;
                return new HassiumInt(Int ^ args[0].ToInt(vm, args[0], location).Int);
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
