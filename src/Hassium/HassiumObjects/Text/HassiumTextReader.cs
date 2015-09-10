using System;
using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumTextReader: HassiumObject
    {
        public TextReader Value { get; private set; }

        public HassiumTextReader(TextReader value)
        {
            Value = value;
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("peek", new InternalFunction(peek, 0));
            Attributes.Add("read", new InternalFunction(read, 0));
            Attributes.Add("readLine", new InternalFunction(readLine, 0));
            Attributes.Add("readToEnd", new InternalFunction(readToEnd, 0));
            Attributes.Add("toString", new InternalFunction(toString, 0));
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
            return new HassiumString(Convert.ToString(((char)Value.Peek())));
        }

        private HassiumObject read(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(((char)Value.Read())));
        }

        private HassiumObject readLine(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadLine());
        }

        private HassiumObject readToEnd(HassiumObject[] args)
        {
            return new HassiumString(Value.ReadToEnd());
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}

