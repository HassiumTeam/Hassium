using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Hassium
{
    public class FilesystemFunctions : ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
            result.Add("puts", new InternalFunction(FilesystemFunctions.Puts));
            result.Add("readf", new InternalFunction(FilesystemFunctions.Readf));
            result.Add("readfarr", new InternalFunction(FilesystemFunctions.Readfarr));
            result.Add("mdir", new InternalFunction(FilesystemFunctions.Mdir));
            result.Add("ddir", new InternalFunction(FilesystemFunctions.Ddir));
            result.Add("dfile", new InternalFunction(FilesystemFunctions.Dfile));
            result.Add("setdir", new InternalFunction(FilesystemFunctions.Setdir));
            result.Add("getdir", new InternalFunction(FilesystemFunctions.Getdir));
            result.Add("fexists", new InternalFunction(FilesystemFunctions.Fexists));
            result.Add("dexists", new InternalFunction(FilesystemFunctions.Dexists));
            result.Add("system", new InternalFunction(FilesystemFunctions.System));

            return result;
        }
        public static object Puts(object[] args)
        {
            File.WriteAllText(args[0].ToString(), args[1].ToString());
            return null;
        }

        public static object Readf(object[] args)
        {
            return File.ReadAllText(args[0].ToString());
        }

        public static object Readfarr(object[] args)
        {
            return File.ReadAllLines(args[0].ToString());
        }

        public static object Mdir(object[] args)
        {
            if (Directory.Exists(args[0].ToString()))
                throw new Exception("Directory already exists!");
            else
                Directory.CreateDirectory(args[0].ToString());

            return null;
        }

        public static object Ddir(object[] args)
        {
            if (!Directory.Exists(args[0].ToString()))
                throw new Exception("Directory does not exist!");
            else
                Directory.Delete(args[0].ToString());

            return null;
        }

        public static object Dfile(object[] args)
        {
            if (!File.Exists(args[0].ToString()))
                throw new Exception("File does not exist!");
            else
                File.Delete(args[0].ToString());

            return null;
        }

        public static object Getdir(object[] args)
        {
            return Directory.GetCurrentDirectory();
        }

        public static object Setdir(object[] args)
        {
            Directory.SetCurrentDirectory(arrayToString(args));
            return null;
        }

        public static object Fexists(object[] args)
        {
            if (File.Exists(arrayToString(args)))
                return true;
            else
                return false;
        }

        public static object Dexists(object[] args)
        {
            if (Directory.Exists(arrayToString(args)))
                return true;
            else 
                return false;
        }

        public static object System(object[] args)
        {
            Process.Start(args[0].ToString(), arrayToString(args, 1));
            return null;
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