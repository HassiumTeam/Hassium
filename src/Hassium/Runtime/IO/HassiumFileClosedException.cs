using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.IO
{
    public class HassiumFileClosedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new FileClosedExceptionTypeDef();

        public HassiumFile File { get; set; }
        public HassiumString FilePath { get; set; }

        public HassiumFileClosedException()
        {
            AddType(TypeDefinition);
        }

        public class FileClosedExceptionTypeDef : HassiumTypeDefinition
        {
            public FileClosedExceptionTypeDef() : base("FileClosedException")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "file", new HassiumProperty(get_file) },
                    { "filepath", new HassiumProperty(get_filepath) },
                    { INVOKE, new HassiumFunction(_new, 2) },
                    { "message", new HassiumFunction(get_message) },
                    { TOSTRING, new HassiumFunction(tostring, 0) }
                };
            }

            [DocStr(
                "@desc Constructs a new FileClosedException using the specified IO.File object and string path.",
                "@param file The IO.File object that has been closed.",
                "@param path The path of the file that has been closed.",
                "@returns The new FileClosedException object."
                )]
            [FunctionAttribute("func new (file : File, path : string) : FileClosedException")]
            public static HassiumFileClosedException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumFileClosedException exception = new HassiumFileClosedException();

                exception.File = args[0] as HassiumFile;
                exception.FilePath = args[1].ToString(vm, args[1], location);

                return exception;
            }

            [DocStr(
                "@desc Gets the readonly IO.File object that has been closed.",
                "@returns The closed IO.File object."
                )]
            [FunctionAttribute("file { get; }")]
            public static HassiumFile get_file(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumFileClosedException).File;
            }

            [DocStr(
                "@desc Gets the readonly string filepath that has been closed.",
                "@returns The closed string filepath."
                )]
            [FunctionAttribute("filepath { get; }")]
            public static HassiumString get_filepath(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumFileClosedException).FilePath;
            }

            [DocStr(
                "@desc Gets the readonly string message of the exception.",
                "@returns The exception message string."
                )]
            [FunctionAttribute("message { message; }")]
            public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(string.Format("File Closed: Filepath '{0}' has been closed", (self as HassiumFileClosedException).FilePath.String));
            }

            [DocStr(
                "@desc Returns the string value of the exception, including the message and callstack.",
                "@returns The string value of the exception."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(get_message(vm, self, location).String);
                sb.Append(vm.UnwindCallStack());

                return new HassiumString(sb.ToString());
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
