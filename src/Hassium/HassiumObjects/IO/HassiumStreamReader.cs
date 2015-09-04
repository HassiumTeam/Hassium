using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.IO   
{
    public class HassiumStreamReader: HassiumObject
    {
        public StreamReader Value { get; private set; }

        public HassiumStreamReader(StreamReader value)
        {
            Value = value;
            Attributes.Add("readLine", new InternalFunction(readLine));
            Attributes.Add("dispose", new InternalFunction(dispose));
            Attributes.Add("close", new InternalFunction(close));
            Attributes.Add("peek", new InternalFunction(peek));
            Attributes.Add("read", new InternalFunction(read));
            Attributes.Add("readToEnd", new InternalFunction(readToEnd));
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
            return new HassiumChar(((char)Value.Peek()));
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumChar(((char)Value.Read()));
        }

        private HassiumObject readToEnd(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadToEnd());
        }
    }
}

