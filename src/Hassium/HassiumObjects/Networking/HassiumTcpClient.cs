using System;
using System.Net.Sockets;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumTcpClient: HassiumObject
    {
        public TcpClient Value { get; private set; }

        public HassiumTcpClient(TcpClient value)
        {
            this.Value = value;
            this.Attributes.Add("available", new InternalFunction(available));
            this.Attributes.Add("connect", new InternalFunction(connect));
            this.Attributes.Add("connected", new InternalFunction(connected));
            this.Attributes.Add("getStream", new InternalFunction(getStream));
            this.Attributes.Add("close", new InternalFunction(close));
        }

        private HassiumObject available(HassiumObject[] args)
        {
            return new HassiumNumber(this.Value.Available);
        }

        private HassiumObject connect(HassiumObject[] args)
        {
            Value.Connect(args[0].ToString(), Convert.ToInt32(((HassiumNumber)args[1]).Value));
            return null;
        }

        private HassiumObject connected(HassiumObject[] args)
        {
            return new HassiumBool(this.Value.Connected);
        }

        private HassiumObject getStream(HassiumObject[] args)
        {
            return new HassiumNetworkStream(Value.GetStream());
        }

        private HassiumObject close(HassiumObject[] args)
        {
            this.Value.Close();
            return null;
        }
    }
}

