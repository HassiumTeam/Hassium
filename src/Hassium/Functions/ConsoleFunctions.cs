using System;
using System.Linq;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Conversion;

namespace Hassium.Functions
{
    public class ConsoleFunctions : ILibrary
    {
        [IntFunc("print")]
        public static HassiumObject Print(HassiumObject[] args)
        {
            Console.Write(String.Join("", args.Cast<object>()));
            return null;
        }

        [IntFunc("println")]
        public static HassiumObject PrintLn(HassiumObject[] args)
        {
            Console.WriteLine(String.Join("", args.Select(x => x.ToString())));
            return null;
        }

        [IntFunc("printarr")]
        public static HassiumObject PrintArr(HassiumObject[] args)
        {
            Console.WriteLine(HassiumConvert.toString(new[] {args[0]}).ToString());
            return null;
        }

        [IntFunc("input")]
        public static HassiumObject Input(HassiumObject[] args)
        {
            return Console.ReadLine();
        }

        [IntFunc("cls")]
        public static HassiumObject Cls(HassiumObject[] args)
        {
            Console.Clear();
            return null;
        }

        [IntFunc("pause")]
        public static HassiumObject Pause(HassiumObject[] args)
        {
            Console.ReadKey(true);
            return null;
        }

        [IntFunc("setForeground")]
        public static HassiumObject Setfcol(HassiumObject[] args)
        {
            Console.ForegroundColor = parseColor(args[0].ToString());
            return null;
        }

        [IntFunc("setBackground")]
        public static HassiumObject Setbcol(HassiumObject[] args)
        {
            Console.BackgroundColor = parseColor(args[0].ToString());
            return null;
        }

        [IntFunc("getForeground")]
        public static HassiumObject Getfcol(HassiumObject[] args)
        {
            return Console.ForegroundColor.ToString();
        }

        [IntFunc("getBackground")]
        public static HassiumObject Getbcol(HassiumObject[] args)
        {
            return Console.BackgroundColor.ToString();
        }

        [IntFunc("setPosition")]
        public static HassiumObject ScursorPosition(HassiumObject[] args)
        {
            Console.SetCursorPosition(args[0].HNum().ValueInt, args[1].HNum().ValueInt);
            return null;
        }

        [IntFunc("getLeft")]
        public static HassiumObject GetLeft(HassiumObject[] args)
        {
            return Console.CursorLeft;
        }

        [IntFunc("getTop")]
        public static HassiumObject GetTop(HassiumObject[] args)
        {
            return Console.CursorTop;
        }

        [IntFunc("getTitle")]
        public static HassiumObject GetTitle(HassiumObject[] args)
        {
            return Console.Title;
        }

        [IntFunc("setTitle")]
        public static HassiumObject SetTitle(HassiumObject[] args)
        {
            Console.Title = args[0].ToString();
            return null;
        }

        [IntFunc("beep")]
        public static HassiumObject Beep(HassiumObject[] args)
        {
            if (args.Length <= 1)
                Console.Beep();
            else
                Console.Beep(args[0].HNum().ValueInt, args[1].HNum().ValueInt);

            return null;
        }

        private static ConsoleColor parseColor(string color)
        {
            switch (color)
            {
                case "black":
                    return ConsoleColor.Black;

                case "blue":
                    return ConsoleColor.Blue;
                case "darkBlue":
                    return ConsoleColor.DarkBlue;

                case "green":
                    return ConsoleColor.Green;
                case "darkGreen":
                    return ConsoleColor.DarkGreen;

                case "red":
                    return ConsoleColor.Red;
                case "darkRed":
                    return ConsoleColor.DarkRed;

                case "magenta":
                    return ConsoleColor.Magenta;
                case "darkMagenta":
                    return ConsoleColor.DarkMagenta;

                case "yellow":
                    return ConsoleColor.Yellow;
                case "darkYellow":
                    return ConsoleColor.DarkYellow;

                case "gray":
                    return ConsoleColor.Gray;
                case "darkGray":
                    return ConsoleColor.DarkGray;

                case "cyan":
                    return ConsoleColor.Cyan;
                case "darkCyan":
                    return ConsoleColor.DarkCyan;
                
                
                case "white":
                    return ConsoleColor.White;
               
                default:
                    throw new Exception("Color is not valid!");
            }
        }
    }
}
