using System;
using System.Collections.Generic;
using System.Linq;

namespace Hassium.Functions
{
	public class ArrayFunctions : ILibrary
	{
		[IntFunc("array_resize", "resizearr")]
		public static HassiumObject ResizeArr(HassiumArray args)
		{
			HassiumObject[] objarr = GetArr(args[0]);

			HassiumObject[] newobj = new HassiumObject[objarr.Length + Convert.ToInt32((object)args[1]) - 1];

			for (int x = 0; x < objarr.Length; x++)
				newobj[x] = objarr[x];

			return newobj;
		}
		[IntFunc("getarr")]
		public static HassiumObject GetArr(HassiumArray args)
		{
			HassiumObject arr = GetArr(args[0]);
			HassiumObject[] objarr = (HassiumObject[])arr;

			return objarr[Convert.ToInt32((object)args[1])];
		}
		[IntFunc("setarr")]
		public static HassiumObject SetArr(HassiumArray args)
		{
			HassiumObject arr = GetArr(args[0]);
			HassiumObject[] objarr = (HassiumObject[])arr;

			objarr[Convert.ToInt32((object)args[2])] = args[1];

			return objarr;
		}
		[IntFunc("array_length", "arrlen")]
		public static HassiumObject ArrLen(HassiumArray args)
		{
			HassiumObject arr = GetArr(args[0]);
			HassiumObject[] objarr = (HassiumObject[])arr;

			return Convert.ToDouble(objarr.Length);
		}
		[IntFunc("array_join", "concatarr")]
		public static HassiumObject ArrayJoin(HassiumArray args)
		{
			HassiumObject[] objarr = GetArr(args[0]);
			string separator = " ";
			if (args.Value.Length > 1) separator = args[1].ToString();

			return objarr.Aggregate((a, b) => a + separator + b);
		}
		[IntFunc("newarr")]
		public static HassiumObject NewArr(HassiumArray args)
		{
			return new HassiumObject[Convert.ToInt32((object)args[0])];
		}
		[IntFunc("array_fill")]
		public static HassiumObject ArrayFill(HassiumArray args)
		{
			int num = Convert.ToInt32((object)args[0]);
			HassiumObject thing = args[1];
			return Enumerable.Repeat(thing, num).ToArray();
		}

		[IntFunc("reversearr", "array_reverse")]
		public static HassiumObject ArrayReverse(HassiumArray args)
		{
			return GetArr(args[0]).ToArray().Reverse().ToArray();
		}
	
		[IntFunc("array_op")]
		public static HassiumObject ArrayOp(HassiumArray args)
		{
			return GetArr(args[0]).Aggregate((a, b) => HassiumFunction.GetFunc2(args[1])(a, b));
		}

		#region LINQ-like functions
		[IntFunc("array_select")]
		public static HassiumObject ArraySelect(HassiumArray args)
		{
			return GetArr(args[0]).Select(x => HassiumFunction.GetFunc1(args[1])(x)).ToArray();
		}

		[IntFunc("array_where")]
		public static HassiumObject ArrayWhere(HassiumArray args)
		{
			return GetArr(args[0]).Where(x => HassiumFunction.GetFunc1(args[1])(x)).ToArray();
		}

		[IntFunc("array_any")]
		public static HassiumObject ArrayAny(HassiumArray args)
		{
			return GetArr(args[0]).Any(x => HassiumFunction.GetFunc1(args[1])(x));
		}

		[IntFunc("array_first")]
		public static HassiumObject ArrayFirst(HassiumArray args)
		{
			if (args[1] is IFunction)
				return GetArr(args[0]).First(x => HassiumFunction.GetFunc1(args[1])(x));
			else
				return GetArr(args[0]).First();
		}

		[IntFunc("array_last")]
		public static HassiumObject ArrayLast(HassiumArray args)
		{
			if (args[1] is IFunction)
				return GetArr(args[0]).Last(x => HassiumFunction.GetFunc1(args[1])(x));
			else
				return GetArr(args[0]).Last();
		}

		[IntFunc("array_contains")]
		public static HassiumObject ArrayContains(HassiumArray args)
		{
			return GetArr(args[0]).Contains(args[1]);
		}

		[IntFunc("array_zip")]
		public static HassiumObject ArrayZip(HassiumArray args)
		{
			return GetArr(args[0]).Zip(GetArr(args[1]), HassiumFunction.GetFunc2(args[2])).ToArray();
		}
		#endregion

		[IntFunc("range")]
		public static HassiumObject Range(HassiumArray args)
		{
			var from = Convert.ToDouble((object)args[0]);
			var to = Convert.ToDouble((object)args[1]);
			if(args.Value.Length > 2)
			{
				var step = Convert.ToDouble((object)args[2]);
				var list = new List<double>();
				if(step == 0) throw new Exception("The step for range() can't be zero");
				if (to < from && step > 0) step = -step;
				if (to > from && step < 0) step = -step;
				for (var i = from; step < 0 ? i > to : i < to; i += step)
				{
					list.Add(i);
				}
				return list.ToArray().Cast<object>().ToArray();
			}
			return from == to
				? new[] {from}.Cast<HassiumObject>().ToArray()
				: (to < from 
					? Enumerable.Range((int)to, (int)from).Reverse().ToArray()
					: Enumerable.Range((int)from, (int)to)).Cast<HassiumObject>().ToArray();
		}

		public static HassiumObject[] GetArr(HassiumObject arg)
		{
		    return ((Dictionary<HassiumObject, HassiumObject>) ((HassiumDictionary) arg)).Values.ToArray();
		}
	}
}