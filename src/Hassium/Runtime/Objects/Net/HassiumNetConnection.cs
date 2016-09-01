using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;

using Hassium.Runtime.Objects.IO;
using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Net
{
    public class HassiumNetConnection: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("NetConnection");

        public TcpClient TcpClient { get; set; }

        public HassiumNetConnection()
        {
            AddType(HassiumNetConnection.TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 2);
        }

        public static HassiumNetConnection CreateFromTcpClient(TcpClient client)
        {
            HassiumNetConnection netConnection = new HassiumNetConnection();

            netConnection.TcpClient = client;
            netConnection.AddAttribute(HassiumObject.DISPOSE, netConnection.Dispose, 0);
            netConnection.AddAttribute("close",        netConnection.close,                      0);
            netConnection.AddAttribute("connected",    new HassiumProperty(netConnection.connected));
            netConnection.AddAttribute("getStream",    netConnection.getStream, new int[] { 0, 1 });

            return netConnection;
        }

        public HassiumNetConnection _new(VirtualMachine vm, HassiumObject[] args)
        {
            var netConnection = CreateFromTcpClient(null);
            netConnection.TcpClient = new TcpClient(args[0].ToString(vm).String, (int)args[1].ToInt(vm).Int);
            return netConnection;
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
            if (args[0].ToBool(vm).Bool)
                return new HassiumStream(new SslStream(TcpClient.GetStream(), false, new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true), null));
            return new HassiumStream(TcpClient.GetStream());
        }

        public override HassiumObject Dispose(VirtualMachine vm, params HassiumObject[] args)
        {
            TcpClient.Close();
            return HassiumObject.Null;
        }
    }
}