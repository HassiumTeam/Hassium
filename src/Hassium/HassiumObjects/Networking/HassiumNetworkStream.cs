using System.IO;
using System.Net.Sockets;
using Hassium.HassiumObjects.IO;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumNetworkStream: HassiumStream
    {
        public NetworkStream Value
        {
            get { return (NetworkStream) base.Value; }
            set { base.Value = value; }
        }

        public HassiumNetworkStream(NetworkStream s) : base(s)
        {
            this.Value = s;
        }

    }
}

