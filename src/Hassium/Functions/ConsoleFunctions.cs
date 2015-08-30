using System;
using System.Linq;

namespace Hassium.Functions
{
    public class ConsoleFunctions : ILibrary
    {
        [IntFunc("print")]
        public static HassiumObject Print(HassiumArray args)
        {
            Console.Write(String.Join("", args.Cast<object>()));
            return null;
        }

        [IntFunc("println")]
        public static HassiumObject PrintLn(HassiumArray args)
        {
            Console.WriteLine(String.Join("", args.Value.Select(x => x.ToString())));
            return null;
        }

        [IntFunc("printarr")]
        public static HassiumObject PrintArr(HassiumArray args)
        {
            Console.WriteLine(ConversionFunctions.ToStr(args).ToString());
            return null;
        }

        [IntFunc("input")]
        public static HassiumObject Input(HassiumArray args)
        {
            return Console.ReadLine();
        }

        [IntFunc("cls")]
        public static HassiumObject Cls(HassiumArray args)
        {
            Console.Clear();
            return null;
        }

        [IntFunc("pause")]
        public static HassiumObject Pause(HassiumArray args)
        {
            Console.ReadKey(true);
            return null;
        }

        [IntFunc("setfcol")]
        public static HassiumObject Setfcol(HassiumArray args)
        {
            Console.ForegroundColor = parseColor(args[0].ToString());
            return null;
        }

        [IntFunc("setbcol")]
        public static HassiumObject Setbcol(HassiumArray args)
        {
            Console.BackgroundColor = parseColor(args[0].ToString());
            return null;
        }

        [IntFunc("getfcol")]
        public static HassiumObject Getfcol(HassiumArray args)
        {
            return Console.ForegroundColor.ToString();
        }

        [IntFunc("getbcol")]
        public static HassiumObject Getbcol(HassiumArray args)
        {
            return Console.BackgroundColor.ToString();
        }

        [IntFunc("setposition")]
        public static HassiumObject ScursorPosition(HassiumArray args)
        {
            Console.SetCursorPosition(Convert.ToInt32((object)args[0]), Convert.ToInt32((object)args[1]));
            return null;
        }

        [IntFunc("getleft")]
        public static HassiumObject GetLeft(HassiumArray args)
        {
            return Console.CursorLeft;
        }

        [IntFunc("gettop")]
        public static HassiumObject GetTop(HassiumArray args)
        {
            return Console.CursorTop;
        }

        [IntFunc("gettitle")]
        public static HassiumObject GetTitle(HassiumArray args)
        {
            return Console.Title;
        }

        [IntFunc("settitle")]
        public static HassiumObject SetTitle(HassiumArray args)
        {
            Console.Title = args[0].ToString();
            return null;
        }

        [IntFunc("beep")]
        public static HassiumObject Beep(HassiumArray args)
        {
            if (args.Value.Length <= 1)
                Console.Beep();
            else
                Console.Beep(Convert.ToInt32((object)args[0]), Convert.ToInt32((object)args[1]));

            return null;
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
