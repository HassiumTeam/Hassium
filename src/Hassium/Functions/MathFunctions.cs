using System;

namespace Hassium
{
    public partial class BuiltInFunctions
    {
        public static object Pow(object[] args)
        {
            return (long)(Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
        }

        public static object Sqrt(object[] args)
        {
            return (long)(Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
        }
    }
}

