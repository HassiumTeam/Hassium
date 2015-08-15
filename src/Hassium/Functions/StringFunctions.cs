using System;

namespace Hassium
{
    public partial class BuiltInFunctions
    {
        public static object Strcat(object[] args)
        {
            return String.Join("", args);
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
    }
}

