using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.List;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Networking.HTTP;
using Hassium.HassiumObjects.Random;
using Hassium.HassiumObjects.Text;
using Hassium.HassiumObjects.Types;

namespace Hassium.Functions
{
    public class Constructors : ILibrary
    {
        [IntFunc("WebClient", true)]
        public static HassiumObject WebClient(HassiumObject[] args)
        {
            return new HassiumWebClient(new WebClient());
        }

        [IntFunc("Object", true)]
        public static HassiumObject Object(HassiumObject[] args)
        {
            return new HassiumObject();
        }

        [IntFunc("Date", true)]
        public static HassiumObject Date(HassiumObject[] args)
        {
            return new HassiumDate(DateTime.Now);
        }

        [IntFunc("TcpClient", true)]
        public static HassiumObject TcpClient(HassiumObject[] args)
        {
            return new HassiumTcpClient(new TcpClient());
        }

        [IntFunc("Array", true)]
        public static HassiumObject Array(HassiumObject[] args)
        {
            return new HassiumArray(new HassiumObject[Convert.ToInt32(args[0].ToString())]);
        }

        [IntFunc("StreamWriter", true)]
        public static HassiumObject StreamWriter(HassiumObject[] args)
        {
            return new HassiumStreamWriter(new StreamWriter(((HassiumStream)args[0]).Value));
        }

        [IntFunc("StreamReader", true)]
        public static HassiumObject StreamReader(HassiumObject[] args)
        {
            return new HassiumStreamReader(new StreamReader(((HassiumStream)args[0]).Value));
        }

        [IntFunc("NetworkStream", true)]
        public static HassiumObject NetworkStream(HassiumObject[] args)
        {
            return new HassiumNetworkStream(new NetworkStream(((HassiumSocket)args[0]).Value));
        }

        [IntFunc("FileStream", true)]
        public static HassiumObject FileStream(HassiumObject[] args)
        {
            return new HassiumFileStream(new FileStream(args[0].HString().Value, FileMode.OpenOrCreate));
        }

        [IntFunc("StringBuilder", true)]
        public static HassiumObject StringBuilder(HassiumObject[] args)
        {
            return new HassiumStringBuilder(new StringBuilder());
        }

        [IntFunc("HttpListener", true)]
        public static HassiumObject HttpListener(HassiumObject[] args)
        {
            return new HassiumHttpListener(new HttpListener());
        }

        [IntFunc("BinaryWriter", true)]
        public static HassiumObject BinaryWriter(HassiumObject[] args)
        {
            return new HassiumBinaryWriter(new BinaryWriter(((HassiumStream)args[0]).Value));
        }

        [IntFunc("BinaryReader", true)]
        public static HassiumObject BinaryReader(HassiumObject[] args)
        {
            return new HassiumBinaryReader(new BinaryReader(((HassiumStream)args[0]).Value));
        }

        [IntFunc("Random", true)]
        public static HassiumObject Random(HassiumObject[] args)
        {
            return args.Length > 0 ? new HassiumRandom(new Random(((HassiumNumber)args[0]).ValueInt)) : new HassiumRandom(new Random());
        }

        [IntFunc("List", true)]
        public static HassiumObject List(HassiumObject[] args)
        {
            return new HassiumList(new List<HassiumObject>());
        }

        [IntFunc("Stack", true)]
        public static HassiumObject Stack(HassiumObject[] args)
        {
            return new HassiumStack(new Stack(((HassiumNumber)args[0]).ValueInt));
        }
    }
}

