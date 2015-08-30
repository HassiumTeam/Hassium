using System;
using System.Linq;

namespace Hassium.Functions
{
	public class StringFunctions : ILibrary
	{
		[IntFunc("strcat")]
		public static HassiumObject Strcat(HassiumArray args)
		{
			return String.Join("", args.Cast<object>());
		}

		[IntFunc("strlen")]
		public static HassiumObject Strlen(HassiumArray args)
		{
			return args[0].ToString().Length;
		}

		[IntFunc("getch")]
		public static HassiumObject Getch(HassiumArray args)
		{
			return args[0].ToString()[Convert.ToInt32((object)args[1])].ToString();
		}

		[IntFunc("substr", "sstr")]
		public static HassiumObject Substr(HassiumArray args)
		{
			return args[0].ToString().Substring(Convert.ToInt32((object)args[1]), Convert.ToInt32((object)args[2]));
		}

		[IntFunc("begins")]
		public static HassiumObject Begins(HassiumArray args)
		{
			return args[0].ToString().StartsWith(args[1].ToString());
		}

		[IntFunc("ends")]
		public static HassiumObject Ends(HassiumArray args)
		{
			return args[0].ToString().EndsWith(args[1].ToString());
		}

		[IntFunc("toupper")]
		public static HassiumObject ToUpper(HassiumArray args)
		{
			return String.Join("", args.Cast<object>()).ToUpper();
		}

		[IntFunc("tolower")]
		public static HassiumObject ToLower(HassiumArray args)
		{
			return String.Join("", args.Cast<object>()).ToLower();
		}

		[IntFunc("contains")]
		public static HassiumObject Contains(HassiumArray args)
		{
			return args[0].ToString().Contains(args[1].ToString());
		}

		[IntFunc("sformat")]
		public static HassiumObject SFormat(HassiumArray args)
		{
			return string.Format(args[0].ToString(), args.Value.Skip(1).ToArray());
		}

		[IntFunc("split")]
		public static HassiumObject Split(HassiumArray args)
		{
			return args[0].ToString().Split(Convert.ToChar((object)args[1]));
		}

		[IntFunc("replace")]
		public static HassiumObject Replace(HassiumArray args)
		{
			return args[0].ToString().Replace(args[1].ToString(), args[2].ToString());
		}

		[IntFunc("index")]
		public static HassiumObject Index(HassiumArray args)
		{
			return args[0].ToString().IndexOf(args[1].ToString());
		}

		[IntFunc("lastindex")]
		public static HassiumObject LastIndex(HassiumArray args)
		{
			return args[0].ToString().LastIndexOf(args[1].ToString());
		}

		[IntFunc("padleft")]
		public static HassiumObject PadLeft(HassiumArray args)
		{
			return args[0].ToString().PadLeft(Convert.ToInt32((object)args[1]));
		}

		[IntFunc("padright")]
		public static HassiumObject PadRight(HassiumArray args)
		{
			return args[0].ToString().PadRight(Convert.ToInt32((object)args[1]));
		}

		[IntFunc("remove")]
		public static HassiumObject Remove(HassiumArray args)
		{
			return args[0].ToString().Replace(Convert.ToChar((object)args[1]), Convert.ToChar((object)args[2]));
		}

		[IntFunc("trim")]
		public static HassiumObject Trim(HassiumArray args)
		{
			return args[0].ToString().Trim();
		}

		[IntFunc("trimleft")]
		public static HassiumObject TrimLeft(HassiumArray args)
		{
			return args[0].ToString().TrimStart();
		}

		[IntFunc("trimright")]
		public static HassiumObject TrimRight(HassiumArray args)
		{
			return args[0].ToString().TrimEnd();
		}

		[IntFunc("addslashes")]
		public static HassiumObject AddSlashes(HassiumArray args)
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
