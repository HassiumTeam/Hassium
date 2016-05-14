using System;
using System.IO;
using System.Net.Security;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.IO
{
    public class HassiumStream: HassiumObject
    {
        public Stream Stream { get; private set; }
        public HassiumStream(Stream stream)
        {
            Stream = stream;
            Attributes.Add("length",            new HassiumProperty(get_Length));
            Attributes.Add("position",          new HassiumProperty(get_Position, set_Position));
            Attributes.Add("authenticateSsl",   new HassiumFunction(authenticateSsl, 1));
            Attributes.Add("close",             new HassiumFunction(close, 0));
            Attributes.Add("flush",             new HassiumFunction(flush, 0));
            Attributes.Add("readByte",          new HassiumFunction(readByte, 0));
            Attributes.Add("readTo",            new HassiumFunction(readTo, 1));
            Attributes.Add("write",             new HassiumFunction(write, 1));
            Attributes.Add("writeByte",         new HassiumFunction(writeByte, 1));
            AddType("Stream");
        }
            
        private HassiumInt get_Length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Stream.Length);
        }
        private HassiumInt get_Position(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Stream.Position);
        }
        private HassiumNull set_Position(VirtualMachine vm, HassiumObject[] args)
        {
            Stream.Position = (int)HassiumInt.Create(args[0]).Value;
            return HassiumObject.Null;
        }

        private HassiumNull authenticateSsl(VirtualMachine vm, HassiumObject[] args)
        {
            if (!(Stream is SslStream))
                throw new InternalException("Stream was not an SSL Stream!");
            ((SslStream)Stream).AuthenticateAsClient(HassiumString.Create(args[0]).Value);

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
            byte[] bytes = new byte[HassiumInt.Create(args[0]).Value];
            Stream.Read(bytes, 0, (int)HassiumInt.Create(args[0]).Value);
            HassiumList list = new HassiumList(new HassiumObject[0]);

            foreach (byte b in bytes)
                list.Value.Add(new HassiumChar((char)b));

            return list;
        }
        private HassiumNull write(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = HassiumList.Create(args[0]);
            byte[] bytes = new byte[list.Value.Count];
            for (int i = 0; i < list.Value.Count; i++)
                bytes[i] = (byte)HassiumChar.Create(list.Value[i]).Value;
            Stream.Write(bytes, 0, bytes.Length);

            return HassiumObject.Null;
        }
        private HassiumNull writeByte(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumChar)
                Stream.WriteByte((byte)HassiumChar.Create(args[0]).Value);
            else
                Stream.WriteByte((byte)(int)HassiumInt.Create(args[0]).Value);

            return HassiumObject.Null;
        }
    }
}