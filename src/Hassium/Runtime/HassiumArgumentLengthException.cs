using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime
{
    public class HassiumArgumentLengthException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ArgumentLengthException");

        public HassiumInt ExpectedLength { get; private set; }
        public HassiumObject Function { get; private set; }
        public HassiumInt GivenLength { get; private set; }

        public HassiumArgumentLengthException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 3);
        }

        [FunctionAttribute("func new (fn : object, expected : int, given : int) : ArgumentLengthException")]
        public static HassiumArgumentLengthException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumArgumentLengthException exception = new HassiumArgumentLengthException();

            exception.ExpectedLength = args[1].ToInt(vm, location);
            exception.Function = args[0];
            exception.GivenLength = args[2].ToInt(vm, location);
            exception.AddAttribute("expected", new HassiumProperty(exception.get_expected));
            exception.AddAttribute("function", new HassiumProperty(exception.get_function));
            exception.AddAttribute("given", new HassiumProperty(exception.get_given));
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute(TOSTRING, exception.ToString, 0);

            return exception;
        }

        [FunctionAttribute("expected { get; }")]
        public HassiumInt get_expected(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return ExpectedLength;
        }

        [FunctionAttribute("function { get; }")]
        public HassiumObject get_function(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Function;
        }

        [FunctionAttribute("given { get; }")]
        public HassiumInt get_given(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return GivenLength;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Argument Length Error: Expected '{0}' arguments, '{1}' given", ExpectedLength.Int, GivenLength.Int));
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
