using System;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumExceptionHandler: HassiumObject
    {
        public HassiumObject Method { get; private set; }
        public double Label { get; private set; }

        public HassiumExceptionHandler(HassiumObject method, double label)
        {
            Method = method;
            Label = label;
        }
    }
}

