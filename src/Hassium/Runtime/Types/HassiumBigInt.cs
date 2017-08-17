using Hassium.Compiler;

using System;
using System.Numerics;

namespace Hassium.Runtime.Types
{
    public class HassiumBigInt : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("BigInt");

        public BigInteger BigInt { get; set; }

        public HassiumBigInt()
        {
            AddType(Number);
            AddType(TypeDefinition);

            AddAttribute(INVOKE, _new, 1);
            AddAttribute("modpow", modpow, 3);
            ImportAttribs(this);
        }

        [FunctionAttribute("func new (obj : object) : BigInt")]
        public static HassiumBigInt _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumBigInt bigint = new HassiumBigInt();
            
            var type = args[0].Type();
            if (type == HassiumFloat.TypeDefinition)
                bigint.BigInt = new BigInteger(args[0].ToFloat(vm, location).Float);
            else if (type == HassiumInt.TypeDefinition)
                bigint.BigInt = new BigInteger(args[0].ToInt(vm, location).Int);
            else if (type == HassiumList.TypeDefinition)
                bigint.BigInt = new BigInteger(ListToByteArr(vm, location, args[0].ToList(vm, location, args[0]) as HassiumList));
            else
                bigint.BigInt = BigInteger.Parse(args[0].ToString(vm, location).String);

            return ImportAttribs(bigint);
        }

        public static HassiumBigInt ImportAttribs(HassiumBigInt bigint)
        {
            bigint.AddAttribute("abs", bigint.Abs, 0);
            bigint.AddAttribute(ADD, bigint.Add, 1);
            bigint.AddAttribute(DIVIDE, bigint.Divide, 1);
            bigint.AddAttribute(EQUALTO, bigint.EqualTo, 1);
            bigint.AddAttribute(GREATERTHAN, bigint.GreaterThan, 1);
            bigint.AddAttribute(GREATERTHANOREQUAL, bigint.GreaterThanOrEqual, 1);
            bigint.AddAttribute(LESSERTHAN, bigint.LesserThan, 1);
            bigint.AddAttribute(LESSERTHANOREQUAL, bigint.LesserThanOrEqual, 1);
            bigint.AddAttribute("log", bigint.log);
            bigint.AddAttribute(MULTIPLY, bigint.Multiply, 1);
            bigint.AddAttribute(NOTEQUALTO, bigint.NotEqualTo, 1);
            bigint.AddAttribute(SUBTRACT, bigint.Subtract, 1);
            bigint.AddAttribute(TOFLOAT, bigint.ToFloat, 0);
            bigint.AddAttribute(TOINT, bigint.ToInt, 0);
            bigint.AddAttribute(TOLIST, bigint.ToList, 0);
            bigint.AddAttribute(TOSTRING, bigint.ToString, 0);

            return bigint;
        }

        [FunctionAttribute("func abs () : BigInt")]
        public HassiumBigInt Abs(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Abs(BigInt) });
        }

        [FunctionAttribute("func __add__ (num : number) : number")]
        public override HassiumObject Add(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Add(BigInt, (bigintarg.BigInt)) });
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Add(BigInt, new BigInteger(args[0].ToInt(vm, location).Int)) });
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __divide__ (num : number) : number")]
        public override HassiumObject Divide(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Divide(BigInt, (bigintarg.BigInt)) });
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Divide(BigInt, new BigInteger(args[0].ToInt(vm, location).Int)) });
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __equals__ (bigint : BigInt) : bool")]
        public override HassiumBool EqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return ToList(vm, location).EqualTo(vm, location, (args[0] as HassiumBigInt).ToList(vm, location));
        }

        [FunctionAttribute("func __greater__ (num : number) : bool")]
        public override HassiumObject GreaterThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) == 1);
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) == 1);
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) >= 0);
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) >= 0);
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __lesser__ (num : number) : bool")]
        public override HassiumObject LesserThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) == -1);
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) == -1);
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) <= 0);
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) <= 0);
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func log (base : float) : BigInt")]
        public HassiumBigInt log(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return _new(vm, location, new HassiumFloat(BigInteger.Log(BigInt, args[0].ToFloat(vm, location).Float)));
        }

        [FunctionAttribute("func modpow (val : BigInt, exp : BigInt, mod : BigInt) : BigInt")]
        public HassiumBigInt modpow(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.ModPow(args[0].ToBigInt(vm, location).BigInt, args[1].ToBigInt(vm, location).BigInt, args[2].ToBigInt(vm, location).BigInt) });
        }

        [FunctionAttribute("func __multiply__ (num : number) : number")]
        public override HassiumObject Multiply(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Multiply(BigInt, (bigintarg.BigInt)) });
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Multiply(BigInt, new BigInteger(args[0].ToInt(vm, location).Int)) });
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __notequal__ (bigint : BigInt) : bool")]
        public override HassiumBool NotEqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return ToList(vm, location).NotEqualTo(vm, location, (args[0] as HassiumBigInt).ToList(vm, location));
        }

        [FunctionAttribute("func __subtract__ (num : number) : number")]
        public override HassiumObject Subtract(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Subtract(BigInt, (bigintarg.BigInt)) });
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Subtract(BigInt, new BigInteger(args[0].ToInt(vm, location).Int)) });
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func tofloat () : float")]
        public override HassiumFloat ToFloat(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(BitConverter.ToDouble(BigInt.ToByteArray(), 0));
        }

        [FunctionAttribute("func toint () : int")]
        public override HassiumInt ToInt(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(BitConverter.ToInt64(BigInt.ToByteArray(), 0));
        }

        [FunctionAttribute("func tolist () : list")]
        public override HassiumList ToList(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumByteArray(BigInt.ToByteArray(), new HassiumObject[0]);
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(BigInt.ToString());
        }
    }
}
