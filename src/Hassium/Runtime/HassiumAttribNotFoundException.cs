using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime
{
    public class HassiumAttribNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("AttributeNotFoundException");

        public HassiumObject Object { get; private set; }
        public HassiumString Attribute { get; private set; }

        public HassiumAttribNotFoundException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 2);
            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumAttribNotFoundException exception)
        {
            exception.AddAttribute("attribute", new HassiumProperty(exception.get_attribute));
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute("object", new HassiumProperty(exception.get_object));
            exception.AddAttribute(TOSTRING, exception.Attributes["message"]);
        }

        [FunctionAttribute("func new (obj : object, attrib : string) : AttributeNotFoundException")]
        public static HassiumAttribNotFoundException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumAttribNotFoundException exception = new HassiumAttribNotFoundException();

            exception.Object = args[0];
            exception.Attribute = args[1].ToString(vm, location);
            ImportAttribs(exception);

            return exception;
        }

        [FunctionAttribute("attribute { get; }")]
        public HassiumObject get_attribute(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Attribute;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Attribute Not Found: Could not find attribute '{0}' in object of type '{1}'", Attribute.String, Object.Type().ToString(vm, location).String));
        }

        [FunctionAttribute("object { get; }")]
        public HassiumObject get_object(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Object;
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(get_message(vm, location).String);
            sb.Append(vm.UnwindCallStack());

            return new HassiumString(sb.ToString());
        }
    }
}
