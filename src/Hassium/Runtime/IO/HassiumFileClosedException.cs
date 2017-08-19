using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.IO
{
    public class HassiumFileClosedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("StreamClosedException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { "file", new HassiumProperty(get_file) },
            { "filepath", new HassiumProperty(get_filepath) },
            { INVOKE, new HassiumFunction(_new, 2) },
            { "message", new HassiumFunction(get_message) },
            { TOSTRING, new HassiumFunction(tostring, 0) }
        };

        public HassiumFile File { get; set; }
        public HassiumString FilePath { get; set; }

        public HassiumFileClosedException()
        {
            AddType(TypeDefinition);
            
        }

        [FunctionAttribute("func new (file : File, path : string) : FileClosedException")]
        public static HassiumFileClosedException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumFileClosedException exception = new HassiumFileClosedException();

            exception.File = args[0] as HassiumFile;
            exception.FilePath = args[1].ToString(vm, args[1], location);

            return exception;
        }

        [FunctionAttribute("file { get; }")]
        public static HassiumFile get_file(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumFileClosedException).File;
        }

        [FunctionAttribute("filepath { get; }")]
        public static HassiumString get_filepath(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumFileClosedException).FilePath;
        }

        [FunctionAttribute("message { message; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("File Closed: Filepath '{0}' has been closed", (self as HassiumFileClosedException).FilePath.String));
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
