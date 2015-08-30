using System;

namespace Hassium
{
    public class HassiumBool: HassiumObject
    {
        public bool Value { get; private set; }

        public HassiumBool(Boolean value)
        {
            this.Value = value;
        }
    }
}

