using System;
using System.Net.Sockets;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumSocket : HassiumObject
    {
        public Socket Value { get; private set; }

        public HassiumSocket(Socket value)
        {
            Value = value;

            Attributes.Add("connect", new InternalFunction(connect, 2));
            Attributes.Add("accept", new InternalFunction(accept, 0));
            Attributes.Add("available", new InternalFunction(x => value.Available, 0, true));
            Attributes.Add("connected", new InternalFunction(x => value.Connected, 0, true));
            Attributes.Add("protocolType", new InternalFunction(x => value.ProtocolType.ToString(), 0, true));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("disconnect", new InternalFunction(disconnect, 1));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("listen", new InternalFunction(listen, 1));
            Attributes.Add("sendFile", new InternalFunction(sendFile, 1));
            Attributes.Add("send", new InternalFunction(send, 1));
        }

        private HassiumObject connect(HassiumObject[] args)
        {
            Value.Connect(args[0].ToString(), args[1].HInt().Value);
            return null;
        }

        private HassiumObject accept(HassiumObject[] args)
        {
            return new HassiumSocket(Value.Accept());
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        private HassiumObject disconnect(HassiumObject[] args)
        {
            Value.Disconnect(args[0].HBool().Value);
            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();
            return null;
        }

        private HassiumObject listen(HassiumObject[] args)
        {
            Value.Listen(args[0].HInt().Value);
            return null;
        }

        private HassiumObject sendFile(HassiumObject[] args)
        {
            Value.SendFile(args[0].ToString());
            return null;
        }

        private HassiumObject send(HassiumObject[] args)
        {
            return Value.Send(Encoding.ASCII.GetBytes(args[0].ToString()));
        }
    }
}