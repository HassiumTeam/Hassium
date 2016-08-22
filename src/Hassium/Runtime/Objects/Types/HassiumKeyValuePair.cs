using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumKeyValuePair: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("keyValuePair");

        public HassiumObject Key { get; private set; }
        public HassiumObject Value { get; private set; }

        public HassiumKeyValuePair(HassiumObject key, HassiumObject value)
        {
            Key = key;
            Value = value;
            AddType(TypeDefinition);

            AddAttribute("key",     new HassiumProperty(get_key, set_key));
            AddAttribute("value",   new HassiumProperty(get_value, set_value));
        }

        public HassiumObject get_key(VirtualMachine vm, params HassiumObject[] args)
        {
            return Key;
        }
        public HassiumObject set_key(VirtualMachine vm, params HassiumObject[] args)
        {
            return Key = args[0];
        }

        public HassiumObject get_value(VirtualMachine vm, params HassiumObject[] args)
        {
            return Value;
        }
        public HassiumObject set_value(VirtualMachine vm, params HassiumObject[] args)
        {
            return Value = args[0];
        }
    }
}

