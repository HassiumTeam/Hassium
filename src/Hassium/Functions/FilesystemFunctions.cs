using System;
using System.Collections.Generic;
using System.IO;

namespace Hassium.Functions
{
	public class FilesystemFunctions : ILibrary
	{
		[IntFunc("puts")]
		public static object Puts(object[] args)
		{
			File.WriteAllText(args[0].ToString(), args[1].ToString());
			return null;
		}

		[IntFunc("readf")]
		public static object Readf(object[] args)
		{
			return File.ReadAllText(args[0].ToString());
		}

		[IntFunc("readfarr")]
		public static object Readfarr(object[] args)
		{
			return File.ReadAllLines(args[0].ToString());
		}

		[IntFunc("mdir")]
		public static object Mdir(object[] args)
		{
			if (Directory.Exists(args[0].ToString()))
				throw new Exception("Directory already exists!");
			else
				Directory.CreateDirectory(args[0].ToString());

			return null;
		}

		[IntFunc("ddir")]
		public static object Ddir(object[] args)
		{
			if (!Directory.Exists(args[0].ToString()))
				throw new Exception("Directory does not exist!");
			else
				Directory.Delete(args[0].ToString());

			return null;
		}

		[IntFunc("dfile")]
		public static object Dfile(object[] args)
		{
			if (!File.Exists(args[0].ToString()))
				throw new Exception("File does not exist!");
			else
				File.Delete(args[0].ToString());

			return null;
		}

		[IntFunc("getdir")]
		public static object Getdir(object[] args)
		{
			return Directory.GetCurrentDirectory();
		}

		[IntFunc("setdir")]
		public static object Setdir(object[] args)
		{
			Directory.SetCurrentDirectory(arrayToString(args));
			return null;
		}

		[IntFunc("fexists")]
		public static object Fexists(object[] args)
		{
			return File.Exists(arrayToString(args));
		}

		[IntFunc("dexists")]
		public static object Dexists(object[] args)
		{
			return Directory.Exists(arrayToString(args));
		}

        [IntFunc("getfiles")]
        public static object Getfiles(object[] args)
        {
            return Directory.GetFiles(args[0].ToString());
        }

        [IntFunc("getdirs")]
        public static object GetDirs(object[] args)
        {
            return Directory.GetDirectories(args[0].ToString());
        }

		private static string arrayToString(IList<object> args, int startIndex = 0)
		{
			return string.Join("", args);
		}
	}
}