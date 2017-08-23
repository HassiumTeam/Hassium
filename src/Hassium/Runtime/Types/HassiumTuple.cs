using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumTuple : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new TupleTypeDef();

        public HassiumObject[] Values { get; private set; }

        public HassiumTuple(params HassiumObject[] val)
        {
            AddType(TypeDefinition);
            Values = val;
        }

        public override HassiumObject Index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return TupleTypeDef.index(vm, this, location, args);
        }

        public override HassiumObject Iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return TupleTypeDef.iter(vm, this, location, args);
        }

        public class TupleTypeDef : HassiumTypeDefinition
        {
            public TupleTypeDef() : base("tuple")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { INDEX, new HassiumFunction(index) },
                    { ITER, new HassiumFunction(iter) }
                };
            }

            [FunctionAttribute("func __index__ (index : int) : object")]
            public static HassiumObject index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumTuple).Values;
                return Values[args[0].ToInt(vm, args[0], location).Int];
            }

            [FunctionAttribute("func __iter__ () : list")]
            public static HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumTuple).Values;
                return new HassiumList(Values);
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
