using System;
using System.Net.Sockets;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumSocket: HassiumObject
    {
        public Socket Value { get; private set; }

        public HassiumSocket(Socket value)
        {
            Value = value;

            Attributes.Add("connect", new InternalFunction(connect));
            Attributes.Add("accept", new InternalFunction(accept));
            Attributes.Add("available", new InternalFunction(available));
            Attributes.Add("connected", new InternalFunction(connected));
            Attributes.Add("protocolType", new InternalFunction(protocolType));
            Attributes.Add("close", new InternalFunction(close));
            Attributes.Add("disconnect", new InternalFunction(disconnect));
            Attributes.Add("dispose", new InternalFunction(dispose));
            Attributes.Add("listen", new InternalFunction(listen));
            Attributes.Add("sendFile", new InternalFunction(sendFile));
            Attributes.Add("send", new InternalFunction(send));
        }

        private HassiumObject connect(HassiumObject[] args)
        {
            Value.Connect(args[0].ToString(), Convert.ToInt32(args[1].ToString()));
            return null;
        }

        private HassiumObject accept(HassiumObject[] args)
        {
            Value.Accept();
            return null;
        }

        private HassiumObject available(HassiumObject[] args)
        {
            return new HassiumNumber(Value.Available);
        }

        private HassiumObject connected(HassiumObject[] args)
        {
            return new HassiumBool(Value.Connected);
        }

        private HassiumObject protocolType(HassiumObject[] args)
        {
            return new HassiumString(Value.ProtocolType.ToString());
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        private HassiumObject disconnect(HassiumObject[] args)
        {
            Value.Disconnect(((HassiumBool)args[0]).Value);
            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();
            return null;
        }

        private HassiumObject listen(HassiumObject[] args)
        {
            Value.Listen(((HassiumNumber)args[0]).ValueInt);
            return null;
        }

        private HassiumObject sendFile(HassiumObject[] args)
        {
            Value.SendFile(args[0].ToString());
            return null;
        }

        private HassiumObject send(HassiumObject[] args)
        {
            Value.Send(Encoding.ASCII.GetBytes(args[0].ToString()));
            return null;
        }
    }
}

