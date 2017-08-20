using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumBool : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("bool");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { EQUALTO, new HassiumFunction(equalto, 1)  },
            { LOGICALAND, new HassiumFunction(logicaland, 1)  },
            { LOGICALNOT, new HassiumFunction(logicalnot, 0)  },
            { LOGICALOR, new HassiumFunction(logicalor, 1)  },
            { NOTEQUALTO, new HassiumFunction(notequalto, 1)  },
            { TOBOOL, new HassiumFunction(tobool, 0)  },
            { TOINT, new HassiumFunction(toint, 0)  },
            { TOSTRING, new HassiumFunction(tostring, 0)  },
        };

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

        [FunctionAttribute("func __equals__ (b : bool) : bool")]
        public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Bool = (self as HassiumBool).Bool;
            return new HassiumBool(Bool == args[0].ToBool(vm, args[0], location).Bool);
        }

        public override HassiumObject LogicalAnd(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool && args[0].ToBool(vm, args[0], location).Bool);
        }

        [FunctionAttribute("func __logicaland__ (b : bool) : bool")]
        public static HassiumObject logicaland(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Bool = (self as HassiumBool).Bool;
            return new HassiumBool(Bool && args[0].ToBool(vm, args[0], location).Bool);
        }

        public override HassiumObject LogicalNot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(!Bool);
        }

        [FunctionAttribute("func __logicalnot__ (b : bool) : bool")]
        public static HassiumObject logicalnot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Bool = (self as HassiumBool).Bool;
            return new HassiumBool(!Bool);
        }

        public override HassiumObject LogicalOr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool || args[0].ToBool(vm, args[0], location).Bool);
        }

        [FunctionAttribute("func __logicalor__ (b : bool) : bool")]
        public static HassiumObject logicalor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Bool = (self as HassiumBool).Bool;
            return new HassiumBool(Bool || args[0].ToBool(vm, args[0], location).Bool);
        }

        public override HassiumBool NotEqualTo(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Bool != args[0].ToBool(vm, args[0], location).Bool);
        }

        [FunctionAttribute("func __notequal__ (b : bool) : bool")]
        public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Bool = (self as HassiumBool).Bool;
            return new HassiumBool(Bool != args[0].ToBool(vm, args[0], location).Bool);
        }

        public override HassiumBool ToBool(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return this;
        }

        [FunctionAttribute("func tobool () : bool")]
        public static HassiumBool tobool(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return self as HassiumBool;
        }

        public override HassiumInt ToInt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Bool ? 1 : 0);
        }

        [FunctionAttribute("func toint () : int")]
        public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Bool = (self as HassiumBool).Bool;
            return new HassiumInt(Bool ? 1 : 0);
        }

        public override HassiumString ToString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Bool.ToString().ToLower());
        }

        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var Bool = (self as HassiumBool).Bool;
            return new HassiumString(Bool.ToString().ToLower());
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
