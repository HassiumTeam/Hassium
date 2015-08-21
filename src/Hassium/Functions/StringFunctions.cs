using System;
using System.Collections.Generic;
using System.Linq;

namespace Hassium
{
    public class StringFunctions : ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
            result.Add("strcat", new InternalFunction(StringFunctions.Strcat));
            result.Add("strlen", new InternalFunction(StringFunctions.Strlen));
            result.Add("getch", new InternalFunction(StringFunctions.Getch));
            result.Add("sstr", new InternalFunction(StringFunctions.Sstr));
            result.Add("begins", new InternalFunction(StringFunctions.Begins));
            result.Add("toupper", new InternalFunction(StringFunctions.ToUpper));
            result.Add("tolower", new InternalFunction(StringFunctions.ToLower));
            result.Add("contains", new InternalFunction(StringFunctions.Contains));
            result.Add("sformat", new InternalFunction(StringFunctions.SFormat));
            result.Add("split", new InternalFunction(StringFunctions.Split));

            return result;
        }
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
            return args[0].ToString().StartsWith(args[1].ToString());
        }

        public static object ToUpper(object[] args)
        {
            return String.Join("", args).ToUpper();
        }

        public static object ToLower(object[] args)
        {
            return String.Join("", args).ToLower();
        }

        public static object Contains(object[] args)
        {
            return args[0].ToString().Contains(args[1].ToString());
        }

        public static object SFormat(object[] args)
        {
            return string.Format(args[0].ToString(), args.Skip(1).ToArray());
        }

        public static object Split(object[] args)
        {
            return args[0].ToString().Split(Convert.ToChar(args[1]));
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
