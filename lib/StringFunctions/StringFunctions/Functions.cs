using System;

namespace StringFunctions
{
	public static class Functions
	{
            public static object Strcat(object[] args)
            {
                return arrayToString(args);
            }

            public static object Strlen(object[] args)
            {
                return args[0].ToString().Length;
            }

            public static object Getch(object[] args)
            {
                return args[0].ToString()[Convert.ToInt32(args[1])].ToString();
            }

            public static object Sstr(object[] args)
            {
                return args[0].ToString().Substring(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
            }

            public static object Begins(object[] args)
            {
                return args[0].ToString().StartsWith(arrayToString(args, 1));
            }

            public static object ToUpper(object[] args)
            {
                return arrayToString(args).ToUpper();
            }

            public static object ToLower(object[] args)
            {
                return arrayToString(args).ToLower();
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
