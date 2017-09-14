using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hassium.Runtime.Net
{
    public class HassiumSocketClosedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new SocketClosedExceptionTypeDef();
        public HassiumSocket Socket { get; set; }

        public HassiumSocketClosedException()
        {
            AddType(TypeDefinition);
        }

        [DocStr(
            "@desc A class representing an exception that is thrown when access is attempted to a socket that has been closed.",
            "@returns SocketClosedException."
            )]
        public class SocketClosedExceptionTypeDef : HassiumTypeDefinition
        {
            public SocketClosedExceptionTypeDef() : base("SocketClosedException")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { INVOKE, new HassiumFunction(_new, 1) },
                    { "message", new HassiumProperty(get_message) },
                    { "socket", new HassiumProperty(get_socket) },
                    { TOSTRING,  new HassiumFunction(tostring, 0) },
                };
            }

            [DocStr(
                "@desc Constructs a new SocketClosedException using the specified Net.Socket object.",
                "@param file The Net.Socket object that has been closed.",
                "@returns The new SocketClosedException object."
                )]
            [FunctionAttribute("func new (sock : Socket) : SocketClosedException")]
            public static HassiumSocketClosedException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumSocketClosedException exception = new HassiumSocketClosedException();

                exception.Socket = args[0] as HassiumSocket;

                return exception;
            }

            [DocStr(
                "@desc Gets the readonly string message of the exception.",
                "@returns The exception message string."
                )]
            [FunctionAttribute("message { get; }")]
            public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(string.Format("Socket Closed: The connection to '{0}' has been terminated", ((self as HassiumSocketClosedException).Socket.Client.Client.RemoteEndPoint as IPEndPoint).Address));
            }

            [DocStr(
                "@desc Gets the readonly Net.Socket object that has been closed.",
                "@returns The closed Net.Socket object."
                )]
            [FunctionAttribute("socket { get; }")]
            public static HassiumSocket get_socket(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumSocketClosedException).Socket;
            }

            [DocStr(
                "@desc Returns the string value of the exception, including the message and callstack.",
                "@returns The string value of the exception."
                )]
            [FunctionAttribute("func toString () : string")]
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
