using System.IO;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumStream : HassiumObject
    {
        public Stream Value { get; protected set; }


        public HassiumStream(Stream value)
        {
            Value = value;
            Attributes.Add("length", new InternalFunction(x => Value.Length, 0, true));
            Attributes.Add("position", new InternalFunction(x => Value.Position, 0, true));
            Attributes.Add("canRead", new InternalFunction(x => Value.CanRead, 0, true));
            Attributes.Add("canWrite", new InternalFunction(x => Value.CanWrite, 0, true));
            Attributes.Add("canSeek", new InternalFunction(x => Value.CanSeek, 0, true));
            Attributes.Add("flush", new InternalFunction(Flush, 0));
            Attributes.Add("close", new InternalFunction(Close, 0));
            Attributes.Add("read", new InternalFunction(Read, 0));
            Attributes.Add("seek", new InternalFunction(Seek, new[] {0, 1}));
            Attributes.Add("write", new InternalFunction(Write, 1));
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

        public HassiumObject Seek(HassiumObject[] args)
        {
            return Value.Seek(args.Length == 1 ? args[0].HInt().Value : 0, SeekOrigin.Begin);
        }

        public HassiumObject Write(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
            {
                var buffer = args[0].ToString().ToCharArray().Select(x => (byte) x).ToArray();
                Value.Write(buffer, 0, buffer.Length);
            }
            else Value.WriteByte((byte) args[0].HDouble().ValueInt);
            return null;
        }
    }
}