using System.IO;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumStreamWriter: HassiumObject
    {
        public StreamWriter Value { get; private set; }

        public HassiumStreamWriter(StreamWriter value)
        {
            this.Value = value;
            this.Attributes.Add("write", new InternalFunction(write));
            this.Attributes.Add("flush", new InternalFunction(flush));
            this.Attributes.Add("close", new InternalFunction(close));
            this.Attributes.Add("dispose", new InternalFunction(dispose));
            this.Attributes.Add("writeLine", new InternalFunction(writeLine));
        }

        private HassiumObject write(HassiumObject[] args)
        {
            this.Value.Write(args[0].ToString());
            return null;
        }

        private HassiumObject flush(HassiumObject[] args)
        {
            this.Value.Flush();
            return null;
        }

        private HassiumObject close(HassiumObject[] args)
        {
            this.Value.Close();
            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            this.Value.Dispose();
            return null;
        }

        private HassiumObject writeLine(HassiumObject[] args)
        {
            this.Value.WriteLine(args[0].ToString());
            return null;
        }
    }
}

