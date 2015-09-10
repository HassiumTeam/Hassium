﻿using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumTextWriter: HassiumObject
    {
        public TextWriter Value { get; private set; }

        public HassiumTextWriter(TextWriter value)
        {
            Value = value;
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("flush", new InternalFunction(flush, 0));
            Attributes.Add("write", new InternalFunction(write, 1));
            Attributes.Add("writeLine", new InternalFunction(writeLine, 1));
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

        private HassiumObject writeLine(HassiumObject[] args)
        {
            Value.WriteLine(args[0].ToString());
            return null;
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}

