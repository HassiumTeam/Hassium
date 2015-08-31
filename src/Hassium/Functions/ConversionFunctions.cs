using System;
using System.Collections.Generic;
using System.Linq;

namespace Hassium.Functions
{
	public class ConversionFunctions : ILibrary
	{
		[IntFunc("tonum")]
		public static HassiumObject ToNum(HassiumObject[] args)
		{
			double tmp = 0;
			if (double.TryParse(args[0].ToString(), out tmp))
				return tmp;
			else
				return args[0].ToString();
		}

		[IntFunc("tostr")]
		public static HassiumObject ToStr(HassiumObject[] args)
		{
			if(args[0] is HassiumDictionary)
			{
				return "Array { " +
					   string.Join(", ", ((HassiumDictionary)(args[0])).Value.Select(x => "[" + x.Key.ToString() + "] => " + x.Value.ToString())) + " }";
			}
            if(args[0] is HassiumObject[])
			{
                return ((HassiumObject[])(args[0]))
						.Aggregate("Array { ",
							(current, item) => current + ((item is HassiumArray ? ToStr(new[] { item }).ToString() : (item.ToString().Replace("\"", "\\\""))) + ", ")).TrimEnd(',', ' ') + " }";
			}
			return String.Join("", args.Cast<object>());
		}

		[IntFunc("tohex")]
		public static HassiumObject ToHex(HassiumObject[] args)
		{
			try
			{
				return Convert.ToInt32((object)args[0]).ToString("{0:X}");
			}
			catch
			{
				return args[0].ToString();
			}
		}

		[IntFunc("tobyte")]
		public static HassiumObject ToByte(HassiumObject[] args)
		{
			try
			{
				return Convert.ToByte((object)args[0]);
			}
			catch
			{
				return args[0].ToString();
			}
		}

		[IntFunc("toarr")]
		public static HassiumObject ToArr(HassiumObject[] args)
		{
            return ((HassiumArray)args);
		}

        [IntFunc("newarray")]
        public static HassiumObject NewArray(HassiumObject[] args)
        {
            return new HassiumArray(new HassiumObject[Convert.ToInt32(((HassiumNumber)args[1]).Value)]);
        }
	}
}
