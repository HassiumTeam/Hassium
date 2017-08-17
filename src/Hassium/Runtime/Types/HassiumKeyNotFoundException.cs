using Hassium.Compiler;

using System.Text;

namespace Hassium.Runtime.Types
{
    public class HassiumKeyNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("KeyNotFoundException");

        public HassiumObject Key { get; private set; }
        public HassiumObject Object { get; private set; }

        public HassiumKeyNotFoundException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 2);
            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumKeyNotFoundException exception)
        {
            exception.AddAttribute("key", new HassiumProperty(exception.get_key));
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute("object", new HassiumProperty(exception.get_object));
            exception.AddAttribute(TOSTRING, exception.ToString, 0);
        }

        [FunctionAttribute("func new (obj : object, key : object) : KeyNotFoundException")]
        public static HassiumKeyNotFoundException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumKeyNotFoundException exception = new HassiumKeyNotFoundException();

            exception.Object = args[0];
            exception.Key = args[1];
            ImportAttribs(exception);

            return exception;
        }

        [FunctionAttribute("key { get; }")]
        public HassiumObject get_key(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Key;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Key Not Found Error: Could not find key '{0}' in object of type '{1}'", Key.ToString(vm, location).String, Object.Type()));
        }

        [FunctionAttribute("object { get; }")]
        public HassiumObject get_object(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Object;
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
