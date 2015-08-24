using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Hassium
{
    public class ConsoleFunctions : ILibrary
    {
        [IntFunc("print")]
        public static object Print(object[] args)
        {
            Console.Write(String.Join("", args));
            return null;
        }

        [IntFunc("println")]
        public static object PrintLn(object[] args)
        {
            Console.WriteLine(String.Join("", args));
            return null;
        }

        [IntFunc("printarr")]
        public static object PrintArr(object[] args)
        {
            Console.WriteLine(ConversionFunctions.ToStr(args));
            return null;
        }

        [IntFunc("input")]
        public static object Input(object[] args)
        {
            return Console.ReadLine();
        }

        [IntFunc("cls")]
        public static object Cls(object[] args)
        {
            Console.Clear();
            return null;
        }

        [IntFunc("pause")]
        public static object Pause(object[] args)
        {
            Console.ReadKey(true);
            return null;
        }

        [IntFunc("setfcol")]
        public static object Setfcol(object[] args)
        {
            Console.ForegroundColor = parseColor(args[0].ToString());
            return null;
        }

        [IntFunc("setbcol")]
        public static object Setbcol(object[] args)
        {
            Console.BackgroundColor = parseColor(args[0].ToString());
            return null;
        }

        [IntFunc("getfcol")]
        public static object Getfcol(object[] args)
        {
            return Console.ForegroundColor.ToString();
        }

        [IntFunc("getbcol")]
        public static object Getbcol(object[] args)
        {
            return Console.BackgroundColor.ToString();
        }

        private static ConsoleColor parseColor(string color)
        {
            switch (color)
            {
                case "red":
                    return ConsoleColor.Red;
                case "yellow":
                    return ConsoleColor.Yellow;
                case "green":
                    return ConsoleColor.Green;
                case "blue":
                    return ConsoleColor.Blue;
                case "white":
                    return ConsoleColor.White;
                case "black":
                    return ConsoleColor.Black;
                default:
                    throw new Exception("Color is not valid!");
            }
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
