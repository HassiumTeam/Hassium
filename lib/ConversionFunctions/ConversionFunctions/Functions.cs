using System;

namespace ConversionFunctions
{
	public static class Functions
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
                return arrayToString(args);
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
       	    private static string arrayToString(object[] args, int startIndex = 0)
       	    {
	    	string result = "";

	        for (int x = startIndex; x < args.Length; x++)
        		result += args[x].ToString();

	       	return result;
           }
	}
}
