using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime.IO
{
    public class HassiumDirectoryNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("DirectoryNotFoundException");

        public HassiumString Path { get; set; }

        public HassiumDirectoryNotFoundException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 1);
        }

        [FunctionAttribute("func new (path : str) : DirectoryNotFoundException")]
        public static HassiumDirectoryNotFoundException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumDirectoryNotFoundException exception = new HassiumDirectoryNotFoundException();

            exception.Path = args[0].ToString(vm, location);
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute("path", new HassiumProperty(exception.get_path));
            exception.AddAttribute(TOSTRING, exception.ToString, 0);

            return exception;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Directory Not Found: '{0}' does not exist", Path.String));
        }

        [FunctionAttribute("path { get; }")]
        public HassiumString get_path(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
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
