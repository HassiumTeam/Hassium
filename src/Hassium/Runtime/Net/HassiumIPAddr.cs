using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;

namespace Hassium.Runtime.Net
{
    public class HassiumIPAddr : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("IPAddr");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {

        };

        public HassiumString Address { get; private set; }
        public HassiumInt Port { get; private set; }

        public HassiumIPAddr()
        {
            AddType(TypeDefinition);

            AddAttribute(INVOKE, _new, 1, 2);
        }

        [FunctionAttribute("func new (host : string) : IPaddr", "func new (host : string, port : int) : IPAddr")]
        public static HassiumIPAddr _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumIPAddr addr = new HassiumIPAddr();

            addr.Address = args[0].ToString(vm, args[0], location);
            addr.Port = args.Length == 2 ? args[1].ToInt(vm, args[1], location) : new HassiumInt(-1);
            
            return addr;
        }

        [FunctionAttribute("address { get; }")]
        public HassiumString get_address(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return Address;
        }

        [FunctionAttribute("port { get; }")]
        public HassiumInt get_port(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return Port;
        }

        [FunctionAttribute("func tostring () : string")]
        public HassiumString toString(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Port.Int == -1)
                return new HassiumString(Address.String);
            else
                return new HassiumString(string.Format("{0}:{1}", Address.String, Port.Int));
        }
    }
}
