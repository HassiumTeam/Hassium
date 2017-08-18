using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime
{
    public class HassiumConversionFailedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ConversionFailedException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { "desired", new HassiumProperty(get_desired) },
            { INVOKE, new HassiumFunction(_new, 2) },
            { "message", new HassiumProperty(get_message) },
            { TOSTRING, new HassiumFunction(tostring, 0) }
        };

        public HassiumObject DesiredType { get; set; }
        public HassiumObject Object { get; set; }

        public HassiumConversionFailedException()
        {
            AddType(TypeDefinition);
            Attributes = HassiumMethod.CloneDictionary(Attribs);
        }

        [FunctionAttribute("func new (obj : object, type : object) : ConversionFailedException")]
        public static HassiumConversionFailedException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumConversionFailedException exception = new HassiumConversionFailedException();

            exception.Object = args[0];
            if (args[1] is HassiumTypeDefinition)
                exception.DesiredType = args[1] as HassiumTypeDefinition;
            else if (args[1] is HassiumTrait)
                exception.DesiredType = args[1] as HassiumTrait;
            else
                exception.Object = args[1];
            exception.Attributes = HassiumMethod.CloneDictionary(Attribs);

            return exception;
        }

        [FunctionAttribute("desired { get; }")]
        public static HassiumObject get_desired(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumConversionFailedException).DesiredType;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var exception = (self as HassiumConversionFailedException);
            return new HassiumString(string.Format("Conversion Failed: Could not convert object of type '{0}' to type '{1}'", exception.Object.Type().ToString(vm, exception.Object.Type(), location).String, exception.DesiredType.ToString(vm, exception.DesiredType, location).String));
        }

        [FunctionAttribute("object { get; }")]
        public HassiumObject get_object(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return Object;
        }

        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(get_message(vm, self, location).String);
            sb.Append(vm.UnwindCallStack());

            return new HassiumString(sb.ToString());
        }
    }
}
