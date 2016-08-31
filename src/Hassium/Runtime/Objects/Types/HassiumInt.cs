using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumInt: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("int");

        public long Int { get; private set; }

        public HassiumInt(long val)
        {
            Int = val;
            AddType(TypeDefinition);
            AddAttribute("getBit",      getBit,     1);
            AddAttribute("setBit",      setBit,     2);
            AddAttribute(HassiumObject.TOBOOL,  ToBool,     0);
            AddAttribute(HassiumObject.TOCHAR,  ToChar,     0);
            AddAttribute(HassiumObject.TOFLOAT, ToFloat,    0);
            AddAttribute(HassiumObject.TOINT,   ToInt,      0);
            AddAttribute(HassiumObject.TOSTRING,ToString,   0);
        }

        public HassiumBool getBit(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool((Int & (1 << (int)args[0].ToInt(vm).Int - 1)) != 0);
        }
        public HassiumInt setBit(VirtualMachine vm, params HassiumObject[] args)
        {
            int index = (int)args[0].ToInt(vm).Int;
            bool val = args[1].ToBool(vm).Bool;
            if (val)
                return new HassiumInt((int)Int | 1 << index);
            else
                return new HassiumInt(Int & ~(1 << index));
        }

        public override HassiumObject Add(VirtualMachine vm, params HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumString(Int + args[0].ToString(vm).String);
            return new HassiumInt(Int + args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject BitshiftLeft(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt((int)Int << (int)args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject BitshiftRight(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt((int)Int >> (int)args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject BitwiseAnd(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Int & args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject BitwiseNot(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(~Int);
        }
        public override HassiumObject BitwiseOr(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Int | args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject BitwiseXor(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Int ^ args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject Divide(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat(Int / args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject EqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Int == args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject GreaterThan(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Int > args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Int >= args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject IntegerDivision(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Int / args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject LesserThan(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Int < args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Int <= args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject LogicalNot(VirtualMachine vm, params HassiumObject[] args)
        {
            int total = 1;
            for (int i = 2; i <= Int; i++)
                total *= i;
            return new HassiumInt(total);
        }
        public override HassiumObject Modulus(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Int % args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject Multiply(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Int * args[0].ToInt(vm, args).Int);
        }
        public override HassiumObject Negate(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(-Int);
        }
        public override HassiumObject NotEqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            return EqualTo(vm, args).LogicalNot(vm, args);
        }
        public override HassiumObject Power(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Pow((double)Int, args[0].ToFloat(vm, args).Float));
        }
        public override HassiumObject Subtract(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Int - args[0].ToInt(vm, args).Int);
        }
        public override HassiumBool ToBool(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Int == 1 ? true : false);
        }
        public override HassiumChar ToChar(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar((char)Int);
        }
        public override HassiumFloat ToFloat(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat(Convert.ToDouble(Int));
        }
        public override HassiumInt ToInt(VirtualMachine vm, params HassiumObject[] args)
        {
            return this;
        }
        public override HassiumString ToString(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Int.ToString());
        }
    }
}

