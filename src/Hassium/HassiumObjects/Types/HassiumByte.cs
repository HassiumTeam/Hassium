using System;
using Hassium.HassiumObjects;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumByte: HassiumObject
    {
        public byte Value { get; private set; }

        public HassiumByte(byte value)
        {
            this.Value = value;
        }
    }
}

