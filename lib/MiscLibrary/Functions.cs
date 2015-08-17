using System;
using Hassium;

namespace MiscFunctions
{
	public static class Functions
	{
            public static object Free(object[] args)
            {
                Interpreter.variables.Remove(args[0].ToString());
                return null;
            }

            public static object Exit(object[] args)
            {
                if (args.Length > 0)
                {
                    Environment.Exit(Convert.ToInt32(args[0]));
                }
                else
                {
                    Environment.Exit(0);
                }

                return null;
            }

            public static object Type(object[] args)
            {
                return args[0].GetType().ToString().Substring(args[0].GetType().ToString().LastIndexOf(".") + 1);
            }

            public static object Throw(object[] args)
            {
                throw new Exception(arrayToString(args));
            }

       	    private static string arrayToString(object[] args, int startIndex = 0)
            {
                string result = "";

                for (int x = startIndex; x < args.Length; x++)
                    result += args[x].ToString();

                return result;
            }
	}
}
