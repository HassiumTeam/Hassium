using System.Net.Sockets;

namespace Hassium.HassiumObjects.Networking
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

