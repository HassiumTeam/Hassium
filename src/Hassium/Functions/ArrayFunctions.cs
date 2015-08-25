using System;
using System.Linq;

namespace Hassium.Functions
{
	public class ArrayFunctions : ILibrary
	{
		[IntFunc("array_resize", "resizearr")]
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
		[IntFunc("array_length", "arrlen")]
		public static object ArrLen(object[] args)
		{
			object arr = args[0];
			object[] objarr = (object[])arr;

			return Convert.ToDouble(objarr.Length);
		}
		[IntFunc("array_join", "concatarr")]
		public static object ArrayJoin(object[] args)
		{
			object arr = args[0];
			object[] objarr = (object[])arr;
			string separator = " ";
			if (args.Length > 1) separator = args[1].ToString();

			return objarr.Aggregate((a, b) => a + separator + b);
		}
		[IntFunc("newarr")]
		public static object NewArr(object[] args)
		{
			return new object[Convert.ToInt32(args[0])];
		}
		[IntFunc("array_fill")]
		public static object ArrayFill(object[] args)
		{
			int num = Convert.ToInt32(args[0]);
			object thing = args[1];
			return Enumerable.Repeat(thing, num).ToArray();
		}

		[IntFunc("array_reverse")]
		public static object ArrayReverse(object[] args)
		{
			return ((object[]) args[0]).Reverse().ToArray();
		}

		[IntFunc("array_op")]
		public static object ArrayOp(object[] args)
		{
			return ((object[]) args[0]).Aggregate((a, b) => HassiumFunction.GetFunc2(args[1])(a, b));
		}

		#region LINQ-like functions
		[IntFunc("array_select")]
		public static object ArraySelect(object[] args)
		{
			return ((object[]) args[0]).Select(x => HassiumFunction.GetFunc1(args[1])(x)).ToArray();
		}

		[IntFunc("array_where")]
		public static object ArrayWhere(object[] args)
		{
			return ((object[])args[0]).Where(x => (bool)HassiumFunction.GetFunc1(args[1])(x)).ToArray();
		}

		[IntFunc("array_any")]
		public static object ArrayAny(object[] args)
		{
			return ((object[])args[0]).Any(x => (bool)HassiumFunction.GetFunc1(args[1])(x));
		}

		[IntFunc("array_first")]
		public static object ArrayFirst(object[] args)
		{
			if (args[1] is IFunction)
				return ((object[]) args[0]).First(x => (bool) HassiumFunction.GetFunc1(args[1])(x));
			else
				return ((object[]) args[0]).First();
		}

		[IntFunc("array_last")]
		public static object ArrayLast(object[] args)
		{
			if (args[1] is IFunction)
				return ((object[])args[0]).Last(x => (bool)HassiumFunction.GetFunc1(args[1])(x));
			else
				return ((object[])args[0]).Last();
		}

		[IntFunc("array_contains")]
		public static object ArrayContains(object[] args)
		{
			return ((object[]) args[0]).Contains(args[1]);
		}

		[IntFunc("array_zip")]
		public static object ArrayZip(object[] args)
		{
			return ((object[]) args[0]).Zip((object[]) args[1], HassiumFunction.GetFunc2(args[2])).ToArray();
		}
		#endregion
	}
}