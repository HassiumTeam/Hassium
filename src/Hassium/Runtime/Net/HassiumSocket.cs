using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;

namespace Hassium.Runtime.Net
{
    public class HassiumSocket : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Socket");

        public TcpClient Client { get; set; }
        public BinaryReader Reader { get; set; }
        public BinaryWriter Writer { get; set; }

        public StreamReader StreamReader { get; set; }
        public StreamWriter StreamWriter { get; set; }

        public bool AutoFlush { get; set; }
        public bool Closed { get; set; }

        public HassiumSocket()
        {
            AddType(TypeDefinition);

            AddAttribute(INVOKE, _new, 0, 1, 2);
            ImportAttribs(this);
        }

        [FunctionAttribute("func new () : Socket", "func new (IPAddrOrStr : object) : Socket", "func new (ip : string, port : int) : Socket", "func new (ip : string, port : int, ssl : bool) : Socket")]
        public HassiumObject _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumSocket socket = new HassiumSocket();
            Stream stream = null;

            switch (args.Length)
            {
                case 0:
                    socket.Client = new TcpClient();
                    stream = new MemoryStream();
                    break;
                case 1:
                    if (args[0] is HassiumIPAddr)
                    {
                        var ipAddr = args[0] as HassiumIPAddr;
                        socket.Client = new TcpClient(ipAddr.Address.String, (int)ipAddr.Port.Int);
                        stream = socket.Client.GetStream();
                    }
                    else
                        return _new(vm, self, location, HassiumIPAddr.Attribs[INVOKE].Invoke(vm, location, args[0].ToString(vm, args[0], location)));
                    break;
                case 2:
                    socket.Client = new TcpClient(args[0].ToString(vm, args[0], location).String, (int)args[1].ToInt(vm, args[1], location).Int);
                    stream = socket.Client.GetStream();
                    break;
                case 3:
                    socket.Client = new TcpClient(args[0].ToString(vm, args[0], location).String, (int)args[1].ToInt(vm, args[1], location).Int);
                    if (args[2].ToBool(vm,args[2], location).Bool)
                        stream = new SslStream(socket.Client.GetStream(), false, new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true), null);
                    else
                        stream = socket.Client.GetStream();
                    break;
            }
            socket.Reader = new BinaryReader(stream);
            socket.Writer = new BinaryWriter(stream);
            socket.StreamReader = new StreamReader(stream);
            socket.StreamWriter = new StreamWriter(stream);

            ImportAttribs(socket);

            return socket;
        }

        public static void ImportAttribs(HassiumSocket socket)
        {
            socket.AutoFlush = true;
            socket.Closed = false;
            socket.AddAttribute("autoflush", new HassiumProperty(socket.get_autoflush, socket.set_autoflush));
            socket.AddAttribute("close", socket.close, 0);
            socket.AddAttribute("connect", socket.connect, 1, 2);
            socket.AddAttribute("fromip", new HassiumProperty(socket.get_fromip));
            socket.AddAttribute("toip", new HassiumProperty(socket.get_toip));
            socket.AddAttribute("flush", socket.flush, 0);
            socket.AddAttribute("isconnected", new HassiumProperty(socket.get_isconnected));
            socket.AddAttribute("readbyte", socket.readbyte, 0);
            socket.AddAttribute("readbytes", socket.readbytes, 1);
            socket.AddAttribute("readint", socket.readint, 0);
            socket.AddAttribute("readline", socket.readline, 0);
            socket.AddAttribute("readlong", socket.readlong, 0);
            socket.AddAttribute("readshort", socket.readshort, 0);
            socket.AddAttribute("readstring", socket.readstring, 0);
            socket.AddAttribute("writebyte", socket.writebyte, 1);
            socket.AddAttribute("writefloat", socket.writefloat, 1);
            socket.AddAttribute("writeint", socket.writeint, 1);
            socket.AddAttribute("writeline", socket.writeline, 1);
            socket.AddAttribute("writelist", socket.writelist, 1);
            socket.AddAttribute("writelong", socket.writelong, 1);
            socket.AddAttribute("writeshort", socket.writeshort, 1);
            socket.AddAttribute("writestring", socket.writestring, 1);
        }

        [FunctionAttribute("autoflush { get; }")]
        public HassiumBool get_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(AutoFlush);
        }

        [FunctionAttribute("autofluah { set; }")]
        public HassiumNull set_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            AutoFlush = args[0].ToBool(vm, args[0], location).Bool;

            return Null;
        }

        [FunctionAttribute("func close () : null")]
        public HassiumNull close(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Client.Close();
            return Null;
        }

        [FunctionAttribute("func connect (IPAddrOrStr : object) : null", "func connect (ip : string, port : int) : null")]
        public HassiumNull connect(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 1:
                    if (args[0] is HassiumIPAddr)
                    {
                        var ipAddr = args[0] as HassiumIPAddr;
                        Client = new TcpClient(ipAddr.Address.String, (int)ipAddr.Port.Int);
                        return Null;
                    }
                    else
                        return connect(vm, self, location, HassiumIPAddr.Attribs[INVOKE].Invoke(vm, location, args[0].ToString(vm, args[0], location)));
                case 2:
                    Client = new TcpClient(args[0].ToString(vm, args[0], location).String, (int)args[1].ToInt(vm, args[1], location).Int);
                    return Null;
            }
            return Null;
        }

        [FunctionAttribute("fromip { get; }")]
        public HassiumObject get_fromip(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }
            string[] parts = (Client.Client.LocalEndPoint as IPEndPoint).ToString().Split(':');
            return HassiumIPAddr.Attribs[INVOKE].Invoke(vm, location, new HassiumString(parts[0]), new HassiumString(parts[1]));
        }

        [FunctionAttribute("toip { get; }")]
        public HassiumObject get_toip(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }
            string[] parts = (Client.Client.RemoteEndPoint as IPEndPoint).ToString().Split(':');
            return HassiumIPAddr.Attribs[INVOKE].Invoke(vm, location, new HassiumString(parts[0]), new HassiumString(parts[1]));
        }

        [FunctionAttribute("func flush () : null")]
        public HassiumNull flush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            Writer.Flush();
            return Null;
        }

        [FunctionAttribute("isconnected { get; }")]
        public HassiumBool get_isconnected(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Client.Connected);
        }

        [FunctionAttribute("func readbyte () : char")]
        public HassiumObject readbyte(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {

            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            return new HassiumChar((char)Reader.ReadBytes(1)[0]);
        }

        [FunctionAttribute("func readbytes (count : int) : list")]
        public HassiumObject readbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            HassiumList list = new HassiumList(new HassiumObject[0]);
            int count = (int)args[0].ToInt(vm, args[0], location).Int;
            for (int i = 0; i < count; i++)
                HassiumList.add(vm, list, location, new HassiumChar((char)Reader.ReadBytes(1)[0]));

            return list;
        }

        [FunctionAttribute("func readfloat () : float")]
        public HassiumObject readfloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {

            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            return new HassiumFloat(Reader.ReadDouble());
        }

        [FunctionAttribute("func readint () : int")]
        public HassiumObject readint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {

            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            return new HassiumInt(Reader.ReadInt32());
        }

        [FunctionAttribute("func readline () : string")]
        public HassiumObject readline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {

            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            return new HassiumString(StreamReader.ReadLine());
        }

        [FunctionAttribute("func readlong () : int")]
        public HassiumObject readlong(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            return new HassiumInt(Reader.ReadInt64());
        }

        [FunctionAttribute("func readshort () : int")]
        public HassiumObject readshort(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {

            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            return new HassiumInt(Reader.ReadInt16());
        }

        [FunctionAttribute("func readstring () : string")]
        public HassiumObject readstring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            return new HassiumString(Reader.ReadString());
        }

        [FunctionAttribute("func writebyte (b : char) : null")]
        public HassiumNull writebyte(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            Writer.Write((byte)args[0].ToChar(vm, args[0], location).Char);
            if (AutoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writefloat (f : float) : null")]
        public HassiumNull writefloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            Writer.Write(args[0].ToFloat(vm, args[0], location).Float);
            if (AutoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writeint (i : int) : null")]
        public HassiumNull writeint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            Writer.Write((int)args[0].ToInt(vm, args[0], location).Int);
            if (AutoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writeline (str : string) : null")]
        public HassiumNull writeline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            string str = args[0].ToString(vm, args[0], location).String;

            foreach (var c in str)
                Writer.Write(c);
            Writer.Write('\r');
            Writer.Write('\n');

            if (AutoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writelist (l : list) : null")]
        public HassiumNull writelist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            foreach (var i in args[0].ToList(vm, args[0], location).Values)
                writeHassiumObject(Writer, i, vm, location);

            return Null;
        }

        [FunctionAttribute("func writelong (l : int) : null")]
        public HassiumNull writelong(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            Writer.Write(args[0].ToInt(vm, args[0], location).Int);
            if (AutoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writeshort (s : int) : null")]
        public HassiumNull writeshort(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            Writer.Write((short)args[0].ToInt(vm, args[0], location).Int);
            if (AutoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writestring (str : string) : null")]
        public HassiumNull writestring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (Closed)
            {
                vm.RaiseException(HassiumSocketClosedException.Attribs[INVOKE].Invoke(vm, location, this));
                return Null;
            }

            Writer.Write(args[0].ToString(vm, args[0], location).String);
            if (AutoFlush)
                Writer.Flush();
            return Null;
        }

        private void writeHassiumObject(BinaryWriter writer, HassiumObject obj, VirtualMachine vm, SourceLocation location)
        {
            var type = obj.Type();

            if (type == HassiumBool.TypeDefinition)
                writer.Write(obj.ToBool(vm, obj, location).Bool);
            else if (type == HassiumChar.TypeDefinition)
                writer.Write((byte)obj.ToChar(vm, obj, location).Char);
            else if (type == HassiumFloat.TypeDefinition)
                writer.Write(obj.ToFloat(vm, obj, location).Float);
            else if (type == HassiumInt.TypeDefinition)
                writer.Write(obj.ToInt(vm, obj, location).Int);
            else if (type == HassiumList.TypeDefinition)
                foreach (var item in obj.ToList(vm, obj, location).Values)
                    writeHassiumObject(writer, item, vm, location);
            else if (type == HassiumString.TypeDefinition)
                writer.Write(obj.ToString(vm, obj, location).String);
            else if (type == HassiumTuple.TypeDefinition)
                foreach (var item in obj.ToTuple(vm, obj, location).Values)
                    writeHassiumObject(writer, item, vm, location);
        }
    }
}
