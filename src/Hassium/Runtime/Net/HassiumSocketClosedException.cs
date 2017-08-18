using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hassium.Runtime.Net
{
    public class HassiumSocketClosedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ConnectionClosedException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { INVOKE, new HassiumFunction(_new, 1) },
            { "message", new HassiumProperty(get_message) },
            { "socket", new HassiumProperty(get_socket) },
            { TOSTRING,  new HassiumFunction(tostring, 0) },
        };

        public HassiumSocket Socket { get; set; }

        public HassiumSocketClosedException()
        {
            AddType(TypeDefinition);
            Attributes = HassiumMethod.CloneDictionary(Attribs);
        }

        [FunctionAttribute("func new (sock : Socket) : SocketClosedException")]
        public static HassiumSocketClosedException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumSocketClosedException exception = new HassiumSocketClosedException();

            exception.Socket = args[0] as HassiumSocket;
            exception.Attributes = HassiumMethod.CloneDictionary(Attribs);

            return exception;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Socket Closed: The connection to '{0}' has been terminated", ((self as HassiumSocketClosedException).Socket.Client.Client.RemoteEndPoint as IPEndPoint).Address));
        }

        [FunctionAttribute("socket { get; }")]
        public static HassiumSocket get_socket(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumSocketClosedException).Socket;
        }

        [FunctionAttribute("func toString () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(get_message(vm, self, location).String);
            sb.Append(vm.UnwindCallStack());

            return new HassiumString(sb.ToString());
        }
    }
}
