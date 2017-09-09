using Hassium.Compiler;

using System.Collections.Generic;
using System.Text;

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

        [DocStr(
            "@desc A class representing a fixed length non-mutable list of objects.",
            "@returns tuple."
            )]
        public class TupleTypeDef : HassiumTypeDefinition
        {
            public TupleTypeDef() : base("tuple")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { INDEX, new HassiumFunction(index) },
                    { ITER, new HassiumFunction(iter) },
                    { "length", new HassiumProperty(get_length) },
                    { TOSTRING, new HassiumFunction(tostring, 0) }
                };
            }

            [DocStr(
                "@desc Implements the [] operator to return the value at the 0-based index.",
                "@oaram index The 0-based index to get.",
                "@returns The object at the index."
                )]
            [FunctionAttribute("func __index__ (index : int) : object")]
            public static HassiumObject index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumTuple).Values;
                return Values[args[0].ToInt(vm, args[0], location).Int];
            }

            [DocStr(
                "@desc Implements the foreach loop by returning a new list of the values in the tuple.",
                "@returns A new list containing the values inside the tuple."
                )]
            [FunctionAttribute("func __iter__ () : list")]
            public static HassiumObject iter(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumTuple).Values;
                return new HassiumList(Values);
            }

            [DocStr(
                "@desc Gets the readonly int that represents the amount of elements in this tuple.",
                "@returns The number of values in this tuple as int."
                )]
            [FunctionAttribute("length { get; }")]
            public static HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt((self as HassiumTuple).Values.Length);
            }

            [DocStr(
                "@desc Returns this tuple as a string formatted as ( val1, val2, ... )",
                "@returns The string value of this list."
            )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Values = (self as HassiumTuple).Values;
                StringBuilder sb = new StringBuilder();

                sb.Append("( ");
                foreach (var v in Values)
                    sb.AppendFormat("{0}, ", v.ToString(vm, v, location).String);
                if (Values.Length > 0)
                    sb.Remove(sb.Length - 2, 2);
                sb.Append(" )");

                return new HassiumString(sb.ToString());
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
