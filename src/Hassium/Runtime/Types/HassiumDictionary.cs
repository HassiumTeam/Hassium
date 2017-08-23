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
                    { ITER, new HassiumFunction(iter, 0)  },
                    { "keybyvalue", new HassiumFunction(keybyvalue, 1)  },
                    { STOREINDEX, new HassiumFunction(storeindex, 2)  },
                    { TOSTRING, new HassiumFunction(tostring, 0)  },
                    { "valuebykey", new HassiumFunction(valuebykey, 1)  },
                };
            }

            [FunctionAttribute("func add (key : object, val : object) : null")]
            public static HassiumNull add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                Dictionary.Add(args[0], args[1]);
                return Null;
            }

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

            [FunctionAttribute("func __iter__ () : list")]
            public static HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Dictionary = (self as HassiumDictionary).Dictionary;
                HassiumList list = new HassiumList(new HassiumObject[0]);
                foreach (var pair in Dictionary)
                    HassiumList.add(vm, list, location, new HassiumTuple(pair.Key, pair.Value));
                return list;
            }

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

        public override HassiumObject GetAttribute(string attrib)
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
