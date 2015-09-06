using System;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Conversion
{
    public class HassiumConvert: HassiumObject
    {
        public HassiumConvert()
        {
            Attributes.Add("toNumber", new InternalFunction(toNumber));
            Attributes.Add("toString", new InternalFunction(toString));
            Attributes.Add("toBool", new InternalFunction(toBool));
        }

        public static HassiumObject toNumber(HassiumObject[] args)
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

        public static HassiumObject toString(HassiumObject[] args)
        {
            return string.Join("", args.Select(x => x.ToString()));
        }

        public static HassiumObject toBool(HassiumObject[] args)
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

