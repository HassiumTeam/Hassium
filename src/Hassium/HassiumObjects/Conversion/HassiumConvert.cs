using System;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Conversion
{
    public class HassiumConvert : HassiumObject
    {
        public HassiumConvert()
        {
            Attributes.Add("toNumber", new InternalFunction(toNumber, 1));
            Attributes.Add("toString", new InternalFunction(toString, 1));
            Attributes.Add("toBool", new InternalFunction(toBool, 1));
        }

        public static HassiumObject toNumber(HassiumObject[] args)
        {
            if (args[0] is HassiumString)
            {
                var ret = Convert.ToDouble(((HassiumString) args[0]).Value);
                if (ret == System.Math.Truncate(ret))
                    return new HassiumInt((int) ret);
                else
                    return new HassiumDouble(ret);
            }
            else if (args[0] is HassiumInt)
            {
                return new HassiumDouble(((HassiumInt) args[0]).Value);
            }
            else if (args[0] is HassiumDouble)
            {
                return new HassiumInt(((HassiumDouble) args[0]).ValueInt);
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
                return new HassiumBool(Convert.ToBoolean(((HassiumString) args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumBool(Convert.ToBoolean(((HassiumDouble) args[0]).ValueInt));
            else if (args[0] is HassiumInt)
                return new HassiumBool(Convert.ToBoolean(((HassiumInt) args[0]).Value));
            else
                throw new Exception("Unknown format for Convert.toBool");
        }
    }
}