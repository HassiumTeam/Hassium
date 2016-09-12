using System;
using System.Net;
using System.Net.Sockets;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Net
{
    public class HassiumSocket: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Socket");

        public Socket Socket { get; set; }

        public HassiumSocket()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, new HassiumFunction(_new, 3));
        }

        public HassiumSocket _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumSocket socket = new HassiumSocket();

            AddressFamily addressFamily = parseEnum<AddressFamily>(args[0].ToString(vm).String);
            SocketType socketType = parseEnum<SocketType>(args[1].ToString(vm).String);
            ProtocolType protocolType = parseEnum<ProtocolType>(args[2].ToString(vm).String);

            socket.Socket = new Socket(addressFamily, socketType, protocolType);
            socket.AddAttribute("available",   new HassiumProperty(socket.get_Available));
            socket.AddAttribute("blocking",    new HassiumProperty(socket.get_Blocking, socket.set_Blocking));
            socket.AddAttribute("connected",   new HassiumProperty(socket.get_Connected));
            socket.AddAttribute("noDelay",     new HassiumProperty(socket.get_NoDelay, socket.set_NoDelay));
            socket.AddAttribute("close",       new HassiumFunction(socket.close, 0));
            socket.AddAttribute("connect",     new HassiumFunction(socket.connect, 2));
            socket.AddAttribute("listen",      new HassiumFunction(socket.listen, 1));
            socket.AddAttribute("send",        new HassiumFunction(socket.send, 1));
            socket.AddAttribute("sendFile",    new HassiumFunction(socket.sendFile, 1));

            return socket;
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
            Socket.Blocking = args[0].ToBool(vm).Bool;
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
            Socket.NoDelay = args[0].ToBool(vm).Bool;
            return HassiumObject.Null;
        }

        public HassiumNull close(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.Close();
            return HassiumObject.Null;
        }
        public HassiumNull connect(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.Connect(args[0].ToString(vm).String, (int)args[1].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        public HassiumNull listen(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.Listen((int)args[0].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        public HassiumInt send(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = args[0].ToList(vm);
            byte[] bytes = new byte[list.List.Count];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)list.List[i].ToChar(vm).Char;
            return new HassiumInt(Socket.Send(bytes));
        }
        public HassiumNull sendFile(VirtualMachine vm, HassiumObject[] args)
        {
            Socket.SendFile(args[0].ToString(vm).String);
            return HassiumObject.Null;
        }

        public override HassiumObject Dispose(VirtualMachine vm, params HassiumObject[] args)
        {
            Socket.Dispose();
            return HassiumObject.Null;
        }

        private static T parseEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }
    }
}
