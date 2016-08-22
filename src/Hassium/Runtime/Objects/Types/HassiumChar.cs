using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumChar: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("char");

        public char Char { get; private set; }

        public HassiumChar(char c)
        {
            Char = c;
            AddType(TypeDefinition);

            AddAttribute("isControl",           isControl,          0);
            AddAttribute("isDigit",             isDigit,            0);
            AddAttribute("isLetter",            isLetter,           0);
            AddAttribute("isLetterOrDigit",     isLetterOrDigit,    0);
            AddAttribute("isLower",             isLower,            0);
            AddAttribute("isSymbol",            isSymbol,           0);
            AddAttribute("isUpper",             isUpper,            0);
            AddAttribute("isWhiteSpace",        isWhiteSpace,       0);
            AddAttribute("toLower",             toLower,            0);
            AddAttribute("toUpper",             toUpper,            0);
            AddAttribute(HassiumObject.TOCHAR,  ToChar,             0);
            AddAttribute(HassiumObject.TOFLOAT, ToFloat,            0);
            AddAttribute(HassiumObject.TOINT,   ToInt,              0);
            AddAttribute(HassiumObject.TOSTRING,ToString,           0);
        }

        public HassiumBool isControl(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsControl(Char));
        }
        public HassiumBool isDigit(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsDigit(Char));
        }
        public HassiumBool isLetter(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsLetter(Char));
        }
        public HassiumBool isLetterOrDigit(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsLetterOrDigit(Char));
        }
        public HassiumBool isLower(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char >= 97 && (int)Char <= 122);
        }
        public HassiumBool isSymbol(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsSymbol(Char));
        }
        public HassiumBool isUpper(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char >= 65 && (int)Char <= 90);
        }
        public HassiumBool isWhiteSpace(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(char.IsWhiteSpace(Char));
        }
        public HassiumChar toLower(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar(Char.ToLower(Char));
        }
        public HassiumChar toUpper(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar(Char.ToUpper(Char));
        }

        public override HassiumObject Add(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar((char)(Char + (char)args[0].ToInt(vm).Int));
        }
        public override HassiumObject BitshiftLeft(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Char << (int)args[0].ToInt(vm).Int);
        }
        public override HassiumObject BitshiftRight(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Char >> (int)args[0].ToInt(vm).Int);
        }
        public override HassiumObject BitwiseAnd(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Char & args[0].ToInt(vm).Int);
        }
        public override HassiumObject BitwiseNot(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(~Char);
        }
        public override HassiumObject BitwiseOr(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Char | args[0].ToInt(vm).Int);
        }
        public override HassiumObject Divide(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Char / args[0].ToInt(vm).Int);
        }
        public override HassiumObject EqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Char == args[0].ToChar(vm).Char);
        }
        public override HassiumObject GreaterThan(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char > args[0].ToInt(vm).Int);
        }
        public override HassiumObject GreaterThanOrEqual(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char >= args[0].ToInt(vm).Int);
        }
        public override HassiumObject IntegerDivision(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Char / args[0].ToInt(vm).Int);
        }
        public override HassiumObject LesserThan(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char < args[0].ToInt(vm).Int);
        }
        public override HassiumObject LesserThanOrEqual(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool((int)Char <= args[0].ToInt(vm).Int);
        }
        public override HassiumObject Modulus(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Char % args[0].ToInt(vm).Int);
        }
        public override HassiumObject Multiply(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Char * args[0].ToInt(vm).Int);
        }
        public override HassiumObject NotEqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            return EqualTo(vm, args).LogicalNot(vm, args);
        }
        public override HassiumObject Subtract(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar((char)(Char - (char)args[0].ToInt(vm).Int));
        }
        public override HassiumObject Power(VirtualMachine vm, params HassiumObject[] args)
        {
            return base.Power(vm, args);
        }
        public override HassiumChar ToChar(VirtualMachine vm, params HassiumObject[] args)
        {
            return this;
        }
        public override HassiumFloat ToFloat(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat((double)Char);
        }
        public override HassiumInt ToInt(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt((long)Char);
        }
        public override HassiumString ToString(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Char.ToString());
        }
    }
}

