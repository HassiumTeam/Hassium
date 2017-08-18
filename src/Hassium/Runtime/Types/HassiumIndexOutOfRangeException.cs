using Hassium.Compiler;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.Types
{
    public class HassiumIndexOutOfRangeException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("IndexOutOfRangeException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { "index", new HassiumProperty(get_index) },
            { "message", new HassiumProperty(get_message) },
            { "object", new HassiumProperty(get_object) },
            { TOSTRING, new HassiumFunction(tostring, 0) }
        };

        public HassiumObject Object { get;  set; }
        public HassiumInt RequestedIndex { get; set; }

        public HassiumIndexOutOfRangeException()
        {
            AddType(TypeDefinition);
            Attributes = new Dictionary<string, HassiumObject>(Attribs);
        }

        [FunctionAttribute("func new (obj : object, int reqIndex) : IndexOutOfRangeException")]
        public static HassiumIndexOutOfRangeException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumIndexOutOfRangeException exception = new HassiumIndexOutOfRangeException();

            exception.Object = args[0];
            exception.RequestedIndex = args[1].ToInt(vm, args[1], location);
            exception.Attributes = new Dictionary<string, HassiumObject>(Attribs);

            return exception;
        }

        [FunctionAttribute("index { get; }")]
        public static HassiumInt get_index(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumIndexOutOfRangeException).RequestedIndex;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var exception = (self as HassiumIndexOutOfRangeException);
            return new HassiumString(string.Format("Out of range: Index '{0}' is less than 0 or greater than the size of the collection of type '{1}'", exception.RequestedIndex.Int, exception.Object.Type()));
        }

        [FunctionAttribute("object { get; }")]
        public static HassiumObject get_object(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumIndexOutOfRangeException).Object;
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
