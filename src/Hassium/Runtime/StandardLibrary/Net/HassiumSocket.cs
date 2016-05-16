using System;
using System.Net;
using System.Net.Sockets;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Net
{
    public class HassiumSocket: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Socket");

        public Socket Socket { get; set; }
        public HassiumSocket()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 3));
            AddType(HassiumSocket.TypeDefinition);
        }

        private HassiumSocket _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumSocket hassiumSocket = new HassiumSocket();

            AddressFamily addressFamily = parseEnum<AddressFamily>(HassiumString.Create(args[0]).Value);
            SocketType socketType = parseEnum<SocketType>(HassiumString.Create(args[1]).Value);
            ProtocolType protocolType = parseEnum<ProtocolType>(HassiumString.Create(args[2]).Value);

            hassiumSocket.Socket = new Socket(addressFamily, socketType, protocolType);
            hassiumSocket.Attributes.Add("available",   new HassiumProperty(hassiumSocket.get_Available));
            hassiumSocket.Attributes.Add("blocking",    new HassiumProperty(hassiumSocket.get_Blocking, hassiumSocket.set_Blocking));
            hassiumSocket.Attributes.Add("connected",   new HassiumProperty(hassiumSocket.get_Connected));
            hassiumSocket.Attributes.Add("noDelay",     new HassiumProperty(hassiumSocket.get_NoDelay, hassiumSocket.set_NoDelay));
            hassiumSocket.Attributes.Add("close",       new HassiumFunction(hassiumSocket.close, 0));
            hassiumSocket.Attributes.Add("connect",     new HassiumFunction(hassiumSocket.connect, 2));
            hassiumSocket.Attributes.Add("listen",      new HassiumFunction(hassiumSocket.listen, 1));
            hassiumSocket.Attributes.Add("send",        new HassiumFunction(hassiumSocket.send, 1));
            hassiumSocket.Attributes.Add("sendFile",    new HassiumFunction(hassiumSocket.sendFile, 1));

            return hassiumSocket;
        }

        public HassiumInt get_Available(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Socket.Available);
        }
        public HassiumBool get_Blocking(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Socket.Blocking);
        }
        public HassiumNull set_Blocking(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.Blocking = HassiumBool.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        public HassiumBool get_Connected(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Socket.Connected);
        }
        public HassiumBool get_NoDelay(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Socket.NoDelay);
        }
        public HassiumNull set_NoDelay(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.NoDelay = HassiumBool.Create(args[0]).Value;
            return HassiumObject.Null;
        }

        public HassiumNull close(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.Close();
            return HassiumObject.Null;
        }
        public HassiumNull connect(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.Connect(HassiumString.Create(args[0]).Value, (int)HassiumInt.Create(args[1]).Value);
            return HassiumObject.Null;
        }
        public HassiumNull listen(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.Listen((int)HassiumInt.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        public HassiumInt send(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = HassiumList.Create(args[0]);
            byte[] bytes = new byte[list.Value.Count];
            for (int i = 0; i < bytes.Length; i++)
            {
                if (list.Value[i] is HassiumInt)
                    bytes[i] = (byte)HassiumInt.Create(list.Value[i]).Value;
                else if (list.Value[i] is HassiumChar)
                    bytes[i] = (byte)HassiumChar.Create(list.Value[i]).Value;
                else
                    throw new InternalException("Cannot send " + bytes[i].GetType().Name);
            }
            return new HassiumInt(Socket.Send(bytes));
        }
        public HassiumNull sendFile(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.SendFile(HassiumString.Create(args[0]).Value);
            return HassiumObject.Null;
        }

        private static T parseEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }
    }
}

