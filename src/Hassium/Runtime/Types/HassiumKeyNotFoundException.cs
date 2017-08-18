using Hassium.Compiler;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.Types
{
    public class HassiumKeyNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("KeyNotFoundException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>
        {
            { "key", new HassiumProperty(get_key) },
            { "message", new HassiumProperty(get_message) },
            { TOSTRING, new HassiumFunction(tostring, 0) }
        };

        public HassiumObject Key { get; private set; }
        public HassiumObject Object { get; private set; }

        public HassiumKeyNotFoundException()
        {
            AddType(TypeDefinition);
            Attributes = new Dictionary<string, HassiumObject>(Attribs);
        }

        [FunctionAttribute("func new (obj : object, key : object) : KeyNotFoundException")]
        public static HassiumKeyNotFoundException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumKeyNotFoundException exception = new HassiumKeyNotFoundException();

            exception.Object = args[0];
            exception.Key = args[1];
            exception.Attributes = new Dictionary<string, HassiumObject>(Attribs);

            return exception;
        }

        [FunctionAttribute("key { get; }")]
        public static HassiumObject get_key(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumKeyNotFoundException).Key;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var exception = (self as HassiumKeyNotFoundException);
            return new HassiumString(string.Format("Key Not Found Error: Could not find key '{0}' in object of type '{1}'", exception.Key.ToString(vm, exception.Key, location).String, exception.Object.Type()));
        }

        [FunctionAttribute("object { get; }")]
        public static HassiumObject get_object(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumKeyNotFoundException).Object;
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
