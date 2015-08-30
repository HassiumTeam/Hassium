using System;

namespace Hassium
{
    public class HassiumNumber: HassiumObject
    {
        public double Value { get; set; }

        public HassiumNumber(double value)
        {
            this.Value = value;
        }
    }
}

