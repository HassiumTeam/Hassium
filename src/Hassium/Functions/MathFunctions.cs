using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hassium;

namespace BSharp
{
	public class MathFunctions : ILibrary
	{
		[IntFunc("hash")]
		public static object Hash(object[] args)
		{
			byte[] encodedText = new UTF8Encoding().GetBytes(args[1].ToString());
			byte[] hash = ((HashAlgorithm) CryptoConfig.CreateFromName(args[0].ToString().ToUpper())).ComputeHash(encodedText);
			return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
		}
		
		[IntFunc("pow")]
		public static object Pow(object[] args)
		{
			return (double) (Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
		}

		[IntFunc("sqrt")]
		public static object Sqrt(object[] args)
		{
			return (double) (Math.Sqrt(Convert.ToDouble(args[0])));
		}

		[IntFunc("abs")]
		public static object Abs(object[] args)
		{
			return (double) (Math.Abs(Convert.ToDouble(args[0])));
		}

		[IntFunc("acos")]
		public static object Acos(object[] args)
		{
			return (double) (Math.Acos(Convert.ToDouble(args[0])));
		}

		[IntFunc("asin")]
		public static object Asin(object[] args)
		{
			return (double) (Math.Asin(Convert.ToDouble(args[0])));
		}

		[IntFunc("atan")]
		public static object Atan(object[] args)
		{
			return (double) (Math.Acos(Convert.ToDouble(args[0])));
		}

		[IntFunc("atan2")]
		public static object Atan2(object[] args)
		{
			return (double) (Math.Atan2(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
		}

		[IntFunc("ceil")]
		public static object Ceil(object[] args)
		{
			return (double) (Math.Ceiling(Convert.ToDouble(args[0])));
		}

		[IntFunc("cos")]
		public static object Cos(object[] args)
		{
			return (double) (Math.Cos(Convert.ToDouble(args[0])));
		}

		[IntFunc("cosh")]
		public static object Cosh(object[] args)
		{
			return (double) (Math.Cosh(Convert.ToDouble(args[0])));
		}

		[IntFunc("exp")]
		public static object Exp(object[] args)
		{
			return (double) (Math.Exp(Convert.ToDouble(args[0])));
		}

		[IntFunc("floor")]
		public static object Floor(object[] args)
		{
			return (double) (Math.Floor(Convert.ToDouble(args[0])));
		}

		[IntFunc("ln")]
		public static object Ln(object[] args)
		{
			return (double) (Math.Log(Convert.ToDouble(args[0])));
		}

		[IntFunc("log")]
		public static object Log(object[] args)
		{
			return (double) (Math.Log(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
		}

		[IntFunc("log10")]
		public static object Log10(object[] args)
		{
			return (double) (Math.Log10(Convert.ToDouble(args[0])));
		}

		[IntFunc("max")]
		public static object Max(object[] args)
		{
			return (double) (Math.Max(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
		}

		[IntFunc("min")]
		public static object Min(object[] args)
		{
			return (double) (Math.Min(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
		}

		[IntFunc("round")]
		public static object Round(object[] args)
		{
			if(args.Count() > 1) return (double)(Math.Round(Convert.ToDouble(args[0]), Convert.ToInt32(args[1])));
			return (double)(Math.Round(Convert.ToDouble(args[0])));
		}

		[IntFunc("sin")]
		public static object Sin(object[] args)
		{
			return (double)(Math.Sin(Convert.ToDouble(args[0])));
		}

		[IntFunc("sinh")]
		public static object Sinh(object[] args)
		{
			return (double)(Math.Sinh(Convert.ToDouble(args[0])));
		}

		[IntFunc("tan")]
		public static object Tan(object[] args)
		{
			return (double)(Math.Tan(Convert.ToDouble(args[0])));
		}

		[IntFunc("tanh")]
		public static object Tanh(object[] args)
		{
			return (double)(Math.Tanh(Convert.ToDouble(args[0])));
		}
	}
}