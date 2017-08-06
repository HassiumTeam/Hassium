using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime
{
    public class HassiumPrivateAttributeException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("PrivateAttributeException");

        public HassiumString Attrib { get; set; }
        public HassiumObject Object { get; set; }

        public HassiumPrivateAttributeException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 2);
        }

        public static HassiumPrivateAttributeException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumPrivateAttributeException exception = new HassiumPrivateAttributeException();

            exception.Attrib = args[0].ToString(vm, location);
            exception.Object = args[1];
            exception.AddAttribute("attrib", new HassiumProperty(exception.get_attrib));
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute("object", new HassiumProperty(exception.get_object));

            return exception;
        }

        public HassiumString get_attrib(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Attrib;
        }

        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Private Attribute Error: Attribute '{0}' is not publicly accessable from object of type '{1}'", Attrib.String, Object.Type()));
        }

        public HassiumObject get_object(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Object;
        }
    }
}
