using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime.IO
{
    public class HassiumFileClosedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("StreamClosedException");

        public HassiumFile File { get; set; }
        public HassiumString FilePath { get; set; }

        public HassiumFileClosedException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 2);
        }

        [FunctionAttribute("func new (file : File, path : string) : FileClosedException")]
        public static HassiumFileClosedException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumFileClosedException exception = new HassiumFileClosedException();

            exception.File = args[0] as HassiumFile;
            exception.FilePath = args[1].ToString(vm, location);
            exception.AddAttribute("file", new HassiumProperty(exception.get_file));
            exception.AddAttribute("filepath", new HassiumProperty(exception.get_filepath));
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute(TOSTRING, exception.ToString, 0);

            return exception;
        }

        [FunctionAttribute("file { get; }")]
        public HassiumFile get_file(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return File;
        }

        [FunctionAttribute("filepath { get; }")]
        public HassiumString get_filepath(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return FilePath;
        }

        [FunctionAttribute("message { message; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("File Closed: Filepath '{0}' has been closed", FilePath.String));
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
