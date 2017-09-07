using System.Collections.Generic;
using System.Text;

using Hassium.Compiler;

namespace Hassium.Runtime.Types
{
    public class HassiumDictionary : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new DictTypeDef();

        public Dictionary<HassiumObject, HassiumObject> Dictionary { get; private set; }

        public HassiumDictionary(Dictionary<HassiumObject, HassiumObject> initial)
        {
            Dictionary = HassiumMethod.CloneDictionary(initial);
            AddType(TypeDefinition);
        }

        public override HassiumObject Index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return DictTypeDef.index(vm, this, location, args);
        }

        public override HassiumObject Iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return DictTypeDef.iter(vm, this, location, args);
        }

        public override HassiumObject StoreIndex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return DictTypeDef.storeindex(vm, this, location, args);
        }

        [DocStr(
            "@desc A class representing a dictionary where the keys and values are objects.",
            "@returns dict."
            )]
        public class DictTypeDef : HassiumTypeDefinition
        {
            public DictTypeDef() : base("dict")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "add", new HassiumFunction(add, 2)  },
                    { "containskey", new HassiumFunction(containskey, 1)  },
                    { "containsvalue", new HassiumFunction(containsvalue, 1)  },
                    { INDEX, new HassiumFunction(index, 1)  },
                    { INVOKE, new HassiumFunction(_new, 1) },
                    { ITER, new HassiumFunction(iter, 0)  },
                    { "keybyvalue", new HassiumFunction(keybyvalue, 1)  },
                    { STOREINDEX, new HassiumFunction(storeindex, 2)  },
                    { TOLIST, new HassiumFunction(tolist, 0)  },
                    { TOSTRING, new HassiumFunction(tostring, 0)  },
                    { "valuebykey", new HassiumFunction(valuebykey, 1)  },
                };
            }

            [DocStr(
                "@desc Constructs a new dict using the given list or given list of tuples.",
                "@param l The list to use.",
                "@returns The new dict object."
                )]
            [FunctionAttribute("func new (l : list) : dict")]
            public static HassiumDictionary _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                if (args[0] is HassiumDictionary)
                    return args[0] as HassiumDictionary;
                var dict = new Dictionary<HassiumObject, HassiumObject>();

                HassiumObject key = null;
                foreach (var e in args[0].ToList(vm, args[0], location).Values)
                {
                    if (e is HassiumTuple)
                    {
                        var tup = (e as HassiumTuple);
                        dict.Add(tup.Values[0], tup.Values[1]);
                    }
                    else if (key == null)
                        key = e;
                    else
                    {
                        dict.Add(key, e);
                        key = null;
                    }
                }

                return new HassiumDictionary(dict);
            }

            [DocStr(
                "@desc Adds the given value to the dictionary under the specified key.",
                "@param key The key for the entry.",
                "@param value The value for the key.",
                "@returns null."
                )]
            [FunctionAttribute("func add (key : object, val : object) : null")]
            public static HassiumNull add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                Dictionary.Add(args[0], args[1]);
                return Null;
            }

            [DocStr(
                "@desc Returns a boolean indicating if the specified key is present in the dictionary.",
                "@param key The key to check.",
                "@returns true if the key is present, otherwise false."
                )]
            [FunctionAttribute("func containskey (key : object) : bool")]
            public static HassiumBool containskey(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                if (Dictionary.ContainsKey(args[0]))
                    return True;
                foreach (var key in Dictionary.Keys)
                    if (key.EqualTo(vm, key, location, args[0]).Bool)
                        return True;
                return False;
            }

            [DocStr(
                "@desc Returns a boolean indicating if the specified value is present in the dictionary.",
                "@param val The value to check.",
                "@returns true if the value is present, otherwise false."
                )]
            [FunctionAttribute("func containsvalue (val : object) : bool")]
            public static HassiumBool containsvalue(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                if (Dictionary.ContainsValue(args[0]))
                    return True;
                foreach (var val in Dictionary.Values)
                    if (val.EqualTo(vm, val, location, args[0]).Bool)
                        return True;
                return False;
            }

            [DocStr(
                "@desc Implements the [] operator by retrieving the value at the specified key.",
                "@param key The key for the value to get.",
                "@returns The value at key."
                )]
            [FunctionAttribute("func __index__ (key : object) : object")]
            public static HassiumObject index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                if (Dictionary.ContainsKey(args[0]))
                    return Dictionary[args[0]];
                foreach (var key in Dictionary.Keys)
                    if (key.EqualTo(vm, key, location, args[0]).Bool)
                        return Dictionary[key];
                vm.RaiseException(HassiumAttribNotFoundException.AttribNotFoundExceptionTypeDef._new(vm, null, location, self, args[0].ToString(vm, args[0], location)));
                return Null;
            }

            [DocStr(
                "@desc Implements the foreach loop, returning a list of tuples in the format (key, value) ...",
                "@returns A list of (key, value) tuples."
                )]
            [FunctionAttribute("func __iter__ () : list")]
            public static HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                HassiumList list = new HassiumList(new HassiumObject[0]);
                foreach (var pair in Dictionary)
                    HassiumList.add(vm, list, location, new HassiumTuple(pair.Key, pair.Value));
                return list;
            }

            [DocStr(
                "@desc Gets the first key that owns the specified value.",
                "@param val The value to get the key by.",
                "@returns The key that owns value."
                )]
            [FunctionAttribute("func keybyvalue (val : object) : object")]
            public static HassiumObject keybyvalue(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                foreach (var pair in Dictionary)
                    if (pair.Value.EqualTo(vm, pair.Value, location, args[0]).ToBool(vm, self, location).Bool)
                        return pair.Key;
                vm.RaiseException(HassiumKeyNotFoundException.KeyNotFoundExceptionTypeDef._new(vm, self, location, self, args[0]));
                return Null;
            }

            [DocStr(
                "@desc Implements the []= operator, storing the specified object at the specified key.",
                "@param key The key to store.",
                "2param val The value to store under key.",
                "@returns The value provided."
                )]
            [FunctionAttribute("func __storeindex__ (key : object, val : object) : object")]
            public static HassiumObject storeindex(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                if (containskey(vm, self, location, args[0]).Bool)
                    Dictionary[args[0]] = args[1];
                else
                    Dictionary.Add(args[0], args[1]);
                return args[1];
            }

            [DocStr(
                "@desc Converts this dictionary to a list of tuples and returns it.",
                "@returns A new list of tuples in (key, value) format."
                )]
            [FunctionAttribute("func tolist () : list")]
            public static HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return iter(vm, self, location) as HassiumList;
            }

            [DocStr(
                "@desc Returns the string value of the dictionary in format { <key> : <value>, ... }",
                "@returns The string value of this dictionary."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                StringBuilder sb = new StringBuilder();

                sb.Append("{ ");
                foreach (var pair in Dictionary)
                    sb.AppendFormat("{0} : {1}, ", pair.Key.ToString(vm, pair.Key, location).String, pair.Value.ToString(vm, pair.Value, location).String);
                if (Dictionary.Count > 0)
                    sb.Append("\b\b ");
                sb.Append("}");

                return new HassiumString(sb.ToString());
            }

            [DocStr(
                "@desc Gets the value for the specified key.",
                "@param key The key for the value to get.",
                "@returns The value at key."
                )]
            [FunctionAttribute("func valuebykey (key : object) : object")]
            public static HassiumObject valuebykey(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return index(vm, self, location, args[0]);
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
