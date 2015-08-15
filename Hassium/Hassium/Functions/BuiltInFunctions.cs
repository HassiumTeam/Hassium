using System;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Hassium
{
    public static class BuiltInFunctions
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
            Process.Start(args[0].ToString(), concatArray(args, 1).ToString());
            return null;
        }

        private static object[] concatArray(object[] args, int startIndex)
        {
            object[] result = new object[args.Length];

            for (int x = startIndex; x < args.Length; x++)
                result[x] = args[x];

            return result;
        }
    }
}

