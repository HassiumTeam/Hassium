using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;

using Hassium.Runtime.StandardLibrary.IO;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Net
{
    public class HassiumNetConnection: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("NetConnection");

        public TcpClient TcpClient { get; set; }

        public HassiumNetConnection()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 2));
            AddType(HassiumNetConnection.TypeDefinition);
        }

        public static HassiumNetConnection CreateFromTcpClient(TcpClient client)
        {
            HassiumNetConnection hassiumNetConnection = new HassiumNetConnection();

            hassiumNetConnection.TcpClient = client;
            hassiumNetConnection.Attributes.Add("close",        new HassiumFunction(hassiumNetConnection.close, 0));
            hassiumNetConnection.Attributes.Add("connected",    new HassiumProperty(hassiumNetConnection.connected));
            hassiumNetConnection.Attributes.Add("getStream",    new HassiumFunction(hassiumNetConnection.getStream, new int[] { 0, 1 }));

            return hassiumNetConnection;
        }

        private HassiumNetConnection _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumNetConnection hassiumNetConnection = new HassiumNetConnection();

            hassiumNetConnection.TcpClient = new TcpClient(HassiumString.Create(args[0]).Value, (int)HassiumInt.Create(args[1]).Value);

            hassiumNetConnection.Attributes.Add("close", new HassiumFunction(hassiumNetConnection.close, 0));
            hassiumNetConnection.Attributes.Add("getStream", new HassiumFunction(hassiumNetConnection.getStream, new int[] { 0, 1 }));
            hassiumNetConnection.Attributes.Add("connected", new HassiumProperty(hassiumNetConnection.connected));

            return hassiumNetConnection;
        }

        public HassiumNull close(VirtualMachine vm, HassiumObject[] args)
        {
            TcpClient.Close();
            return HassiumObject.Null;
        }
        public HassiumBool connected(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(TcpClient.Connected);
        }
        public HassiumStream getStream(VirtualMachine vm, HassiumObject[] args)
        {
            if (args.Length == 1)
            if (HassiumBool.Create(args[0]).Value)
                return new HassiumStream(new SslStream(TcpClient.GetStream(), false, new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true), null));
            return new HassiumStream(TcpClient.GetStream());
        }
    }
}