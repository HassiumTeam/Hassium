using System;
using Hassium.HassiumObjects;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumChar: HassiumObject
    {
        public char Value { get; private set; }

        public HassiumChar(char value)
        {
            this.Value = value;
            this.Attributes.Add("toString", new InternalFunction(toString));
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Convert.ToString(this.Value));
        }
    }
}

