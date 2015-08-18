using System;
using System.Collections.Generic;

namespace Hassium
{
    public class MathFunctions : ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
            result.Add("pow", new InternalFunction(MathFunctions.Pow));
            result.Add("sqrt", new InternalFunction(MathFunctions.Sqrt));

            return result;
        }
        public static object Pow(object[] args)
        {
            return (double)(Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
        }

        public static object Sqrt(object[] args)
        {
            return (double)(Math.Sqrt(Convert.ToDouble(args[0])));
        }
    }
}
