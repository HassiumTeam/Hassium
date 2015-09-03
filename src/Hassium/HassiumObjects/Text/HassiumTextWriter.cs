using System;
using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumTextWriter: HassiumObject
    {
        public TextWriter Value { get; private set; }

        public HassiumTextWriter(TextWriter value)
        {
            this.Value = value;
            this.Attributes.Add("close", new InternalFunction(close));
            this.Attributes.Add("dispose", new InternalFunction(dispose));
            this.Attributes.Add("flush", new InternalFunction(flush));
            this.Attributes.Add("write", new InternalFunction(write));
            this.Attributes.Add("writeLine", new InternalFunction(writeLine));
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

        private HassiumObject flush(HassiumObject[] args)
        {
            this.Value.Flush();
            return null;
        }

        private HassiumObject write(HassiumObject[] args)
        {
            this.Value.Write(args[0].ToString());
            return null;
        }

        private HassiumObject writeLine(HassiumObject[] args)
        {
            this.Value.WriteLine(args[0].ToString());
            return null;
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ToString());
        }
    }
}

