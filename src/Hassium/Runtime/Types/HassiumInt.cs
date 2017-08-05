using Hassium.Compiler;

namespace Hassium.Runtime.Types
{
    public class HassiumInt : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("int");

        public long Int { get; private set; }

        public HassiumInt(long val)
        {
            AddType(Number);
            AddType(TypeDefinition);
            Int = val;

            AddAttribute(ADD, Add, 1);
            AddAttribute(BITSHIFTLEFT, BitshiftLeft, 1);
            AddAttribute(BITSHIFTRIGHT, BitshiftRight, 1);
            AddAttribute(BITWISEAND, BitwiseAnd, 1);
            AddAttribute(BITWISENOT, BitwiseNot, 0);
            AddAttribute(BITWISEOR, BitwiseOr, 1);
            AddAttribute(DIVIDE, Divide, 1);
            AddAttribute(EQUALTO, EqualTo, 1);
            AddAttribute("getbit", getbit, 1);
            AddAttribute(GREATERTHAN, GreaterThan, 1);
            AddAttribute(GREATERTHANOREQUAL, GreaterThanOrEqual, 1);
            AddAttribute(INTEGERDIVISION, IntegerDivision, 1);
            AddAttribute(LESSERTHAN, LesserThan, 1);
            AddAttribute(LESSERTHANOREQUAL, LesserThanOrEqual, 1);
            AddAttribute(MULTIPLY, Multiply, 1);
            AddAttribute(NEGATE, Negate, 0);
            AddAttribute(NOTEQUALTO, NotEqualTo, 1);
            AddAttribute(POWER, Power, 1);
            AddAttribute("setbit", setbit, 2);
            AddAttribute(SUBTRACT, Subtract, 1);
            AddAttribute(TOCHAR, ToChar, 0);
            AddAttribute(TOFLOAT, ToFloat, 0);
            AddAttribute(TOINT, ToInt, 0);
            AddAttribute(TOSTRING, ToString, 0);
            AddAttribute(XOR, Xor, 1);
        }


        [FunctionAttribute("func __add__ (num : number) : number")]
        public override HassiumObject Add(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var charArg = args[0] as HassiumChar;
            if (charArg != null)
                return new HassiumInt(Int + args[0].ToChar(vm, location).Char);
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int + args[0].ToInt(vm, location).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int + args[0].ToFloat(vm, location).Float);
            var strArg = args[0] as HassiumString;
            if (strArg != null)
                return new HassiumString(Int.ToString() + args[0].ToString(vm, location).String);
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return this;
        }

        [FunctionAttribute("func __bitshiftleft__ (i : int) : int")]
        public override HassiumObject BitshiftLeft(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((int)Int << (int)args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __bitshiftright__ (i : int) : int")]
        public override HassiumObject BitshiftRight(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((int)Int >> (int)args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __bitwiseand__ (i : int) : int")]
        public override HassiumObject BitwiseAnd(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int & args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __bitwisenot__ () : int")]
        public override HassiumObject BitwiseNot(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(~Int);
        }

        [FunctionAttribute("func __bitwiseor__ (i : int) : int")]
        public override HassiumObject BitwiseOr(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int | args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __divide__ (num : number) : number")]
        public override HassiumObject Divide(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int / args[0].ToInt(vm, location).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int / args[0].ToFloat(vm, location).Float);
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return this;
        }

        [FunctionAttribute("func __equals__ ")]
        public override HassiumBool EqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int == args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func getbit (index : int) : bool")]
        public HassiumBool getbit(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((Int & (1 << (int)args[0].ToInt(vm, location).Int - 1)) != 0);
        }

        [FunctionAttribute("func __greater__ (num : number) : bool")]
        public override HassiumObject GreaterThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int > args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int >= args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __intdivision__ (num : number) : int")]
        public override HassiumObject IntegerDivision(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int / args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __lesser__ (num : number) : bool")]
        public override HassiumObject LesserThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int < args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int <= args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __modulus__ (i : int) : int")]
        public override HassiumObject Modulus(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int % args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __multiply__ (num : number) : number")]
        public override HassiumObject Multiply(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int * args[0].ToInt(vm, location).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int * args[0].ToFloat(vm, location).Float);
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return this;
        }

        [FunctionAttribute("func __negate__ () : int")]
        public override HassiumObject Negate(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(-Int);
        }

        [FunctionAttribute("func __notequal__ (i : int) : bool")]
        public override HassiumBool NotEqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int != args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __power__ (pow : number) : int")]
        public override HassiumObject Power(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((long)System.Math.Pow((double)Int, (double)args[0].ToInt(vm, location).Int));
        }

        [FunctionAttribute("func setbit (index : int, val : bool) : int")]
        public HassiumInt setbit(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            int index = (int)args[0].ToInt(vm, location).Int;
            bool val = args[1].ToBool(vm, location).Bool;
            if (val)
                return new HassiumInt((int)Int | 1 << index);
            else
                return new HassiumInt(Int & ~(1 << index));
        }

        [FunctionAttribute("func __subtract__ (num : number) : number")]
        public override HassiumObject Subtract(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var intArg = args[0] as HassiumInt;
            if (intArg != null)
                return new HassiumInt(Int - args[0].ToInt(vm, location).Int);
            var floatArg = args[0] as HassiumFloat;
            if (floatArg != null)
                return new HassiumFloat(Int - args[0].ToFloat(vm, location).Float);
            vm.RaiseException(HassiumConversionFailedException._new(vm, location, args[0], Number));
            return this;
        }

        [FunctionAttribute("func tobool () : bool")]
        public override HassiumBool ToBool(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Int == 1);
        }

        [FunctionAttribute("func tochar () : char")]
        public override HassiumChar ToChar(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)Int);
        }

        [FunctionAttribute("func tofloat () : float")]
        public override HassiumFloat ToFloat(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Int);
        }

        [FunctionAttribute("func toint () : int")]
        public override HassiumInt ToInt(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Int.ToString());
        }

        [FunctionAttribute("func __xor__ (i : int) : int")]
        public override HassiumObject Xor(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Int ^ args[0].ToInt(vm, location).Int);
        }
    }
}
