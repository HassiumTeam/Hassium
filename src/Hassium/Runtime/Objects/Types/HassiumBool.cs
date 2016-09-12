using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumBool: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("bool");
        public bool Bool { get; private set; }

        public HassiumBool(bool val)
        {
            AddType(TypeDefinition);
            Bool = val;

            AddAttribute(HassiumObject.TOBOOL, ToBool, 0);
            AddAttribute(HassiumObject.TOINT, ToInt, 0);
            AddAttribute(HassiumObject.TOSTRING, ToString, 0);
        }

        public override HassiumObject EqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Bool == args[0].ToBool(vm, args).Bool);
        }
        public override HassiumObject LogicalAnd(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Bool && args[0].ToBool(vm).Bool);
        }
        public override HassiumObject LogicalOr(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Bool || args[0].ToBool(vm).Bool);
        }
        public override HassiumObject LogicalNot(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(!Bool);
        }
        public override HassiumObject NotEqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            return EqualTo(vm, args).LogicalNot(vm, args);
        }
        public override HassiumBool ToBool(VirtualMachine vm, params HassiumObject[] args)
        {
            return this;
        }
        public override HassiumInt ToInt(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Bool ? 1 : 0);
        }
        public override HassiumString ToString(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Bool.ToString());
        }
    }
}