using System;
using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumBinaryReader: HassiumObject
    {
        public BinaryReader Value { get; private set; }

        public HassiumBinaryReader(BinaryReader value)
        {
            Value = value;
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("peekChar", new InternalFunction(peekChar, 0));
            Attributes.Add("read", new InternalFunction(read, 0));
            Attributes.Add("readBoolean", new InternalFunction(readBoolean, 0));
            Attributes.Add("readByte", new InternalFunction(readByte, 0));
            Attributes.Add("readString", new InternalFunction(readString, 0));
            Attributes.Add("readChars", new InternalFunction(readChars, 1));
            Attributes.Add("toString", new InternalFunction(toString, 0));
        }

        public HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        public HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();
            return null;
        }

        public HassiumObject peekChar(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(Value.PeekChar()));
        }

        public HassiumObject read(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(Value.Read()));
        }

        public HassiumObject readBoolean(HassiumObject[] args)
        {
            return new HassiumBool(Value.ReadBoolean());
        }

        public HassiumObject readByte(HassiumObject[] args)
        {
            return new HassiumByte(Value.ReadByte());
        }

        public HassiumObject readChars(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadChars(((HassiumInt)args[0])).ToString());
        }

        public HassiumObject readString(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadString());
        }

        public HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}

