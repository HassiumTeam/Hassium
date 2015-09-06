using System;
using System.Net.Sockets;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumTcpClient: HassiumObject
    {
        public TcpClient Value { get; private set; }

        public HassiumTcpClient(TcpClient value)
        {
            Value = value;
            Attributes.Add("available", new InternalFunction(available));
            Attributes.Add("connect", new InternalFunction(connect));
            Attributes.Add("connected", new InternalFunction(connected));
            Attributes.Add("getStream", new InternalFunction(getStream));
            Attributes.Add("close", new InternalFunction(close));
        }

        private HassiumObject available(HassiumObject[] args)
        {
            return Value.Available;
        }

        private HassiumObject connect(HassiumObject[] args)
        {
            Value.Connect(args[0].ToString(), args[1].HInt().Value);
            return null;
        }

        private HassiumObject connected(HassiumObject[] args)
        {
            return Value.Connected;
        }

        private HassiumObject getStream(HassiumObject[] args)
        {
            return new HassiumNetworkStream(Value.GetStream());
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }
    }
}

