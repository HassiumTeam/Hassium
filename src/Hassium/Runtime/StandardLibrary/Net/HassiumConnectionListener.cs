using System;
using System.Net;
using System.Net.Sockets;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Net
{
    public class HassiumConnectionListener: HassiumObject
    {
        public TcpListener TcpListener { get; set; }
        public HassiumConnectionListener()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 2));
        }

        private HassiumConnectionListener _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumConnectionListener hassiumConnectionListener = new HassiumConnectionListener();

            hassiumConnectionListener.TcpListener = new TcpListener(IPAddress.Parse(HassiumString.Create(args[0]).Value), (int)HassiumInt.Create(args[1]).Value);
            hassiumConnectionListener.Attributes.Add("acceptConnection",    new HassiumFunction(hassiumConnectionListener.acceptConnection, 0));
            hassiumConnectionListener.Attributes.Add("pending",             new HassiumFunction(hassiumConnectionListener.pending, 0));
            hassiumConnectionListener.Attributes.Add("start",               new HassiumFunction(hassiumConnectionListener.start, 0));
            hassiumConnectionListener.Attributes.Add("stop",                new HassiumFunction(hassiumConnectionListener.stop, 0));

            return hassiumConnectionListener;
        }

        public HassiumNetConnection acceptConnection(VirtualMachine vm, HassiumObject[] args)
        {
            return HassiumNetConnection.CreateFromTcpClient(TcpListener.AcceptTcpClient());
        }
        public HassiumBool pending(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(TcpListener.Pending());
        }
        public HassiumNull start(VirtualMachine vm, HassiumObject[] args)
        {
            TcpListener.Start();
            return HassiumObject.Null;
        }
        public HassiumNull stop(VirtualMachine vm, HassiumObject[] args)
        {
            TcpListener.Stop();
            return HassiumObject.Null;
        }
    }
}

