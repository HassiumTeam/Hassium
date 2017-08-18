using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime
{
    public class HassiumArgLengthException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ArgumentLengthException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { "expected", new HassiumProperty(get_expected) },
            { "function", new HassiumProperty(get_function) },
            { "given", new HassiumProperty(get_given) },
            { INVOKE, new HassiumFunction(_new, 3) },
            { TOSTRING, new HassiumFunction(tostring, 0) }
        };

        public HassiumInt ExpectedLength { get; private set; }
        public HassiumObject Function { get; private set; }
        public HassiumInt GivenLength { get; private set; }

        public HassiumArgLengthException()
        {
            AddType(TypeDefinition);
            
            Attributes = HassiumMethod.CloneDictionary(Attribs);
        }

        [FunctionAttribute("func new (fn : object, expected : int, given : int) : ArgumentLengthException")]
        public static HassiumArgLengthException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumArgLengthException exception = new HassiumArgLengthException();

            exception.ExpectedLength = args[1].ToInt(vm, args[1], location);
            exception.Function = args[0];
            exception.GivenLength = args[2].ToInt(vm, args[2], location);
            exception.Attributes = HassiumMethod.CloneDictionary(Attribs);

            return exception;
        }

        [FunctionAttribute("expected { get; }")]
        public static HassiumInt get_expected(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumArgLengthException).ExpectedLength;
        }

        [FunctionAttribute("function { get; }")]
        public static HassiumObject get_function(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumArgLengthException).Function;
        }

        [FunctionAttribute("given { get; }")]
        public static HassiumInt get_given(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumArgLengthException).GivenLength;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var exception = (self as HassiumArgLengthException);
            return new HassiumString(string.Format("Argument Length Error: Expected '{0}' arguments, '{1}' given", exception.ExpectedLength.Int, exception.GivenLength.Int));
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
