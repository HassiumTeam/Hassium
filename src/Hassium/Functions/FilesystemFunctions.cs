using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hassium.Functions
{
	public class FilesystemFunctions : ILibrary
	{
		[IntFunc("puts")]
		public static HassiumObject Puts(HassiumArray args)
		{
			File.WriteAllText(args[0].ToString(), args[1].ToString());
			return null;
		}   

		[IntFunc("readf")]
		public static HassiumObject Readf(HassiumArray args)
		{
			return File.ReadAllText(args[0].ToString());
		}

		[IntFunc("readfarr")]
		public static HassiumObject Readfarr(HassiumArray args)
		{
			return File.ReadAllLines(args[0].ToString());
		}

		[IntFunc("mdir")]
		public static HassiumObject Mdir(HassiumArray args)
		{
			if (Directory.Exists(args[0].ToString()))
				throw new Exception("Directory already exists!");
			else
				Directory.CreateDirectory(args[0].ToString());

			return null;
		}

		[IntFunc("ddir")]
		public static HassiumObject Ddir(HassiumArray args)
		{
			if (!Directory.Exists(args[0].ToString()))
				throw new Exception("Directory does not exist!");
			else
				Directory.Delete(args[0].ToString());

			return null;
		}

		[IntFunc("dfile")]
		public static HassiumObject Dfile(HassiumArray args)
		{
			if (!File.Exists(args[0].ToString()))
				throw new Exception("File does not exist!");
			else
				File.Delete(args[0].ToString());

			return null;
		}

		[IntFunc("getdir")]
		public static HassiumObject Getdir(HassiumArray args)
		{
			return Directory.GetCurrentDirectory();
		}

		[IntFunc("setdir")]
		public static HassiumObject Setdir(HassiumArray args)
		{
			Directory.SetCurrentDirectory(arrayToString(args));
			return null;
		}

		[IntFunc("fexists")]
		public static HassiumObject Fexists(HassiumArray args)
		{
			return File.Exists(arrayToString(args));
		}

		[IntFunc("dexists")]
		public static HassiumObject Dexists(HassiumArray args)
		{
			return Directory.Exists(arrayToString(args));
		}

		[IntFunc("getfiles")]
		public static HassiumObject Getfiles(HassiumArray args)
		{
			return Directory.GetFiles(args[0].ToString());
		}

		[IntFunc("getdirs")]
		public static HassiumObject GetDirs(HassiumArray args)
		{
			return Directory.GetDirectories(args[0].ToString());
		}

		private static string arrayToString(HassiumArray args, int startIndex = 0)
		{
			return string.Join("", args.Cast<object>());
		}
	}
}