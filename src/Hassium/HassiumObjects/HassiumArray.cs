using System;

namespace Hassium
{
    public class HassiumArray: HassiumObject
    {
        public HassiumObject[] Value { get; private set; }

        public HassiumArray(object[] value)
        {
            this.Value = value;
        }
    }
}

