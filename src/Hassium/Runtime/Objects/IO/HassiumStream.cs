using System;
using System.IO;
using System.Net.Security;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.IO
{
    public class HassiumStream: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Stream");
        public Stream Stream { get; private set; }
        public HassiumStream(Stream stream)
        {
            Stream = stream;
            AddType(HassiumStream.TypeDefinition);
            AddAttribute("length",            new HassiumProperty(get_length));
            AddAttribute("position",          new HassiumProperty(get_position, set_position));
            AddAttribute("authenticateSsl",     authenticateSsl, 1);
            AddAttribute("close",               close,      0);
            AddAttribute("flush",               flush,      0);
            AddAttribute("readByte",            readByte,   0);
            AddAttribute("readTo",              readTo,     1);
            AddAttribute("write",               write,      1);
            AddAttribute("writeByte",           writeByte,  1);
        }

        private HassiumInt get_length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Stream.Length);
        }
        private HassiumInt get_position(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Stream.Position);
        }
        private HassiumNull set_position(VirtualMachine vm, HassiumObject[] args)
        {
            Stream.Position = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }

        private HassiumNull authenticateSsl(VirtualMachine vm, HassiumObject[] args)
        {
            if (!(Stream is SslStream))
                throw new InternalException(vm, "Stream was not an SSL Stream!");
            ((SslStream)Stream).AuthenticateAsClient(args[0].ToString(vm).String);

            return HassiumObject.Null;
        }
        private HassiumNull close(VirtualMachine vm, HassiumObject[] args)
        {
            Stream.Close();
            return HassiumObject.Null;
        }
        private HassiumNull flush(VirtualMachine vm, HassiumObject[] args)
        {
            Stream.Flush();
            return HassiumObject.Null;
        }
        private HassiumChar readByte(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumChar((char)Stream.ReadByte());
        }
        private HassiumList readTo(VirtualMachine vm, HassiumObject[] args)
        {
            byte[] bytes = new byte[args[0].ToInt(vm).Int];
            Stream.Read(bytes, 0, (int)args[0].ToInt(vm).Int);
            HassiumList list = new HassiumList(new HassiumObject[0]);

            foreach (byte b in bytes)
                list.add(vm, new HassiumChar((char)b));

            return list;
        }
        private HassiumNull write(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = args[0].ToList(vm);
            byte[] bytes = new byte[list.List.Count];
            for (int i = 0; i < list.List.Count; i++)
                bytes[i] = (byte)list.List[i].ToChar(vm).Char;
            Stream.Write(bytes, 0, bytes.Length);

            return HassiumObject.Null;
        }
        private HassiumNull writeByte(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumChar)
                Stream.WriteByte((byte)args[0].ToChar(vm).Char);
            else
                Stream.WriteByte((byte)args[0].ToInt(vm).Int);

            return HassiumObject.Null;
        }

        public override HassiumObject Dispose(VirtualMachine vm, params HassiumObject[] args)
        {
            Stream.Dispose();
            return HassiumObject.Null;
        }
    }
}

