using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class HassiumPrivateAttribException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new PrivateAttribExceptionTypeDef();

        public HassiumString Attrib { get; set; }
        public HassiumObject Object { get; set; }

        public HassiumPrivateAttribException()
        {
            AddType(TypeDefinition);
        }

        public class PrivateAttribExceptionTypeDef : HassiumTypeDefinition
        {
            public PrivateAttribExceptionTypeDef() : base("PrivateAttributeException")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "attrib", new HassiumProperty(get_attrib) },
                    { INVOKE, new HassiumFunction(_new, 2) },
                    { "message", new HassiumProperty(get_message) },
                    { "object", new HassiumProperty(get_object) },
                    { TOSTRING, new HassiumFunction(tostring, 0) }
                };
            }

            [FunctionAttribute("func new (obj : object, attrib : string) : PrivateAttribException")]
            public static HassiumPrivateAttribException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumPrivateAttribException exception = new HassiumPrivateAttribException();

                exception.Object = args[0];
                exception.Attrib = args[1].ToString(vm, args[1], location);

                return exception;
            }

            [FunctionAttribute("attrib { get; }")]
            public static HassiumString get_attrib(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumPrivateAttribException).Attrib;
            }

            [FunctionAttribute("message { get; }")]
            public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var exception = (self as HassiumPrivateAttribException);
                return new HassiumString(string.Format("Private Attribute Error: Attribute '{0}' is not publicly accessable from object of type '{1}'", exception.Attrib.String, exception.Object.Type()));
            }

            [FunctionAttribute("object { get; }")]
            public static HassiumObject get_object(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumPrivateAttribException).Object;
            }

            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return get_message(vm, self, location, args);
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
