using System;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumKeyValuePair: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("KeyValuePair");

        public HassiumObject Key { get; set; }
        public new HassiumObject Value { get; set; }
        public HassiumKeyValuePair(HassiumObject key, HassiumObject value)
        {
            Key = key;
            Value = value;

            Attributes.Add("key",   new HassiumProperty(get_Key, set_Key));
            Attributes.Add("value", new HassiumProperty(get_Value, set_Value));
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION, new HassiumFunction(toString, 0));
            AddType(HassiumKeyValuePair.TypeDefinition);
        }

        private HassiumObject get_Key(VirtualMachine vm, HassiumObject[] args)
        {
            return Key;
        }
        private HassiumNull set_Key(VirtualMachine vm, HassiumObject[] args)
        {
            Key = args[0];
            return HassiumObject.Null;
        }
        private HassiumObject get_Value(VirtualMachine vm, HassiumObject[] args)
        {
            return Value;
        }
        private HassiumNull set_Value(VirtualMachine vm, HassiumObject[] args)
        {
            Value = args[0];
            return HassiumObject.Null;
        }
        private HassiumObject value(VirtualMachine vm, HassiumObject[] args)
        {
            return Value;
        }
        private HassiumString toString(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Key.ToString(vm) + " : " + Value.ToString(vm));
        }
    }
}

