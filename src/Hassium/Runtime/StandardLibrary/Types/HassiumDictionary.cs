using System;
using System.Collections.Generic;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumDictionary: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("dictionary");

        public new Dictionary<HassiumObject, HassiumObject> Value = new Dictionary<HassiumObject, HassiumObject>();
        public HassiumDictionary(HassiumKeyValuePair[] pairs)
        {
            foreach (HassiumKeyValuePair pair in pairs)
                Value.Add(pair.Key, pair.Value);

            Attributes.Add("containsKey",   new HassiumFunction(containsKey, 1));
            Attributes.Add("containsValue", new HassiumFunction(containsValue, 1));
            Attributes.Add("length",        new HassiumProperty(get_Length));
            Attributes.Add(HassiumObject.ADD_FUNCTION, new HassiumFunction(__add__, 1));
            Attributes.Add(HassiumObject.INDEX_FUNCTION, new HassiumFunction(__index__, 1));
            Attributes.Add(HassiumObject.STORE_INDEX_FUNCTION, new HassiumFunction(__storeindex__, 2));
            AddType(HassiumDictionary.TypeDefinition);
        }

        private HassiumBool containsKey(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (var pair in Value)
                if (pair.Key.Equals(vm, args[0]).Value)
                    return new HassiumBool(true);
            return new HassiumBool(false);
        }
        private HassiumBool containsValue(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (var pair in Value)
                if (pair.Value.Equals(vm, args[0]).Value)
                    return new HassiumBool(true);
            return new HassiumBool(false);
        }
        private HassiumInt get_Length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.Count);
        }

        private HassiumDictionary __add__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumDictionary dict = this.Clone() as HassiumDictionary;
            HassiumKeyValuePair pair = HassiumKeyValuePair.Create(args[0]);
            dict.Value.Add(pair.Key, pair.Value);

            return dict;
        }
        private HassiumList __iter__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            foreach (var pair in Value)
                list.Value.Add(new HassiumKeyValuePair(pair.Key, pair.Value));

            return list;
        }
        private HassiumObject __index__ (VirtualMachine vm, HassiumObject[] args)
        {
            foreach (var pair in Value)
                if (pair.Key.Equals(vm, args[0]).Value)
                    return pair.Value;
            throw new InternalException("Key not found!");
        }
        private HassiumObject __storeindex__ (VirtualMachine vm, HassiumObject[] args)
        {
            var keys = Value.Keys;
            foreach (HassiumObject key in keys)
            {
                if (key.Equals(vm, args[0]).Value)
                {
                    Value[key] = args[1];
                    return args[1];
                }
            }
            Value.Add(args[0], args[1]);
            return args[1];
        }
    }
}

