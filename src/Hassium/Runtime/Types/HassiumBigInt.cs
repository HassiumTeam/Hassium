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
            
            [FunctionAttribute("func abs () : BigInt")]
            public static HassiumBigInt abs(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumBigInt() { BigInt = BigInteger.Abs(BigInt) };
            }

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
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
                return Null;
            }

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
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
                return Null;
            }

            [FunctionAttribute("func __equals__ (bigint : BigInt) : bool")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var list = tolist(vm, self, location);
                return list.EqualTo(vm, list, location, (args[0] as HassiumBigInt).ToList(vm, args[0], location));
            }

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
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
                return Null;
            }

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
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
                return Null;
            }

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
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
                return Null;
            }

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
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
                return Null;
            }

            [FunctionAttribute("func log (base : float) : BigInt")]
            public static HassiumBigInt log(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return _new(vm, self, location, new HassiumFloat(BigInteger.Log(BigInt, args[0].ToFloat(vm, args[0], location).Float)));
            }

            [FunctionAttribute("func modpow (val : BigInt, exp : BigInt, mod : BigInt) : BigInt")]
            public static HassiumBigInt modpow(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBigInt() { BigInt = BigInteger.ModPow(args[0].ToBigInt(vm, args[0], location).BigInt, args[1].ToBigInt(vm, args[1], location).BigInt, args[2].ToBigInt(vm, args[2], location).BigInt) };
            }

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
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
                return Null;
            }

            [FunctionAttribute("func __notequal__ (bigint : BigInt) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                var list = tolist(vm, self, location);
                return list.NotEqualTo(vm, list, location, (args[0] as HassiumBigInt).ToList(vm, args[0], location));
            }

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
                vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
                return Null;
            }

            [FunctionAttribute("func tofloat () : float")]
            public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumFloat(BitConverter.ToDouble(BigInt.ToByteArray(), 0));
            }

            [FunctionAttribute("func toint () : int")]
            public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumInt(BitConverter.ToInt64(BigInt.ToByteArray(), 0));
            }

            [FunctionAttribute("func tolist () : list")]
            public static HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var BigInt = (self as HassiumBigInt).BigInt;
                return new HassiumByteArray(BigInt.ToByteArray(), new HassiumObject[0]);
            }

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

        public override HassiumObject GetAttribute(string attrib)
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
