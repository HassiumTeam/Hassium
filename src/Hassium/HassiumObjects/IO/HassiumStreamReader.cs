using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumStreamReader : HassiumObject
    {
        public StreamReader Value { get; private set; }

        public HassiumStreamReader(StreamReader value)
        {
            Value = value;
            Attributes.Add("readLine", new InternalFunction(readLine, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("peek", new InternalFunction(peek, 0));
            Attributes.Add("read", new InternalFunction(read, 0));
            Attributes.Add("readToEnd", new InternalFunction(readToEnd, 0));
        }

        private HassiumObject readLine(HassiumObject[] args)
        {
            return Value.ReadLine();
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();
            return null;
        }

        private HassiumObject peek(HassiumObject[] args)
        {
            return new HassiumChar(((char) Value.Peek()));
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumChar(((char) Value.Read()));
        }

        private HassiumObject readToEnd(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadToEnd());
        }
    }
}