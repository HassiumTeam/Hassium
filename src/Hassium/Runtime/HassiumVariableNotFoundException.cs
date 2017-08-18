using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime
{
    public class HassiumVariableNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("VariableNotFoundException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { INVOKE, new HassiumFunction(_new, 0) },
            { "message", new HassiumProperty(get_message) }
        };

        public HassiumVariableNotFoundException()
        {
            AddType(TypeDefinition);
            Attributes = HassiumMethod.CloneDictionary(Attribs);
        }

        [FunctionAttribute("func new () : VariableNotFoundException")]
        public static HassiumVariableNotFoundException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumVariableNotFoundException exception = new HassiumVariableNotFoundException();

            exception.Attributes = HassiumMethod.CloneDictionary(Attribs);

            return exception;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Variable Not Found: variable was not found inside the stack frmae"));
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
