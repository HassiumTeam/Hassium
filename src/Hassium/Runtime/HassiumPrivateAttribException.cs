using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime
{
    public class HassiumPrivateAttribException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("PrivateAttributeException");

        public HassiumString Attrib { get; set; }
        public HassiumObject Object { get; set; }

        public HassiumPrivateAttribException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 2);
        }

        public static void ImportAttribs(HassiumPrivateAttribException exception)
        {
            exception.AddAttribute("attrib", new HassiumProperty(exception.get_attrib));
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute("object", new HassiumProperty(exception.get_object));
        }

        [FunctionAttribute("func new () : PrivateAttribException")]
        public static HassiumPrivateAttribException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumPrivateAttribException exception = new HassiumPrivateAttribException();

            exception.Object = args[0];
            exception.Attrib = args[1].ToString(vm, location);
            ImportAttribs(exception);

            return exception;
        }

        [FunctionAttribute("attrib { get; }")]
        public HassiumString get_attrib(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Attrib;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Private Attribute Error: Attribute '{0}' is not publicly accessable from object of type '{1}'", Attrib.String, Object.Type()));
        }

        [FunctionAttribute("object { get; }")]
        public HassiumObject get_object(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Object;
        }
    }
}
