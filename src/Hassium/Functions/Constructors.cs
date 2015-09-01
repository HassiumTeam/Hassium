using System;
using System.Net;
using Hassium.HassiumObjects;

namespace Hassium
{
    public class Constructors : ILibrary
    {
        [IntFunc("WebClient", true)]
        public static HassiumObject WebClient(HassiumObject[] args)
        {
            return new HassiumClient(new WebClient());
        }

        [IntFunc("Object", true)]
        public static HassiumObject Object(HassiumObject[] args)
        {
            return new HassiumObject();
        }

        [IntFunc("TcpClient", true)]
        public static HassiumObject TcpClient(HassiumObject[] args)
        {
            return new HassiumTcpClient(new System.Net.Sockets.TcpClient());
        }

        [IntFunc("Array", true)]
        public static HassiumObject Array(HassiumObject[] args)
        {
            return new HassiumArray(new HassiumObject[Convert.ToInt32(args[0].ToString())]);
        }

        [IntFunc("StreamWriter", true)]
        public static HassiumObject StreamWriter(HassiumObject[] args)
        {
            return new HassiumStreamWriter(new System.IO.StreamWriter(((HassiumNetworkStream)args[0]).Value));
        }

        [IntFunc("StreamReader", true)]
        public static HassiumObject StreamReader(HassiumObject[] args)
        {
            return new HassiumStreamReader(new System.IO.StreamReader(((HassiumNetworkStream)args[0]).Value));
        }

        [IntFunc("NetworkStream", true)]
        public static HassiumObject NetworkStream(HassiumObject[] args)
        {
            return new HassiumNetworkStream(new System.Net.Sockets.NetworkStream(((HassiumSocket)args[0]).Value));
        }
    }
}

