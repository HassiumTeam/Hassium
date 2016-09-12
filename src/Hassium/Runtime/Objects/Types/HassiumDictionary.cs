using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumDictionary: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("dictionary");
        public Dictionary<HassiumObject, HassiumObject> Dictionary { get; private set; }

        public HassiumDictionary(List<HassiumKeyValuePair> initial)
        {
            Dictionary = new Dictionary<HassiumObject, HassiumObject>();
            AddType(TypeDefinition);
            foreach (var pair in initial)
                Dictionary.Add(pair.Key, pair.Value);

            AddAttribute("add",             add,            2);
            AddAttribute("containsKey",     containsKey,    1);
            AddAttribute("containsValue",   containsValue,  1);
            AddAttribute("getKeyByValue",   getKeyByValue,  1);
            AddAttribute("getValueByKey",   getValueByKey,  1);
            AddAttribute("remove",          remove, 1);
            AddAttribute(HassiumObject.DISPOSE, Dispose, 0);
        }

        public HassiumObject add(VirtualMachine vm, params HassiumObject[] args)
        {
            Dictionary.Add(args[0], args[1]);
            return args[1];
        }
        public HassiumObject containsKey(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Dictionary.ContainsKey(args[0]));
        }
        public HassiumObject containsValue(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Dictionary.ContainsValue(args[0]));
        }

        public HassiumObject getKeyByValue(VirtualMachine vm, params HassiumObject[] args)
        {
            foreach (var pair in Dictionary)
                if (pair.Value.EqualTo(vm, args[0]).ToBool(vm).Bool)
                    return pair.Key;
            throw new InternalException(vm, InternalException.VALUE_NOT_FOUND_ERROR, args[0].ToString(vm).String);
        }
        public HassiumObject getValueByKey(VirtualMachine vm, params HassiumObject[] args)
        {
            return Index(vm, args);
        }
        public HassiumObject remove(VirtualMachine vm, params HassiumObject[] args)
        {
            Dictionary.Remove(args[0]);
            return args[0];
        }

        public override HassiumObject Dispose(VirtualMachine vm, params HassiumObject[] args)
        {
            Dictionary.Clear();
            return HassiumObject.Null;
        }
        public override HassiumObject Index(VirtualMachine vm, params HassiumObject[] args)
        {
            foreach (var pair in Dictionary)
                if (pair.Key.EqualTo(vm, args[0]).ToBool(vm).Bool)
                    return pair.Value;
            throw new InternalException(vm, InternalException.VALUE_NOT_FOUND_ERROR, args[0].ToString(vm).String);
        }
        public override HassiumObject StoreIndex(VirtualMachine vm, params HassiumObject[] args)
        {
            return Dictionary[args[0]] = args[1];
        }
        public override HassiumObject Iter(VirtualMachine vm, params HassiumObject[] args)
        {
            List<HassiumKeyValuePair> pairs = new List<HassiumKeyValuePair>();
            foreach (var pair in Dictionary)
                pairs.Add(new HassiumKeyValuePair(pair.Key, pair.Value));
            return new HassiumList(pairs.ToArray());
        }
    }
}

