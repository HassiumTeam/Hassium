using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Hassium.Runtime.Net
{
    public class HassiumSocketListener : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new SocketListenerTypeDef();

        public TcpListener TcpListener { get; set; }

        public HassiumSocketListener()
        {
            AddType(TypeDefinition);
        }

        public class SocketListenerTypeDef : HassiumTypeDefinition
        {
            public SocketListenerTypeDef() : base("SocketListener")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "acceptsock", new HassiumFunction(acceptsock, 0) },
                    { INVOKE, new HassiumFunction(_new, 1, 2) },
                    { "localip", new HassiumProperty(get_localip) },
                    { "start", new HassiumFunction(start, 0) },
                    { "stop", new HassiumFunction(stop, 0) },
                };
            }

            [DocStr(
                "@desc Constructs a new SocketListener object using either the specified port, the specified Net.IPAddr, or the specified string ip and int port.",
                "@optional portOrIPAddr The int port to listen on or Net.IPAddr object.",
                "@optional ip The string ip address to listen on.",
                "@optional port The int port to listen on.",
                "@returns The new SocketListener object."
                )]
            [FunctionAttribute("func new (portOrIPAddr : object) : SocketListener", "func new (ip : string, port : int) : SocketListener")]
            public static HassiumSocketListener _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumSocketListener listener = new HassiumSocketListener();

                switch (args.Length)
                {
                    case 1:
                        if (args[0] is HassiumIPAddr)
                        {
                            var ip = args[0] as HassiumIPAddr;
                            listener.TcpListener = new TcpListener(IPAddress.Parse(ip.Address.String), (int)args[1].ToInt(vm, args[1], location).Int);
                        }
                        else
                            listener.TcpListener = new TcpListener(IPAddress.Any, (int)args[0].ToInt(vm, args[0], location).Int);
                        break;
                    case 2:
                        listener.TcpListener = new TcpListener(IPAddress.Parse(args[0].ToString(vm, args[0], location).String), (int)args[1].ToInt(vm, args[1], location).Int);
                        break;
                }

                return listener;
            }

            [DocStr(
                "@desc Hangs until a new connection has been received and returns a Net.Socket object of that client.",
                "@returns The Net.Socket object that connected to this listener."
                )]
            [FunctionAttribute("func acceptsock () : Socket")]
            public HassiumSocket acceptsock(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumSocket socket = new HassiumSocket();

                socket.Client = (self as HassiumSocketListener).TcpListener.AcceptTcpClient();

                return socket;
            }

            [DocStr(
                "@desc Gets the readonly Net.IPAddr of the ip the listener is listening on (local ip).",
                "@returns The Net.IPAddr object of the local address."
                )]
            [FunctionAttribute("localip { get; }")]
            public HassiumObject get_localip(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return HassiumIPAddr.IPAddrTypeDef._new(vm, null, location, new HassiumString(((self as HassiumSocketListener).TcpListener.LocalEndpoint as IPEndPoint).Address.ToString()), new HassiumInt(((self as HassiumSocketListener).TcpListener.LocalEndpoint as IPEndPoint).Port));
            }

            [DocStr(
                "@desc Starts the listener.",
                "@returns null."
                )]
            [FunctionAttribute("func start () : null")]
            public HassiumNull start(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumSocketListener).TcpListener.Start();
                return Null;
            }

            [DocStr(
                "@desc Stops the listener.",
                "@returns null."
                )]
            [FunctionAttribute("func stop () : null")]
            public HassiumNull stop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumSocketListener).TcpListener.Stop();
                return Null;
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
