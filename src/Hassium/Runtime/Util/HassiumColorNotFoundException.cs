using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime.Util
{
    public class HassiumColorNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ColorNotFoundException");

        public HassiumString ColorString { get; set; }

        public HassiumColorNotFoundException()
        {
            AddType(TypeDefinition);

            AddAttribute(INVOKE, _new, 1);
        }

        [FunctionAttribute("func new (col : string) : ColorNotFoundException")]
        public static HassiumColorNotFoundException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumColorNotFoundException exception = new HassiumColorNotFoundException();

            exception.ColorString = args[0].ToString(vm, location);
            exception.AddAttribute("color", new HassiumProperty(exception.get_color));
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute(TOSTRING, exception.ToString, 0);

            return exception;
        }

        [FunctionAttribute("color { get; }")]
        public HassiumString get_color(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return ColorString;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Color Not Found: Color '{0}' does not exist", ColorString.String));
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
