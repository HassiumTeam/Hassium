using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime
{
    public class HassiumVariableNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("VariableNotFoundException");

        public HassiumVariableNotFoundException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 0);
        }

        [FunctionAttribute("func new () : VariableNotFoundException")]
        public static HassiumVariableNotFoundException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumVariableNotFoundException exception = new HassiumVariableNotFoundException();

            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute(TOSTRING, exception.Attributes["message"]);

            return exception;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Variable Not Found: variable was not found inside the stack frmae"));
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
