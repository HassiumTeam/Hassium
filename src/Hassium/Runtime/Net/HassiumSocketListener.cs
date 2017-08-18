using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Net;
using System.Net.Sockets;

namespace Hassium.Runtime.Net
{
    public class HassiumSocketListener : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("SocketListener");

        public TcpListener TcpListener { get; set; }

        public HassiumSocketListener()
        {
            AddType(TypeDefinition);

            AddAttribute(INVOKE, _new, 1, 2);
            ImportAttribs(this);
        }
        
        public static void ImportAttribs(HassiumSocketListener listener)
        {
            listener.AddAttribute("acceptsock", listener.acceptsock, 0);
            listener.AddAttribute("localip", new HassiumProperty(listener.get_localip));
            listener.AddAttribute("start", listener.start, 0);
            listener.AddAttribute("stop", listener.stop, 0);
        }

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
            ImportAttribs(listener);

            return listener;
        }

        [FunctionAttribute("func acceptsock () : Socket")]
        public HassiumSocket acceptsock(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumSocket socket = new HassiumSocket();

            HassiumSocket.ImportAttribs(socket);
            socket.Client = TcpListener.AcceptTcpClient();

            return socket;
        }

        [FunctionAttribute("localip { get; }")]
        public HassiumObject get_localip(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return HassiumIPAddr.Attribs[INVOKE].Invoke(vm, location, new HassiumString((TcpListener.LocalEndpoint as IPEndPoint).Address.ToString()), new HassiumInt((TcpListener.LocalEndpoint as IPEndPoint).Port));
        }

        [FunctionAttribute("func start () : null")]
        public HassiumNull start(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            TcpListener.Start();
            return Null;
        }

        [FunctionAttribute("func stop () : null")]
        public HassiumNull stop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            TcpListener.Stop();
            return Null;
        }
    }
}
