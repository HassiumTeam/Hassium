using Hassium.Compiler;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Hassium.Runtime.Types
{
    public class HassiumBigInt : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new BigIntType();

        public BigInteger BigInt { get; set; }

        public HassiumBigInt()
        {
            AddType(Number);
            AddType(TypeDefinition);
        }

        public class BigIntType : HassiumTypeDefinition
        {
            public BigIntType() : base("BigInt")
            {
                AddAttribute("abs", abs, 0);
                AddAttribute(ADD, add, 1);
                AddAttribute(DIVIDE, divide, 1);
                AddAttribute(EQUALTO, equalto, 1);
                AddAttribute(GREATERTHAN, greaterthan, 1);
                AddAttribute(GREATERTHANOREQUAL, greaterthanorequal, 1);
                AddAttribute(LESSERTHAN, lesserthan, 1);
                AddAttribute(LESSERTHANOREQUAL, lesserthanorequal, 1);
                AddAttribute(INVOKE, _new, 1);
                AddAttribute("log", log, 1);
                AddAttribute(MULTIPLY, multiply, 1);
                AddAttribute(NOTEQUALTO, notequalto, 1);
                AddAttribute(SUBTRACT, subtract, 1);
                AddAttribute(TOFLOAT, tofloat, 0);
                AddAttribute(TOINT, toint, 0);
                AddAttribute(TOLIST, tolist, 0);
                AddAttribute(TOSTRING, tostring, 0);
            }

            [DocStr(
                "@desc Constructs a new BigInt using the specified object, which is either a float, int, or list.",
                "@param obj The object that will be the BigInt, either a float, int, or list.",
                "@returns The new BigInt object."
                )]
            [FunctionAttribute("func new (obj : object) : BigInt")]
            public static HassiumBigInt _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumBigInt bigint = new HassiumBigInt();

                var type = args[0].Type();
                if (type == HassiumFloat.TypeDefinition)
                    bigint.BigInt = new BigInteger(args[0].ToFloat(vm, args[0], location).Float);
                else if (type == HassiumInt.TypeDefinition)
                    bigint.BigInt = new BigInteger(args[0].ToInt(vm, args[0], location).Int);
                else if (type == HassiumList.TypeDefinition)
                    bigint.BigInt = new BigInteger(ListToByteArr(vm, location, args[0].ToList(vm, args[0], location, args[0]) as HassiumList));
                else
                    bigint.BigInt = BigInteger.Parse(args[0].ToString(vm, args[0], location).String);

                return bigint;
            }
            
            [DocStr(
                "@desc Gets the absolute value of this BigInt.",
                "@returns The absolute value as BigInt."
                )]
            [FunctionAttribute("func abs () : BigInt")]
            public static HassiumBigInt abs(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumBigInt() { BigInt = BigInteger.Abs(BigInt) };
            }

            [DocStr(
                "@desc Implements the + operator to add to the BigInt.",
                "@param num The number to add.",
                "@returns This BigInt plus the number."
                )]
            [FunctionAttribute("func __add__ (num : number) : number")]
            public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var bigintarg = args[0] as HassiumBigInt;
                if (bigintarg != null)
                    return new HassiumBigInt() { BigInt = BigInteger.Add(BigInt, (bigintarg.BigInt)) };
                var intarg = args[0] as HassiumInt;
                if (intarg != null)
                    return new HassiumBigInt() { BigInt = BigInteger.Add(BigInt, new BigInteger(args[0].ToInt(vm, args[0], location).Int)) };
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Implements the / operator to divide from the BigInt.",
                "@param num The number to divide by.",
                "@returns This BigInt divided by the number."
                )]
            [FunctionAttribute("func __divide__ (num : number) : number")]
            public static HassiumObject divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var bigintarg = args[0] as HassiumBigInt;
                if (bigintarg != null)
                    return new HassiumBigInt() { BigInt = BigInteger.Divide(BigInt, (bigintarg.BigInt)) };
                var intarg = args[0] as HassiumInt;
                if (intarg != null)
                    return new HassiumBigInt() { BigInt = BigInteger.Divide(BigInt, new BigInteger(args[0].ToInt(vm, args[0], location).Int)) };
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Implements the == operator to determine equality of the BigInt.",
                "@param bigint The BigInt to compare.",
                "@returns true if the BigInts are equal, otherwise false."
                )]
            [FunctionAttribute("func __equals__ (bigint : BigInt) : bool")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var list = tolist(vm, self, location);
                return list.EqualTo(vm, list, location, (args[0] as HassiumBigInt).ToList(vm, args[0], location));
            }

            [DocStr(
                "@desc Implements the > operator to determine if this BigInt is greater than the specified num.",
                "@param num The number to compare.",
                "@returns true if this BigInt is greater than the number."
                )]
            [FunctionAttribute("func __greater__ (num : number) : bool")]
            public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var bigintarg = args[0] as HassiumBigInt;
                if (bigintarg != null)
                    return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) == 1);
                var intarg = args[0] as HassiumInt;
                if (intarg != null)
                    return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) == 1);
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Implements the >= operator to determine if this BigInt is greater than or equal to the specified num.",
                "@param num The number to compare.",
                "@returns true if this BigInt is greater than or equal to the number."
                )]
            [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
            public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var bigintarg = args[0] as HassiumBigInt;
                if (bigintarg != null)
                    return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) >= 0);
                var intarg = args[0] as HassiumInt;
                if (intarg != null)
                    return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) >= 0);
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Implements the < operator to determine if this BigInt is lesser than the specified num.",
                "@param num The number to compare.",
                "@returns true if this BigInt is lesser than the number."
                )]
            [FunctionAttribute("func __lesser__ (num : number) : bool")]
            public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var bigintarg = args[0] as HassiumBigInt;
                if (bigintarg != null)
                    return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) == -1);
                var intarg = args[0] as HassiumInt;
                if (intarg != null)
                    return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) == -1);
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Implements the <= operator to determine if this BigInt is lesser than or equal to the specified num.",
                "@param num The number to compare.",
                "@returns true if this BigInt is lesser than or equal to the number."
                )]
            [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
            public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var bigintarg = args[0] as HassiumBigInt;
                if (bigintarg != null)
                    return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) <= 0);
                var intarg = args[0] as HassiumInt;
                if (intarg != null)
                    return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) <= 0);
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Calculates the logarithm of this BigInt to the specified base.",
                "@param base The base for the logarithm.",
                "@returns This BigInt to the base."
                )]
            [FunctionAttribute("func log (base : float) : BigInt")]
            public static HassiumBigInt log(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return _new(vm, self, location, new HassiumFloat(BigInteger.Log(BigInt, args[0].ToFloat(vm, args[0], location).Float)));
            }

            [DocStr(
                "@desc Calculates the modpow value of the specified value, exponent, and modulus.",
                "@param val The value.",
                "@param exp The exponent.",
                "@param mod The modulus.",
                "@returns The modpow of the value, exponent, and modulus."
                )]
            [FunctionAttribute("func modpow (val : BigInt, exp : BigInt, mod : BigInt) : BigInt")]
            public static HassiumBigInt modpow(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBigInt() { BigInt = BigInteger.ModPow(args[0].ToBigInt(vm, args[0], location).BigInt, args[1].ToBigInt(vm, args[1], location).BigInt, args[2].ToBigInt(vm, args[2], location).BigInt) };
            }

            [DocStr(
                "@desc Implements the * operator to multiply this BigInt by the specified number.",
                "@param num The number to multiply by.",
                "@returns This BigInt times the number."
                )]
            [FunctionAttribute("func __multiply__ (num : number) : number")]
            public static HassiumObject multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var bigintarg = args[0] as HassiumBigInt;
                if (bigintarg != null)
                    return new HassiumBigInt() { BigInt = BigInteger.Multiply(BigInt, (bigintarg.BigInt)) };
                var intarg = args[0] as HassiumInt;
                if (intarg != null)
                    return new HassiumBigInt() { BigInt = BigInteger.Multiply(BigInt, new BigInteger(args[0].ToInt(vm, args[0], location).Int)) };
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Implements the != operator to determine if this BigInt is not equal to the specified BigInt.",
                "@param bigint The BigInt to compare to.",
                "@returns true if the BigInts are not equal, otherwise false."
                )]
            [FunctionAttribute("func __notequal__ (bigint : BigInt) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var list = tolist(vm, self, location);
                return list.NotEqualTo(vm, list, location, (args[0] as HassiumBigInt).ToList(vm, args[0], location));
            }

            [DocStr(
                "@desc Implements the - operator to calculate this BigInt minus the specified number.",
                "@param num The number to subtract.",
                "@returns This BigInt minus the number."
                )]
            [FunctionAttribute("func __subtract__ (num : number) : number")]
            public static HassiumObject subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var bigintarg = args[0] as HassiumBigInt;
                if (bigintarg != null)
                    return new HassiumBigInt() { BigInt = BigInteger.Subtract(BigInt, (bigintarg.BigInt)) };
                var intarg = args[0] as HassiumInt;
                if (intarg != null)
                    return new HassiumBigInt() { BigInt = BigInteger.Subtract(BigInt, new BigInteger(args[0].ToInt(vm, args[0], location).Int)) };
                vm.RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(vm, null, location, args[0], Number));
                return Null;
            }

            [DocStr(
                "@desc Converts this BigInt to a float and returns it.",
                "@returns The float value."
                )]
            [FunctionAttribute("func tofloat () : float")]
            public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumFloat(BitConverter.ToDouble(BigInt.ToByteArray(), 0));
            }

            [DocStr(
                "@desc Converts this BigInt to an integer and returns it.",
                "@returns The int value."
                )]
            [FunctionAttribute("func toint () : int")]
            public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumInt(BitConverter.ToInt64(BigInt.ToByteArray(), 0));
            }

            [DocStr(
                "@desc Converts this BigInt to a list of bytes and returns it.",
                "@returns The list value."
                )]
            [FunctionAttribute("func tolist () : list")]
            public static HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumByteArray(BigInt.ToByteArray(), new HassiumObject[0]);
            }

            [DocStr(
                "@desc Converts this BigInt to a string and returns it.",
                "@returns The string value."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumString(BigInt.ToString());
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
