using System;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium
{
    public class HassiumConvert: HassiumObject
    {
        public HassiumConvert()
        {
            Attributes.Add("toNumber", new InternalFunction(toNumber));
            Attributes.Add("toString", new InternalFunction(toString));
            Attributes.Add("toBool", new InternalFunction(toBool));
        }

        private HassiumObject toNumber(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
            {
                return new HassiumNumber(Convert.ToDouble(((HassiumString)args[0]).Value));
            }
            else
            {
                throw new Exception("Unknown format for Convert.toNumber");
            }
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(args[0].ToString());
        }

        private HassiumObject toBool(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumBool(Convert.ToBoolean(((HassiumString)args[0]).Value));
            else if (args[0] is HassiumNumber)
                return new HassiumBool(Convert.ToBoolean(((HassiumNumber)args[0]).ValueInt));
            else
                throw new Exception("Unknown format for Convert.toBool");
        }
    }
}

