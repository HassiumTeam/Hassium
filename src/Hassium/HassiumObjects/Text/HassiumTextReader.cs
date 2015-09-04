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
            Attributes.Add("close", new InternalFunction(close));
            Attributes.Add("dispose", new InternalFunction(dispose));
            Attributes.Add("peek", new InternalFunction(peek));
            Attributes.Add("read", new InternalFunction(read));
            Attributes.Add("readLine", new InternalFunction(readLine));
            Attributes.Add("readToEnd", new InternalFunction(readToEnd));
            Attributes.Add("toString", new InternalFunction(toString));
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

