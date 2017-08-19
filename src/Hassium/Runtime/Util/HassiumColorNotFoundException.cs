using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.Util
{
    public class HassiumColorNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ColorNotFoundException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { "color", new HassiumProperty(get_color) },
            { INVOKE, new HassiumFunction(_new, 1) },
            { "message", new HassiumFunction(get_message) },
            { TOSTRING, new HassiumFunction(tostring, 0) }
        };

        public HassiumString ColorString { get; set; }

        public HassiumColorNotFoundException()
        {
            AddType(TypeDefinition);
            
        }

        [FunctionAttribute("func new (col : string) : ColorNotFoundException")]
        public static HassiumColorNotFoundException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumColorNotFoundException exception = new HassiumColorNotFoundException();

            exception.ColorString = args[0].ToString(vm, args[0], location);

            return exception;
        }

        [FunctionAttribute("color { get; }")]
        public static HassiumString get_color(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumColorNotFoundException).ColorString;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Color Not Found: Color '{0}' does not exist", (self as HassiumColorNotFoundException).ColorString.String));
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
