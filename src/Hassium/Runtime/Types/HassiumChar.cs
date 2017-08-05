using Hassium.Compiler;

using System;

namespace Hassium.Runtime.Types
{
    public class HassiumChar : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("char");

        public char Char { get; private set; }

        public HassiumChar(char val)
        {
            AddType(Number);
            AddType(TypeDefinition);
            Char = val;

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
            AddAttribute("iscontrol", iscontrol, 0);
            AddAttribute("isdigit", isdigit, 0);
            AddAttribute("isletter", isletter, 0);
            AddAttribute("isletterordigit", isletterordigit, 0);
            AddAttribute("islower", islower, 0);
            AddAttribute("issymbol", issymbol, 0);
            AddAttribute("isupper", isupper, 0);
            AddAttribute("iswhitespace", iswhitespace, 0);
            AddAttribute(LESSERTHAN, LesserThan, 1);
            AddAttribute(LESSERTHANOREQUAL, LesserThanOrEqual, 1);
            AddAttribute(MODULUS, Modulus, 1);
            AddAttribute(MULTIPLY, Multiply, 1);
            AddAttribute(NOTEQUALTO, NotEqualTo, 1);
            AddAttribute("setbit", setbit, 2);
            AddAttribute(SUBTRACT, Subtract, 1);
            AddAttribute(TOCHAR, ToChar, 0);
            AddAttribute(TOFLOAT, ToFloat, 0);
            AddAttribute(TOINT, ToInt, 0);
            AddAttribute("tolower", tolower, 0);
            AddAttribute(TOSTRING, ToString, 0);
            AddAttribute("toupper", toupper, 0);
            AddAttribute(XOR, Xor, 1);
        }

        [FunctionAttribute("func __add__ (c : char) : char")]
        public override HassiumObject Add(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)(Char + (char)args[0].ToInt(vm, location).Int));
        }

        [FunctionAttribute("func __bitshiftleft__ (c : char) : int")]
        public override HassiumObject BitshiftLeft(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char << (int)args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __bitshiftright__ (c : char) : int")]
        public override HassiumObject BitshiftRight(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char >> (int)args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __bitswiseand__ (c : char) : int")]
        public override HassiumObject BitwiseAnd(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char & args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __bitwisenot__ (c : char) : int")]
        public override HassiumObject BitwiseNot(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(~Char);
        }

        [FunctionAttribute("func __bitwiseor__ (c : char) : int")]
        public override HassiumObject BitwiseOr(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char | args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __divide__ (c : char) : int")]
        public override HassiumObject Divide(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char / args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __equals__ (c : char) : bool")]
        public override HassiumBool EqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Char == args[0].ToChar(vm, location).Char);
        }

        [FunctionAttribute("func getbit (index : int) : bool")]
        public HassiumBool getbit(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(((byte)Char & (1 << (int)args[0].ToInt(vm, location).Int - 1)) != 0);
        }

        [FunctionAttribute("func __greater__ (i : int) : bool")]
        public override HassiumObject GreaterThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char > args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __greaterorequal__ (i : int) : bool")]
        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char >= args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __intdivision__ (i : int) : int")]
        public override HassiumObject IntegerDivision(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char / args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func iscontrol () : bool")]
        public HassiumBool iscontrol(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsControl(Char));
        }

        [FunctionAttribute("func isdigit () : bool")]
        public HassiumBool isdigit(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsDigit(Char));
        }

        [FunctionAttribute("func isletter () : bool")]
        public HassiumBool isletter(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsLetter(Char));
        }

        [FunctionAttribute("func isletterordigit () : bool")]
        public HassiumBool isletterordigit(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsLetterOrDigit(Char));
        }

        [FunctionAttribute("func islower () : bool")]
        public HassiumBool islower(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char >= 97 && (int)Char <= 122);
        }

        [FunctionAttribute("func issymbol () : bool")]
        public HassiumBool issymbol(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsSymbol(Char));
        }

        [FunctionAttribute("func isupper () : bool")]
        public HassiumBool isupper(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char >= 65 && (int)Char <= 90);
        }

        [FunctionAttribute("func iswhitespace () : bool")]
        public HassiumBool iswhitespace(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsWhiteSpace(Char));
        }

        [FunctionAttribute("func __lesser__ (i : int) : bool")]
        public override HassiumObject LesserThan(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char < args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __lesserorequal__ (i : int) : bool")]
        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char <= args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __modulus__ (i : int) : int")]
        public override HassiumObject Modulus(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char % args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __multiply__ (i : int) : int")]
        public override HassiumObject Multiply(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Char * args[0].ToInt(vm, location).Int);
        }

        [FunctionAttribute("func __notequal__ (c : char) : bool")]
        public override HassiumBool NotEqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Char != args[0].ToChar(vm, location).Char);
        }

        [FunctionAttribute("func __power__ () : float")]
        public override HassiumObject Power(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return base.Power(vm, location, args);
        }

        [FunctionAttribute("func setbit (index : int, val : bool) : char")]
        public HassiumChar setbit(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            int index = (int)args[0].ToInt(vm, location).Int;
            bool val = args[1].ToBool(vm, location).Bool;
            if (val)
                return new HassiumChar((char)(Char | 1 << index));
            else
                return new HassiumChar((char)(Char & ~(1 << index)));
        }

        [FunctionAttribute("func __subtract__ (i : int) : char")]
        public override HassiumObject Subtract(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)(Char - (char)args[0].ToInt(vm, location).Int));
        }

        [FunctionAttribute("func tochar () : char")]
        public override HassiumChar ToChar(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func tofloat () : float")]
        public override HassiumFloat ToFloat(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat((double)Char);
        }

        [FunctionAttribute("func toint () : int")]
        public override HassiumInt ToInt(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt((long)Char);
        }

        [FunctionAttribute("func tolower () : char")]
        public HassiumChar tolower(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar(Char.ToLower(Char));
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Char.ToString());
        }

        [FunctionAttribute("func toupper () : char")]
        public HassiumChar toupper(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar(Char.ToUpper(Char));
        }

        [FunctionAttribute("func __xor__ (c : char) : char")]
        public override HassiumObject Xor(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumChar((char)((byte)Char ^ (byte)args[0].ToChar(vm, location).Char));
        }
    }
}
