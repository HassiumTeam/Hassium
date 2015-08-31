using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hassium.Functions
{
	public class MathFunctions : ILibrary
	{
		[IntFunc("hash")]
		public static HassiumObject Hash(HassiumObject[] args)
		{
			byte[] encodedText = new UTF8Encoding().GetBytes(args[1].ToString());
			byte[] hash = ((HashAlgorithm) CryptoConfig.CreateFromName(args[0].ToString().ToUpper())).ComputeHash(encodedText);
			return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
		}
		
		[IntFunc("pow")]
		public static HassiumObject Pow(HassiumObject[] args)
		{
			return (double) (Math.Pow(Convert.ToDouble((object)args[0]), Convert.ToDouble((object)args[1])));
		}

		[IntFunc("sqrt")]
		public static HassiumObject Sqrt(HassiumObject[] args)
		{
			return (double) (Math.Sqrt(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("abs")]
		public static HassiumObject Abs(HassiumObject[] args)
		{
			return (double) (Math.Abs(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("acos")]
		public static HassiumObject Acos(HassiumObject[] args)
		{
			return (double) (Math.Acos(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("asin")]
		public static HassiumObject Asin(HassiumObject[] args)
		{
			return (double) (Math.Asin(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("atan")]
		public static HassiumObject Atan(HassiumObject[] args)
		{
			return (double) (Math.Acos(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("atan2")]
		public static HassiumObject Atan2(HassiumObject[] args)
		{
			return (double) (Math.Atan2(Convert.ToDouble((object)args[0]), Convert.ToDouble((object)args[1])));
		}

		[IntFunc("ceil")]
		public static HassiumObject Ceil(HassiumObject[] args)
		{
			return (double) (Math.Ceiling(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("cos")]
		public static HassiumObject Cos(HassiumObject[] args)
		{
			return (double) (Math.Cos(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("cosh")]
		public static HassiumObject Cosh(HassiumObject[] args)
		{
			return (double) (Math.Cosh(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("exp")]
		public static HassiumObject Exp(HassiumObject[] args)
		{
			return (double) (Math.Exp(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("floor")]
		public static HassiumObject Floor(HassiumObject[] args)
		{
			return (double) (Math.Floor(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("ln")]
		public static HassiumObject Ln(HassiumObject[] args)
		{
			return (double) (Math.Log(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("log")]
		public static HassiumObject Log(HassiumObject[] args)
		{
			return (double) (Math.Log(Convert.ToDouble((object)args[0]), Convert.ToDouble((object)args[1])));
		}

		[IntFunc("log10")]
		public static HassiumObject Log10(HassiumObject[] args)
		{
			return (double) (Math.Log10(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("max")]
		public static HassiumObject Max(HassiumObject[] args)
		{
			return (double) (Math.Max(Convert.ToDouble((object)args[0]), Convert.ToDouble((object)args[1])));
		}

		[IntFunc("min")]
		public static HassiumObject Min(HassiumObject[] args)
		{
			return (double) (Math.Min(Convert.ToDouble((object)args[0]), Convert.ToDouble((object)args[1])));
		}

		[IntFunc("round")]
		public static HassiumObject Round(HassiumObject[] args)
		{
			if(args.Count() > 1) return (double)(Math.Round(Convert.ToDouble((object)args[0]), Convert.ToInt32((object)args[1])));
			return (double)(Math.Round(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("sin")]
		public static HassiumObject Sin(HassiumObject[] args)
		{
			return (double)(Math.Sin(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("sinh")]
		public static HassiumObject Sinh(HassiumObject[] args)
		{
			return (double)(Math.Sinh(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("tan")]
		public static HassiumObject Tan(HassiumObject[] args)
		{
			return (double)(Math.Tan(Convert.ToDouble((object)args[0])));
		}

		[IntFunc("tanh")]
		public static HassiumObject Tanh(HassiumObject[] args)
		{
			return (double)(Math.Tanh(Convert.ToDouble((object)args[0])));
		}
	}
}