using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Collections;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Networking.HTTP;
using Hassium.HassiumObjects.Random;
using Hassium.HassiumObjects.Text;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;

namespace Hassium.Functions
{
    public class Constructors : ILibrary
    {
        [IntFunc("WebClient", true, 0)]
        public static HassiumObject WebClient(HassiumObject[] args)
        {
            return new HassiumWebClient(new WebClient());
        }

        [IntFunc("Object", true, 0)]
        public static HassiumObject Object(HassiumObject[] args)
        {
            return new HassiumObject();
        }

        [IntFunc("Date", true, 0)]
        public static HassiumObject Date(HassiumObject[] args)
        {
            return new HassiumDate(DateTime.Now);
        }

        [IntFunc("TcpClient", true, 0)]
        public static HassiumObject TcpClient(HassiumObject[] args)
        {
            return new HassiumTcpClient(new TcpClient());
        }

        [IntFunc("Array", true, 1)]
        public static HassiumObject Array(HassiumObject[] args)
        {
            return new HassiumArray(new HassiumObject[args[0].HInt().Value]);
        }

        [IntFunc("StreamWriter", true, 1)]
        public static HassiumObject StreamWriter(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumStreamWriter(new StreamWriter(args[0].HString().Value));
            return new HassiumStreamWriter(new StreamWriter(((HassiumStream)args[0]).Value));
        }

        [IntFunc("StreamReader", true, 1)]
        public static HassiumObject StreamReader(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumStreamReader(new StreamReader(args[0].HString().Value));
            return new HassiumStreamReader(new StreamReader(((HassiumStream)args[0]).Value));
        }

        [IntFunc("NetworkStream", true, 1)]
        public static HassiumObject NetworkStream(HassiumObject[] args)
        {
            return new HassiumNetworkStream(new NetworkStream(((HassiumSocket)args[0]).Value));
        }

        [IntFunc("FileStream", true, 1)]
        public static HassiumObject FileStream(HassiumObject[] args)
        {
            return new HassiumFileStream(new FileStream(args[0].HString().Value, FileMode.OpenOrCreate));
        }

        [IntFunc("StringBuilder", true, 0)]
        public static HassiumObject StringBuilder(HassiumObject[] args)
        {
            return new HassiumStringBuilder(new StringBuilder());
        }

        [IntFunc("HttpListener", true, 0)]
        public static HassiumObject HttpListener(HassiumObject[] args)
        {
            return new HassiumHttpListener(new HttpListener());
        }

        [IntFunc("BinaryWriter", true, 1)]
        public static HassiumObject BinaryWriter(HassiumObject[] args)
        {
            return new HassiumBinaryWriter(new BinaryWriter(((HassiumStream)args[0]).Value));
        }

        [IntFunc("BinaryReader", true, 1)]
        public static HassiumObject BinaryReader(HassiumObject[] args)
        {
            return new HassiumBinaryReader(new BinaryReader(((HassiumStream)args[0]).Value));
        }

        [IntFunc("Random", true, new []{0, 1})]
        public static HassiumObject Random(HassiumObject[] args)
        {
            return args.Length > 0 ? new HassiumRandom(new Random(args[0].HInt().Value)) : new HassiumRandom(new Random());
        }

        [IntFunc("Stack", true, 1)]
        public static HassiumObject Stack(HassiumObject[] args)
        {
            return new HassiumStack(new Stack<HassiumObject>(args[0].HInt().Value));
        }

        [IntFunc("List", true, 0)]
        public static HassiumObject List(HassiumObject[] args)
        {
            return new HassiumList();
        }
       
        [IntFunc("Event", true, -1)]
        public static HassiumObject Event(HassiumObject[] args)
        {
            var ret = new HassiumEvent();
            if (args.Length > 0)
            {
                args.All(x =>
                {
                    if (x is HassiumMethod) ret.AddHandler((HassiumMethod) x);
                    return true;
                });
            }
            return ret;
        }
    }
}

