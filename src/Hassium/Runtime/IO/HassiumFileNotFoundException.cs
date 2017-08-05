using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime.IO
{
    public class HassiumFileNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("FileNotFoundException");
        
        public HassiumString Path { get; set; }

        public HassiumFileNotFoundException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 1);
        }

        [FunctionAttribute("func new (path : string) : FileNotFoundException")]
        public static HassiumFileNotFoundException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumFileNotFoundException exception = new HassiumFileNotFoundException();

            exception.Path = args[0].ToString(vm, location);
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute("path", new HassiumProperty(exception.get_path));
            exception.AddAttribute(TOSTRING, exception.ToString, 0);

            return exception;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("File not found: '{0}' does not exist!", Path.String));
        }

        [FunctionAttribute("path { get; }")]
        public HassiumObject get_path(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Path;
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
