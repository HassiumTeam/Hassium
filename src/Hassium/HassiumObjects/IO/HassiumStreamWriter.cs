using System.IO;
using Hassium.Functions;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumStreamWriter: HassiumObject
    {
        public StreamWriter Value { get; private set; }

        public HassiumStreamWriter(StreamWriter value)
        {
            Value = value;
            Attributes.Add("write", new InternalFunction(write, 1));
            Attributes.Add("flush", new InternalFunction(flush, 0));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("writeLine", new InternalFunction(writeLine, 1));
        }

        private HassiumObject write(HassiumObject[] args)
        {
            Value.Write(args[0].ToString());
            return null;
        }

        private HassiumObject flush(HassiumObject[] args)
        {
            Value.Flush();
            return null;
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

        private HassiumObject writeLine(HassiumObject[] args)
        {
            Value.WriteLine(args[0].ToString());
            return null;
        }
    }
}

