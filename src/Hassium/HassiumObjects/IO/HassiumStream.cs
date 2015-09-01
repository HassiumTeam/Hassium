

using System.IO;

namespace Hassium.HassiumObjects.IO
{
    public abstract class HassiumStream: HassiumObject
    {
        public Stream Value { get; protected set; }

        protected HassiumStream(Stream value)
        {
            this.Value = value;
            Attributes.Add("length", new InternalFunction(x => Value.Length, true));
            Attributes.Add("position", new InternalFunction(x => Value.Position, true));
            Attributes.Add("flush", new InternalFunction(Flush));
            Attributes.Add("close", new InternalFunction(Close));
            Attributes.Add("read", new InternalFunction(Read));
            Attributes.Add("write", new InternalFunction(Close));
        }

        public HassiumObject Flush(HassiumObject[] args)
        {
            Value.Flush();
            return null;
        }

        public HassiumObject Close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        public HassiumObject Read(HassiumObject[] args)
        {
            return Value.ReadByte();
        }

        public HassiumObject Write(HassiumObject[] args)
        {
            Value.WriteByte((byte)args[0].HNum().ValueInt);
            return null;
        }
    }
}

