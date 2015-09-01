using System;
using System.Net.Sockets;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumSocket: HassiumObject
    {
        public Socket Value { get; private set; }

        public HassiumSocket(Socket value)
        {
            this.Value = value;

            this.Attributes.Add("connect", new InternalFunction(connect));
            this.Attributes.Add("accept", new InternalFunction(accept));
            this.Attributes.Add("available", new InternalFunction(available));
            this.Attributes.Add("connected", new InternalFunction(connected));
            this.Attributes.Add("protocolType", new InternalFunction(protocolType));
            this.Attributes.Add("close", new InternalFunction(close));
            this.Attributes.Add("disconnect", new InternalFunction(disconnect));
            this.Attributes.Add("dispose", new InternalFunction(dispose));
            this.Attributes.Add("listen", new InternalFunction(listen));
            this.Attributes.Add("sendFile", new InternalFunction(sendFile));
            this.Attributes.Add("send", new InternalFunction(send));
        }

        private HassiumObject connect(HassiumObject[] args)
        {
            Value.Connect(args[0].ToString(), Convert.ToInt32(args[1].ToString()));
            return null;
        }

        private HassiumObject accept(HassiumObject[] args)
        {
            this.Value.Accept();
            return null;
        }

        private HassiumObject available(HassiumObject[] args)
        {
            return new HassiumNumber(this.Value.Available);
        }

        private HassiumObject connected(HassiumObject[] args)
        {
            return new HassiumBool(this.Value.Connected);
        }

        private HassiumObject protocolType(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ProtocolType.ToString());
        }

        private HassiumObject close(HassiumObject[] args)
        {
            this.Value.Close();
            return null;
        }

        private HassiumObject disconnect(HassiumObject[] args)
        {
            this.Value.Disconnect(((HassiumBool)args[0]).Value);
            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            this.Value.Dispose();
            return null;
        }

        private HassiumObject listen(HassiumObject[] args)
        {
            this.Value.Listen(((HassiumNumber)args[0]).ValueInt);
            return null;
        }

        private HassiumObject sendFile(HassiumObject[] args)
        {
            this.Value.SendFile(args[0].ToString());
            return null;
        }

        private HassiumObject send(HassiumObject[] args)
        {
            this.Value.Send(System.Text.Encoding.ASCII.GetBytes(args[0].ToString()));
            return null;
        }
    }
}

