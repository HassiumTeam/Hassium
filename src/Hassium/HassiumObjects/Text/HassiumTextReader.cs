using System;
using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumTextReader: HassiumObject
    {
        public TextReader Value { get; private set; }

        public HassiumTextReader(TextReader value)
        {
            this.Value = value;
            this.Attributes.Add("close", new InternalFunction(close));
            this.Attributes.Add("dispose", new InternalFunction(dispose));
            this.Attributes.Add("peek", new InternalFunction(peek));
            this.Attributes.Add("read", new InternalFunction(read));
            this.Attributes.Add("readLine", new InternalFunction(readLine));
            this.Attributes.Add("readToEnd", new InternalFunction(readToEnd));
            this.Attributes.Add("toString", new InternalFunction(toString));
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
            return new HassiumString(Convert.ToString(((char)this.Value.Peek())));
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(((char)this.Value.Read())));
        }

        private HassiumObject readLine(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ReadLine());
        }

        private HassiumObject readToEnd(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ReadToEnd());
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ToString());
        }
    }
}

