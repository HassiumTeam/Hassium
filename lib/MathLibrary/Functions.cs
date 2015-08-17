using System;

namespace MathFunctions
{
	public static class Functions
	{
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
