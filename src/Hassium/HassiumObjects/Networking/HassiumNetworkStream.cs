using System.IO;
using System.Net.Sockets;
using Hassium.HassiumObjects.IO;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumNetworkStream : HassiumStream
    {
        public new NetworkStream Value
        {
            get { return (NetworkStream) base.Value; }
            set { base.Value = value; }
        }

        public HassiumNetworkStream(Stream s) : base(s)
        {
        }
    }
}