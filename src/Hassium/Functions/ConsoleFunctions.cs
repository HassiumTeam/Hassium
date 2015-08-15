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

        public static object Input(object[] args)
        {
            return Console.ReadLine();
        }

        public static object Cls(object[] args)
        {
            Console.Clear();
            return null;
        }

        public static object Pause(object[] args)
        {
            Console.ReadKey(true);
            return null;
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

