using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumBool : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new BoolTypeDef();

        public bool Bool { get; private set; }

        public HassiumBool(bool val)
        {
            AddType(TypeDefinition);
            Bool = val;
        }

        public override HassiumBool EqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool == args[0].ToBool(vm, args[0], location).Bool);
        }

        public override HassiumObject Invoke(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            if (args[0] is HassiumBool)
                return args[0];
            return new HassiumBool(System.Convert.ToBoolean(args[0].ToString(vm, args[0], location).String));
        }

        public override HassiumObject LogicalAnd(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool && args[0].ToBool(vm, args[0], location).Bool);
        }

        public override HassiumObject LogicalNot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(!Bool);
        }

        public override HassiumObject LogicalOr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool || args[0].ToBool(vm, args[0], location).Bool);
        }

        public override HassiumBool NotEqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool != args[0].ToBool(vm, args[0], location).Bool);
        }

        public override HassiumBool ToBool(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        public override HassiumInt ToInt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Bool ? 1 : 0);
        }

        public override HassiumString ToString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Bool.ToString().ToLower());
        }

        [DocStr(
            "@desc A class representing a boolean that is either true or false.",
            "@returns bool."
            )]
        public class BoolTypeDef : HassiumTypeDefinition
        {
            public BoolTypeDef() : base("bool")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { EQUALTO, new HassiumFunction(equalto, 1)  },
                    { INVOKE, new HassiumFunction(_new, 1)  },
                    { LOGICALAND, new HassiumFunction(logicaland, 1)  },
                    { LOGICALNOT, new HassiumFunction(logicalnot, 0)  },
                    { LOGICALOR, new HassiumFunction(logicalor, 1)  },
                    { NOTEQUALTO, new HassiumFunction(notequalto, 1)  },
                    { TOBOOL, new HassiumFunction(tobool, 0)  },
                    { TOINT, new HassiumFunction(toint, 0)  },
                    { TOSTRING, new HassiumFunction(tostring, 0)  }
                };
            }

            [DocStr(
                "@desc Constructs a new bool object using the specified value.",
                "@param val The value of the bool.",
                "@returns The new bool object."
            )]
            [FunctionAttribute("func new (val : object) : bool")]
            public static HassiumBool _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                if (args[0] is HassiumBool)
                    return args[0] as HassiumBool;
                return new HassiumBool(System.Convert.ToBoolean(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Implements the == operator to determine if this bool is equal to the specified bool.",
                "@param b The bool to compare.",
                "@returns true if the bools are equal, otherwise false."
                )]
            [FunctionAttribute("func __equals__ (b : bool) : bool")]
            public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bool = (self as HassiumBool).Bool;
                return new HassiumBool(Bool == args[0].ToBool(vm, args[0], location).Bool);
            }

            [DocStr(
                "@desc Implements the && operator to determine if both this bool and the specified bool are true.",
                "@param b The second bool to check.",
                "@returns true if both bools are true, otherwise false."
                )]
            [FunctionAttribute("func __logicaland__ (b : bool) : bool")]
            public static HassiumObject logicaland(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bool = (self as HassiumBool).Bool;
                return new HassiumBool(Bool && args[0].ToBool(vm, args[0], location).Bool);
            }

            [DocStr(
                "@desc Implements the ! operator to return the boolean opposite of this bool.",
                "@returns true if this bool is false, otherwise false."
                )]
            [FunctionAttribute("func __logicalnot__ () : bool")]
            public static HassiumObject logicalnot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bool = (self as HassiumBool).Bool;
                return new HassiumBool(!Bool);
            }

            [DocStr(
                "@desc Implements the || operator to determine if either this bool or the specified bool are true.",
                "@param b The second bool to check.",
                "@returns true if either this bool or the other is true, otherwise false."
                )]
            [FunctionAttribute("func __logicalor__ (b : bool) : bool")]
            public static HassiumObject logicalor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bool = (self as HassiumBool).Bool;
                return new HassiumBool(Bool || args[0].ToBool(vm, args[0], location).Bool);
            }

            [DocStr(
                "@desc Implements the != operator to determine if this bool is not equal to the specified bool.",
                "@param b The bool to compare to.",
                "@returns true if the bools are not equal, otherwise false."
                )]
            [FunctionAttribute("func __notequal__ (b : bool) : bool")]
            public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bool = (self as HassiumBool).Bool;
                return new HassiumBool(Bool != args[0].ToBool(vm, args[0], location).Bool);
            }

            [DocStr(
                "@desc Returns this bool.",
                "@returns This bool."
                )]
            [FunctionAttribute("func tobool () : bool")]
            public static HassiumBool tobool(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return self as HassiumBool;
            }

            [DocStr(
                "@desc Returns the integer value of this bool.",
                "@returns 1 if this is true, otherwise 0."
                )]
            [FunctionAttribute("func toint () : int")]
            public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bool = (self as HassiumBool).Bool;
                return new HassiumInt(Bool ? 1 : 0);
            }
            
            [DocStr(
                "@desc Returns the string value of this bool.",
                "@returns 'true' if this is true, otherwise 'false'."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bool = (self as HassiumBool).Bool;
                return new HassiumString(Bool.ToString().ToLower());
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
