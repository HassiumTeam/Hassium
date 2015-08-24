using System;
using System.Linq;

namespace Hassium.Functions
{
	public class ArrayFunctions : ILibrary
	{
		[IntFunc("resizearr")]
		public static object ResizeArr(object[] args)
		{
			object arr = args[0];
			object[] objarr = (object[])arr;

			object[] newobj = new object[objarr.Length + Convert.ToInt32(args[1]) - 1];

			for (int x = 0; x < objarr.Length; x++)
				newobj[x] = objarr[x];

			return newobj;
		}
		[IntFunc("getarr")]
		public static object GetArr(object[] args)
		{
			object arr = args[0];
			object[] objarr = (object[])arr;

			return objarr[Convert.ToInt32(args[1])];
		}
		[IntFunc("setarr")]
		public static object SetArr(object[] args)
		{
			object arr = args[0];
			object[] objarr = (object[])arr;

			objarr[Convert.ToInt32(args[2])] = args[1];

			return objarr;
		}
        [IntFunc("arrlen")]
		public static object ArrLen(object[] args)
		{
			object arr = args[0];
			object[] objarr = (object[])arr;

			return Convert.ToDouble(objarr.Length);
		}
		[IntFunc("concatarr")]
		public static object ConcatArr(object[] args)
		{
			string result = "";
			object arr = args[0];
			object[] objarr = (object[])arr;

			foreach (object entry in objarr)
			{
				result += entry + " ";
			}

			return result;
		}
		[IntFunc("newarr")]
		public static object NewArr(object[] args)
		{
			return new object[Convert.ToInt32(args[0])];
		}
		[IntFunc("arrayfill")]
		public static object ArrayFill(object[] args)
		{
			int num = Convert.ToInt32(args[0]);
			object thing = args[1];
			return Enumerable.Repeat(thing, num).ToArray();
		}
	}
}