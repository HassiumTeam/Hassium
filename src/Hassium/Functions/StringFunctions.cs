using System;
using System.Linq;

namespace Hassium.Functions
{
	public class StringFunctions : ILibrary
	{
		[IntFunc("strcat")]
		public static object Strcat(object[] args)
		{
			return String.Join("", args);
		}

		[IntFunc("strlen")]
		public static object Strlen(object[] args)
		{
			return args[0].ToString().Length;
		}

		[IntFunc("getch")]
		public static object Getch(object[] args)
		{
			return args[0].ToString()[Convert.ToInt32(args[1])].ToString();
		}

		[IntFunc("substr", "sstr")]
		public static object Substr(object[] args)
		{
			return args[0].ToString().Substring(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
		}

		[IntFunc("begins")]
		public static object Begins(object[] args)
		{
			return args[0].ToString().StartsWith(args[1].ToString());
		}

		[IntFunc("toupper")]
		public static object ToUpper(object[] args)
		{
			return String.Join("", args).ToUpper();
		}

		[IntFunc("tolower")]
		public static object ToLower(object[] args)
		{
			return String.Join("", args).ToLower();
		}

		[IntFunc("contains")]
		public static object Contains(object[] args)
		{
			return args[0].ToString().Contains(args[1].ToString());
		}

		[IntFunc("sformat")]
		public static object SFormat(object[] args)
		{
			return string.Format(args[0].ToString(), args.Skip(1).ToArray());
		}

		[IntFunc("split")]
		public static object Split(object[] args)
		{
			return args[0].ToString().Split(Convert.ToChar(args[1]));
		}

		[IntFunc("replace")]
		public static object Replace(object[] args)
		{
			return args[0].ToString().Replace(args[1].ToString(), args[2].ToString());
		}

        [IntFunc("index")]
        public static object Index(object[] args)
        {
            return args[0].ToString().IndexOf(args[1].ToString());
        }

        [IntFunc("lastindex")]
        public static object LastIndex(object[] args)
        {
            return args[0].ToString().LastIndexOf(args[1].ToString());
        }

		[IntFunc("addslashes")]
		public static object AddSlashes(object[] args)
		{
			return args[0].ToString()
				.Replace("\n", "\\n")
				.Replace("\r", "\\r")
				.Replace("\t", "\\t")
				.Replace("\\", "\\\\")
				.Replace("\"", "\\\"");
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
