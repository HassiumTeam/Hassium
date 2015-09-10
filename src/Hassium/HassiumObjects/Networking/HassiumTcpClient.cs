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
            Attributes.Add("available", new InternalFunction(x => value.Available, 0, true));
            Attributes.Add("connect", new InternalFunction(connect, 2));
            Attributes.Add("connected", new InternalFunction(x => value.Connected, 0, true));
            Attributes.Add("getStream", new InternalFunction(getStream, 0));
            Attributes.Add("close", new InternalFunction(close, 0));
        }

        private HassiumObject connect(HassiumObject[] args)
        {
            Value.Connect(args[0].ToString(), args[1].HInt().Value);
            return null;
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

