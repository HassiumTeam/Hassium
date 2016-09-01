using System;
using System.Net;
using System.Net.Sockets;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Net
{
    public class HassiumConnectionListener: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ConnectionListener");

        public TcpListener TcpListener { get; set; }
        public HassiumConnectionListener()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 2);
        }

        public HassiumConnectionListener _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumConnectionListener connectionListener = new HassiumConnectionListener();
            connectionListener.TcpListener = new TcpListener(IPAddress.Parse(args[0].ToString(vm).String), (int)args[0].ToInt(vm).Int);
            connectionListener.AddAttribute("acceptConnection",    connectionListener.acceptConnection, 0);
            connectionListener.AddAttribute("pending",             connectionListener.pending, 0);
            connectionListener.AddAttribute("start",               connectionListener.start, 0);
            connectionListener.AddAttribute("stop",                connectionListener.stop, 0);

            return connectionListener;
        }

        public HassiumNetConnection acceptConnection(VirtualMachine vm, HassiumObject[] args)
        {
            return HassiumNetConnection.CreateFromTcpClient(TcpListener.AcceptTcpClient());
        }
        public HassiumBool pending(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(TcpListener.Pending());
        }
        public HassiumNull start(VirtualMachine vm, params HassiumObject[] args)
        {
            TcpListener.Start();
            return HassiumObject.Null;
        }
        public HassiumNull stop(VirtualMachine vm, params HassiumObject[] args)
        {
            TcpListener.Stop();
            return HassiumObject.Null;
        }
    }
}
