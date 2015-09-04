using System.IO;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumBinaryWriter: HassiumObject
    {
        public BinaryWriter Value { get; private set; }

        public HassiumBinaryWriter(BinaryWriter value)
        {
            Value = value;
            Attributes.Add("close", new InternalFunction(close));
            Attributes.Add("dispose", new InternalFunction(dispose));
            Attributes.Add("flush", new InternalFunction(flush));
            Attributes.Add("write", new InternalFunction(write));
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

        private HassiumObject flush(HassiumObject[] args)
        {
            Value.Flush();
            return null;
        }

        private HassiumObject write(HassiumObject[] args)
        {
            Value.Write(args[0].ToString());
            return null;
        }
    }
}

