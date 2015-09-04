using System;
using System.Linq;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

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
		    return string.Join("", args.Select(x => x.ToString()));
		}

		[IntFunc("tohex")]
		public static HassiumObject ToHex(HassiumObject[] args)
		{
			try
			{
				return args[0].HNum().ValueInt.ToString("{0:X}");
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
				return Convert.ToByte(args[0].HNum().ValueInt);
			}
			catch
			{
				return args[0].ToString();
			}
		}

		[IntFunc("toarr")]
		public static HassiumObject ToArr(HassiumObject[] args)
		{
			return args[0].HArray();
		}

		[IntFunc("newarray")]
		public static HassiumObject NewArray(HassiumObject[] args)
		{
			return new HassiumArray(new HassiumObject[args[0].HNum().ValueInt]);
		}
	}
}
