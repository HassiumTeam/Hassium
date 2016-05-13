using System;
using System.Collections.Generic;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumObject : ICloneable
    {
        public static HassiumNull Null = new HassiumNull();
        public const string ADD_FUNCTION =              "__add__";
        public const string SUB_FUNCTION =              "__sub__";
        public const string MUL_FUNCTION =              "__mul__";
        public const string DIV_FUNCTION =              "__div__";
        public const string MOD_FUNCTION =              "__mod__";
        public const string XOR_FUNCTION =              "__xor__";
        public const string OR_FUNCTION =               "__or__";
        public const string XAND_FUNCTION =             "__xand__";
        public const string EQUALS_FUNCTION =           "__equals__";
        public const string NOT_EQUAL_FUNCTION =        "__notequal__";
        public const string GREATER_FUNCTION =          "__greater__";
        public const string LESSER_FUNCTION =           "__lesser__";
        public const string GREATER_OR_EQUAL_FUNCTION = "__greaterorequal__";
        public const string LESSER_OR_EQUAL_FUNCTION =  "__lesserorequal__";
        public const string INVOKE_FUNCTION =           "__invoke__";
        public const string INDEX_FUNCTION =            "__index__";
        public const string STORE_INDEX_FUNCTION =      "__storeindex__";
        public const string ENUMERABLE_FULL =           "__enumerablefull__";
        public const string ENUMERABLE_NEXT =           "__enumerablenext__";
        public const string ENUMERABLE_RESET =          "__enumerableReset__";
        public const string NOT =                       "__not__";
        public const string BITWISE_COMPLEMENT =        "__bcompl__";
        public const string NEGATE =                    "__negate__";
        public const string BIT_SHIFT_LEFT =            "__bshiftleft__";
        public const string BIT_SHIFT_RIGHT =           "__bshiftright__";
        public const string CONTAINS =                  "__contains__";
        public const string TOSTRING_FUNCTION =         "toString";

        public Dictionary<string, HassiumObject> Attributes = new Dictionary<string, HassiumObject>();
        public List<string> Types = new List<string>()
        {
            "object"
        };

        public object Value { get; set; }

        public virtual HassiumObject Add(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(ADD_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support adding!");
            return Attributes[ADD_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject Sub(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(SUB_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support subtracting!");
            return Attributes[SUB_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject Mul(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(MUL_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support multiplying!");
            return Attributes[MUL_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject Div(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(DIV_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support dividing!");
            return Attributes[DIV_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject Mod(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(MOD_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support modulus!");
            return Attributes[MOD_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject XOR(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(XOR_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support xor!");
            return Attributes[XOR_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject OR(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(OR_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support logical or!");
            return Attributes[OR_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject Xand(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(XAND_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support and!");
            return Attributes[XAND_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumBool Equals(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(EQUALS_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support equals!");
            return (HassiumBool)Attributes[EQUALS_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumBool NotEquals(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(NOT_EQUAL_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support not equal!");
            return (HassiumBool)Attributes[NOT_EQUAL_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumBool GreaterThan(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(GREATER_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support greater than!");
            return (HassiumBool)Attributes[GREATER_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumBool GreaterThanOrEqual(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(GREATER_OR_EQUAL_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support greater than or equal!");
            return (HassiumBool)Attributes[GREATER_OR_EQUAL_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumBool LesserThan(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(LESSER_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support lesser than!");
            return (HassiumBool)Attributes[LESSER_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumBool LesserThanOrEqual(VirtualMachine vm, HassiumObject obj)
        {
            if (!Attributes.ContainsKey(LESSER_OR_EQUAL_FUNCTION))
                throw new InternalException("Object " + Type() + " does not support lesser than or equal!");
            return (HassiumBool)Attributes[LESSER_OR_EQUAL_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject Invoke(VirtualMachine vm, HassiumObject[] args)
        {
            if (Attributes.ContainsKey(INVOKE_FUNCTION))
                return Attributes[INVOKE_FUNCTION].Invoke(vm, args);
            throw new Exception("Object does not support invoking!");
        }
        public virtual HassiumObject Index(VirtualMachine vm, HassiumObject obj)
        {
            return Attributes[INDEX_FUNCTION].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject StoreIndex(VirtualMachine vm, HassiumObject index, HassiumObject value)
        {
            return Attributes[STORE_INDEX_FUNCTION].Invoke(vm, new HassiumObject[] { index, value });
        }
        public virtual HassiumObject EnumerableFull(VirtualMachine vm)
        {
            return Attributes[ENUMERABLE_FULL].Invoke(vm, new HassiumObject[0]);
        }
        public virtual HassiumObject EnumerableNext(VirtualMachine vm)
        {
            return Attributes[ENUMERABLE_NEXT].Invoke(vm, new HassiumObject[0]);
        }
        public virtual HassiumObject EnumerableReset(VirtualMachine vm)
        {
            return Attributes[ENUMERABLE_RESET].Invoke(vm, new HassiumObject[0]);
        }
        public virtual HassiumObject Not(VirtualMachine vm)
        {
            return Attributes[NOT].Invoke(vm, new HassiumObject[0]);
        }
        public virtual HassiumObject BitwiseComplement(VirtualMachine vm)
        {
            return Attributes[BITWISE_COMPLEMENT].Invoke(vm, new HassiumObject[0]);
        }
        public virtual HassiumObject Negate(VirtualMachine vm)
        {
            return Attributes[NEGATE].Invoke(vm, new HassiumObject[0]);
        }
        public virtual HassiumObject BitShiftLeft(VirtualMachine vm, HassiumObject obj)
        {
            return Attributes[BIT_SHIFT_LEFT].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumObject BitShiftRight(VirtualMachine vm, HassiumObject obj)
        {
            return Attributes[BIT_SHIFT_RIGHT].Invoke(vm, new HassiumObject[] { obj });
        }
        public virtual HassiumBool Contains(VirtualMachine vm, HassiumObject obj)
        {
            return HassiumBool.Create(Attributes[CONTAINS].Invoke(vm, new[] {obj}));
        }
        public void AddType(string type)
        {
            Types.Add(type);
        }
        public string Type()
        {
            return Types[Types.Count - 1];
        }
        public string ToString(VirtualMachine vm)
        {
            if (Attributes.ContainsKey("toString"))
                return ((HassiumString)Attributes["toString"].Invoke(vm, new HassiumObject[0])).Value;
            return Type();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}