using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Net;
using System.Text;

namespace Hassium.Runtime.Net
{
    public class HassiumSocketClosedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ConnectionClosedException");

        public HassiumSocket Socket { get; set; }

        public HassiumSocketClosedException()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 1);
            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumSocketClosedException exception)
        {
            exception.AddAttribute("message", new HassiumProperty(exception.get_message));
            exception.AddAttribute("socket", new HassiumProperty(exception.get_socket));
            exception.AddAttribute(TOSTRING, exception.ToString, 0);
        }

        [FunctionAttribute("func new (sock : Socket) : SocketClosedException")]
        public static HassiumSocketClosedException _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumSocketClosedException exception = new HassiumSocketClosedException();

            exception.Socket = args[0] as HassiumSocket;
            ImportAttribs(exception);

            return exception;
        }

        [FunctionAttribute("message { get; }")]
        public HassiumString get_message(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Socket Closed: The connection to '{0}' has been terminated", (Socket.Client.Client.RemoteEndPoint as IPEndPoint).Address));
        }

        [FunctionAttribute("socket { get; }")]
        public HassiumSocket get_socket(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Socket;
        }

        [FunctionAttribute("func toString () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(get_message(vm, location).String);
            sb.Append(vm.UnwindCallStack());

            return new HassiumString(sb.ToString());
        }
    }
}
