using System;
using System.Collections.Generic;

namespace Hassium
{
    public class ConversionFunctions : ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
            result.Add("tonum", new InternalFunction(ConversionFunctions.ToNum));
            result.Add("tostr", new InternalFunction(ConversionFunctions.ToStr));
            result.Add("tobyte", new InternalFunction(ConversionFunctions.ToByte));

            return result;
        }

        public static object ToNum(object[] args)
        {
            double tmp = 0;
            if (double.TryParse(args[0].ToString(), out tmp))
                return tmp;
            else
                return args[0].ToString();
        }

        public static object ToStr(object[] args)
        {
            return String.Join("", args);
        }

        public static object ToByte(object[] args)
        {
            try
            {
                return Convert.ToByte(args[0]);
            }
            catch
            {
                return args[0].ToString();
            }
        }

        public static object ToArr(object[] args)
        {
            return args;
        }
    }
}
