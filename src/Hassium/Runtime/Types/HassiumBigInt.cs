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

            return bigint;
        }

        public static HassiumBigInt ImportAttribs(HassiumBigInt bigint)
        {
            bigint.AddAttribute(ADD, bigint.Add, 1);
            bigint.AddAttribute(TOFLOAT, bigint.ToFloat, 0);
            bigint.AddAttribute(TOINT, bigint.ToInt, 0);
            bigint.AddAttribute(TOLIST, bigint.ToList, 0);
            bigint.AddAttribute(TOSTRING, bigint.ToString, 0);

            return bigint;
        }

        [FunctionAttribute("func __add__ (num : number) : number")]
        public override HassiumObject Add(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var bigintarg = args[0] as HassiumBigInt;
            return ImportAttribs(new HassiumBigInt() { BigInt = BigInteger.Add(BigInt, new BigInteger(args[0].ToInt(vm, location).Int)) });
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
