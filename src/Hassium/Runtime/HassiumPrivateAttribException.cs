using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class HassiumPrivateAttribException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("PrivateAttributeException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {

        };

        public HassiumString Attrib { get; set; }
        public HassiumObject Object { get; set; }

        public HassiumPrivateAttribException()
        {
            AddType(TypeDefinition);
        }

        [FunctionAttribute("func new () : PrivateAttribException")]
        public static HassiumPrivateAttribException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumPrivateAttribException exception = new HassiumPrivateAttribException();

            exception.Object = args[0];
            exception.Attrib = args[1].ToString(vm, args[1], location);

            return exception;
        }

        [FunctionAttribute("attrib { get; }")]
        public HassiumString get_attrib(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return Attrib;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Private Attribute Error: Attribute '{0}' is not publicly accessable from object of type '{1}'", Attrib.String, Object.Type()));
        }

        [FunctionAttribute("object { get; }")]
        public HassiumObject get_object(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return Object;
        }
    }
}
