using Hassium.Compiler;

namespace Hassium.Runtime.Types
{
    public class HassiumBool : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("bool");

        public bool Bool { get; private set; }

        public HassiumBool(bool val)
        {
            AddType(TypeDefinition);
            Bool = val;

            AddAttribute(EQUALTO, EqualTo, 1);
            AddAttribute(LOGICALAND, LogicalAnd, 1);
            AddAttribute(LOGICALNOT, LogicalNot, 0);
            AddAttribute(LOGICALOR, LogicalOr, 1);
            AddAttribute(NOTEQUALTO, NotEqualTo, 1);
            AddAttribute(TOBOOL, ToBool, 0);
            AddAttribute(TOINT, ToInt, 0);
            AddAttribute(TOSTRING, ToString, 0);
        }

        [FunctionAttribute("func __equals__ (b : bool) : bool")]
        public override HassiumBool EqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool == args[0].ToBool(vm, location).Bool);
        }

        [FunctionAttribute("func __logicaland__ (b : bool) : bool")]
        public override HassiumObject LogicalAnd(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool && args[0].ToBool(vm, location).Bool);
        }

        [FunctionAttribute("func __logicalnot__ (b : bool) : bool")]
        public override HassiumObject LogicalNot(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(!Bool);
        }

        [FunctionAttribute("func __logicalor__ (b : bool) : bool")]
        public override HassiumObject LogicalOr(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool || args[0].ToBool(vm, location).Bool);
        }

        [FunctionAttribute("func __notequal__ (b : bool) : bool")]
        public override HassiumBool NotEqualTo(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool != args[0].ToBool(vm, location).Bool);
        }

        [FunctionAttribute("func tobool () : bool")]
        public override HassiumBool ToBool(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func toint () : int")]
        public override HassiumInt ToInt(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Bool ? 1 : 0);
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Bool.ToString().ToLower());
        }
    }
}
