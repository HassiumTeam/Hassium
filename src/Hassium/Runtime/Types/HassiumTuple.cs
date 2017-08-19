using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumTuple : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("tuple");

        public Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { INDEX, new HassiumFunction(index, 1) },
            { ITER, new HassiumFunction(iter, 0) }
        };

        public HassiumObject[] Values { get; private set; }

        public HassiumTuple(params HassiumObject[] val)
        {
            AddType(TypeDefinition);
            Values = val;
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

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || Attribs.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (Attribs[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in Attribs)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
