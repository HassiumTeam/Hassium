using System;
using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumBinaryReader: HassiumObject
    {
        public BinaryReader Value { get; private set; }

        public HassiumBinaryReader(BinaryReader value)
        {
            this.Value = value;
            this.Attributes.Add("close", new InternalFunction(close));
            this.Attributes.Add("dispose", new InternalFunction(dispose));
            this.Attributes.Add("peekChar", new InternalFunction(peekChar));
            this.Attributes.Add("read", new InternalFunction(read));
            this.Attributes.Add("readBoolean", new InternalFunction(readBoolean));
            this.Attributes.Add("readByte", new InternalFunction(readByte));
            this.Attributes.Add("readString", new InternalFunction(readString));
            this.Attributes.Add("readChars", new InternalFunction(readChars));
            this.Attributes.Add("toString", new InternalFunction(toString));
        }

        public HassiumObject close(HassiumObject[] args)
        {
            this.Value.Close();
            return null;
        }

        public HassiumObject dispose(HassiumObject[] args)
        {
            this.Value.Dispose();
            return null;
        }

        public HassiumObject peekChar(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(this.Value.PeekChar()));
        }

        public HassiumObject read(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(this.Value.Read()));
        }

        public HassiumObject readBoolean(HassiumObject[] args)
        {
            return new HassiumBool(this.Value.ReadBoolean());
        }

        public HassiumObject readByte(HassiumObject[] args)
        {
            return new HassiumByte(this.Value.ReadByte());
        }

        public HassiumObject readChars(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ReadChars(((HassiumNumber)args[0])).ToString());
        }

        public HassiumObject readString(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ReadString());
        }

        public HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ToString());
        }
    }
}

