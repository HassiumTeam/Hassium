using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime
{
    public class HassiumConversionFailedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ConversionFailedException");

        public HassiumObject DesiredType { get; set; }
        public HassiumObject Object { get; set; }

        public HassiumConversionFailedException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 2);
        }

        [FunctionAttribute("func new (obj : object, type : object) : ConversionFailedException")]
        public static HassiumConversionFailedException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumConversionFailedException exception = new HassiumConversionFailedException();

            exception.Object = args[0];
            if (args[1] is HassiumTypeDefinition)
                exception.DesiredType = args[1] as HassiumTypeDefinition;
            else if (args[1] is HassiumTrait)
                exception.DesiredType = args[1] as HassiumTrait;
            else
                exception.Object = args[1];
            exception.AddAttribute("desired", new HassiumProperty(exception.get_desired));
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute("object", new HassiumProperty(exception.get_object));
            exception.AddAttribute(TOSTRING, exception.ToString, 0);

            return exception;
        }

        [FunctionAttribute("desired { get; }")]
        public HassiumObject get_desired(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return DesiredType;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Conversion Failed: Could not convert object of type '{0}' to type '{1}'", Object.Type().ToString(vm, location).String, DesiredType.ToString(vm, location).String));
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
