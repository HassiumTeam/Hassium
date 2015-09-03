using System;
using System.IO;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumBinaryWriter: HassiumObject
    {
        public BinaryWriter Value { get; private set; }

        public HassiumBinaryWriter(BinaryWriter value)
        {
            this.Value = value;
            this.Attributes.Add("close", new InternalFunction(close));
            this.Attributes.Add("dispose", new InternalFunction(dispose));
            this.Attributes.Add("flush", new InternalFunction(flush));
            this.Attributes.Add("write", new InternalFunction(write));
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
    }
}

