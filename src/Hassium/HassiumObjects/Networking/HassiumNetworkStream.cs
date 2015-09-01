using System;
using System.Net;
using System.Net.Sockets;
using Hassium.HassiumObjects;

namespace Hassium
{
    public class HassiumNetworkStream: HassiumObject
    {
        public NetworkStream Value { get; private set; }

        public HassiumNetworkStream(NetworkStream value)
        {
            this.Value = value;
        }
    }
}

