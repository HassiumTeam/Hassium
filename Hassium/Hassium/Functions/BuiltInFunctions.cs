using System;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Hassium
{
    public partial class BuiltInFunctions
    {
        public static object Print(object[] args)
        {
            Console.Write(String.Join("", args).Replace("\\n", "\n"));
            return null;
        }

        public static object Strcat(object[] args)
        {
            return String.Join("", args);
        }

        public static object Input(object[] args)
        {
            return Console.ReadLine();
        }

        public static object Strlen(object[] args)
        {
            return args[0].ToString().Length;
        }

        public static object Cls(object[] args)
        {
            Console.Clear();
            return null;
        }

        public static object Getch(object[] args)
        {
            return args[0].ToString()[Convert.ToInt32(args[1])].ToString();
        }

        public static object Puts(object[] args)
        {
            File.WriteAllText(args[0].ToString(), args[1].ToString());
            return null;
        }

        public static object Free(object[] args)
        {
            Interpreter.variables.Remove(args[0].ToString());
            return null;
        }

        public static object Exit(object[] args)
        {
            if (args.Length > 0)
            {
                Environment.Exit(Convert.ToInt32(args[0]));
            }
            else
            {
                Environment.Exit(0);
            }

            return null;
        }

        public static object System(object[] args)
        {
            Process.Start(args[0].ToString(), arrayToString(args, 1));
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

        public static object Sstr(object[] args)
        {
            return args[0].ToString().Substring(Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
        }

        public static object Begins(object[] args)
        {
            return args[0].ToString().StartsWith(arrayToString(args, 1));
        }

        public static object Type(object[] args)
        {
            return args[0].GetType().ToString().Substring(args[0].GetType().ToString().LastIndexOf(".") + 1);
        }

        private static object[] narrowArray(object[] args, int startIndex)
        {
            object[] result = new object[args.Length];

            for (int x = startIndex; x < args.Length; x++)
                result[x] = args[x];

            return result;
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

