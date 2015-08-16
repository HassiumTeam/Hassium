using System;
using System.Diagnostics;
using System.IO;

namespace Hassium
{
    public partial class BuiltInFunctions
    {
        public static object Puts(object[] args)
        {
            File.WriteAllText(args[0].ToString(), args[1].ToString());
            return null;
        }
        
        public static object Mdir(object[] args)
        {
            if (Directory.Exists(args[0].ToString()))
                throw new Exception("Directory already exists!");
            else
                Directory.CreateDirectory(args[0].ToString());

            return null;
        }

        public static object DDir(object[] args)
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
    }
}

