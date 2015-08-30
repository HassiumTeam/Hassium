using System;
using System.Collections.Generic;

namespace Hassium
{
    public class HassiumDictionary: HassiumObject
    {
        public Dictionary<HassiumObject, HassiumObject> Value { get; private set; }

        public HassiumDictionary(Dictionary<HassiumObject, HassiumObject> value)
        {
            this.Value = value;
        }
    }
}

