using System;
using System.IO;
using Hassium.HassiumObjects;

namespace Hassium   
{
    public class HassiumStreamReader: HassiumObject
    {
        public StreamReader Value { get; private set; }

        public HassiumStreamReader(StreamReader value)
        {
            this.Value = value;
            this.Attributes.Add("readLine", new InternalFunction(readLine));
            this.Attributes.Add("dispose", new InternalFunction(dispose));
            this.Attributes.Add("close", new InternalFunction(close));
            this.Attributes.Add("peek", new InternalFunction(peek));
            this.Attributes.Add("read", new InternalFunction(read));
            this.Attributes.Add("readToEnd", new InternalFunction(readToEnd));
        }

        private HassiumObject readLine(HassiumObject[] args)
        {
            return this.Value.ReadLine();
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

        private HassiumObject peek(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToChar(this.Value.Peek()).ToString());
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToChar(this.Value.Read()).ToString());
        }

        private HassiumObject readToEnd(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ReadToEnd());
        }
    }
}

