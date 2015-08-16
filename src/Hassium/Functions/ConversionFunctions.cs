using System;

namespace Hassium
{
    public partial class BuiltInFunctions
    {
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
    }
}

