using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.Text
{
    public class HassiumStringBuilder : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new StringBuilderTypeDef();

        public StringBuilder StringBuilder { get; private set; }

        public HassiumStringBuilder()
        {
            AddType(TypeDefinition);
        }

        public class StringBuilderTypeDef : HassiumTypeDefinition
        {
            public StringBuilderTypeDef() : base("StringBuilder")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { INVOKE, new HassiumFunction(_new, 0, 1)  },
                    { "append", new HassiumFunction(append, 1)  },
                    { "appendf", new HassiumFunction(appendf, -1)  },
                    { "appendline", new HassiumFunction(appendline, 1)  },
                    { "clear", new HassiumFunction(clear, 0)  },
                    { "insert", new HassiumFunction(insert, 2)  },
                    { "length", new HassiumProperty(get_length)  },
                    { "replace", new HassiumFunction(replace, 2)  },
                    { TOSTRING, new HassiumFunction(tostring, 0)  },
                };
            }

            [FunctionAttribute("func new () : StringBuilder", "func new (obj : object) : StringBuilder")]
            public static HassiumStringBuilder _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumStringBuilder sb = new HassiumStringBuilder();

                sb.StringBuilder = args.Length == 0 ? new StringBuilder() : new StringBuilder(args[0].ToString(vm, args[0], location).String);

                return sb;
            }

            [FunctionAttribute("func append (obj : object) : StringBuilder")]
            public static HassiumStringBuilder append(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StringBuilder = (self as HassiumStringBuilder).StringBuilder;
                StringBuilder.Append(args[0].ToString(vm, args[0], location).String);

                return self as HassiumStringBuilder;
            }

            [FunctionAttribute("func appendf (fmt : string, params obj) : StringBuilder")]
            public static HassiumStringBuilder appendf(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var val = GlobalFunctions.format(vm, self, location, args);

                append(vm, self, location, val);

                return self as HassiumStringBuilder;
            }

            [FunctionAttribute("func appendline (obj : object) : StringBuilder")]
            public static HassiumStringBuilder appendline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StringBuilder = (self as HassiumStringBuilder).StringBuilder;
                StringBuilder.AppendLine(args[0].ToString(vm, args[0], location).String);

                return self as HassiumStringBuilder;
            }

            [FunctionAttribute("func clear () : StringBuilder")]
            public static HassiumStringBuilder clear(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StringBuilder = (self as HassiumStringBuilder).StringBuilder;
                StringBuilder.Clear();

                return self as HassiumStringBuilder;
            }

            [FunctionAttribute("func insert (index : int, obj : object) : StringBuilder")]
            public static HassiumStringBuilder insert(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StringBuilder = (self as HassiumStringBuilder).StringBuilder;
                StringBuilder.Insert((int)args[0].ToInt(vm, args[0], location).Int, args[1].ToString(vm, args[1], location).String);

                return self as HassiumStringBuilder;
            }

            [FunctionAttribute("length { get; }")]
            public static HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StringBuilder = (self as HassiumStringBuilder).StringBuilder;
                return new HassiumInt(StringBuilder.Length);
            }

            [FunctionAttribute("func replace (obj1 : object, obj2 : object) : StringBuilder")]
            public static HassiumStringBuilder replace(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StringBuilder = (self as HassiumStringBuilder).StringBuilder;
                StringBuilder.Replace(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

                return self as HassiumStringBuilder;
            }

            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString((self as HassiumStringBuilder).StringBuilder.ToString());
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
