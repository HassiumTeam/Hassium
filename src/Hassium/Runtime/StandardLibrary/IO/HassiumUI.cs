using System;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.IO
{
    public class HassiumUI: HassiumObject
    {
        public HassiumUI()
        {
            Attributes.Add("backgroundColor",   new HassiumProperty(get_BackgroundColor, set_BackgroundColor));
            Attributes.Add("beep",              new HassiumFunction(beep, new int[] { 0, 2 }));
            Attributes.Add("capsLock",          new HassiumProperty(get_CapsLock));
            Attributes.Add("clear",             new HassiumFunction(clear, 0));
            Attributes.Add("cursorLeft",        new HassiumProperty(get_CursorLeft, set_CursorLeft));
            Attributes.Add("cursorSize",        new HassiumProperty(get_CursorSize, set_CursorSize));
            Attributes.Add("cursorTop",         new HassiumProperty(get_CursorTop, set_CursorTop));
            Attributes.Add("cursorVisible",     new HassiumProperty(get_CursorVisible, set_CursorVisible));
            Attributes.Add("foregroundColor",   new HassiumProperty(get_ForegroundColor, set_ForegroundColor));
            Attributes.Add("title",             new HassiumProperty(get_Title, set_Title));
            Attributes.Add("windowHeight",      new HassiumProperty(get_WindowHeight, set_WindowHeight));
            Attributes.Add("windowLeft",        new HassiumProperty(get_WindowLeft, set_WindowLeft));
            Attributes.Add("windowTop",         new HassiumProperty(get_WindowTop, set_WindowTop));
            Attributes.Add("windowWidth",       new HassiumProperty(get_WindowWidth, set_WindowWidth));
            AddType("UI");
        }

        private HassiumString get_BackgroundColor(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Console.BackgroundColor.ToString());
        }
        private HassiumNull set_BackgroundColor(VirtualMachine vm, HassiumObject[] args)
        {
            Console.BackgroundColor = stringToConsoleColor(HassiumString.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        private HassiumNull beep(VirtualMachine vm, HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Console.Beep();
                    break;
                case 2:
                    Console.Beep((int)HassiumInt.Create(args[0]).Value, (int)HassiumInt.Create(args[1]).Value);
                    break;
            }
            return HassiumObject.Null;
        }
        private HassiumBool get_CapsLock(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Console.CapsLock);
        }
        private HassiumNull clear(VirtualMachine vm, HassiumObject[] args)
        {
            Console.Clear();
            return HassiumObject.Null;
        }
        private HassiumInt get_CursorLeft(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorLeft);
        }
        private HassiumNull set_CursorLeft(VirtualMachine vm, HassiumObject[] args)
        {
            Console.CursorLeft = (int)HassiumInt.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        private HassiumInt get_CursorSize(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorSize);
        }
        private HassiumNull set_CursorSize(VirtualMachine vm, HassiumObject[] args)
        {
            Console.CursorSize = (int)HassiumInt.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        private HassiumInt get_CursorTop(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorTop);
        }
        private HassiumNull set_CursorTop(VirtualMachine vm, HassiumObject[] args)
        {
            Console.CursorTop = (int)HassiumInt.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        private HassiumBool get_CursorVisible(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Console.CursorVisible);
        }
        private HassiumNull set_CursorVisible(VirtualMachine vm, HassiumObject[] args)
        {
            Console.CursorVisible = HassiumBool.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        private HassiumString get_ForegroundColor(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Console.ForegroundColor.ToString());
        }
        private HassiumNull set_ForegroundColor(VirtualMachine vm, HassiumObject[] args)
        {
            Console.ForegroundColor = stringToConsoleColor(HassiumString.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        private HassiumNull setCursorPosition(VirtualMachine vm, HassiumObject[] args)
        {
            Console.SetCursorPosition((int)HassiumInt.Create(args[0]).Value, (int)HassiumInt.Create(args[1]).Value);
            return HassiumObject.Null;
        }
        private HassiumNull setWindowPosition(VirtualMachine vm, HassiumObject[] args)
        {
            Console.SetWindowPosition((int)HassiumInt.Create(args[0]).Value, (int)HassiumInt.Create(args[1]).Value);
            return HassiumObject.Null;
        }
        private HassiumNull setWindowSize(VirtualMachine vm, HassiumObject[] args)
        {
            Console.SetWindowSize((int)HassiumInt.Create(args[0]).Value, (int)HassiumInt.Create(args[1]).Value);
            return HassiumObject.Null;
        }
        private HassiumString get_Title(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Console.Title);
        }
        private HassiumNull set_Title(VirtualMachine vm, HassiumObject[] args)
        {
            Console.Title = HassiumString.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        private HassiumInt get_WindowHeight(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowHeight);
        }
        private HassiumNull set_WindowHeight(VirtualMachine vm, HassiumObject[] args)
        {
            Console.WindowHeight = (int)HassiumInt.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        private HassiumInt get_WindowLeft(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowLeft);
        }
        private HassiumNull set_WindowLeft(VirtualMachine vm, HassiumObject[] args)
        {
            Console.WindowLeft = (int)HassiumInt.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        private HassiumInt get_WindowTop(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowTop);
        }
        private HassiumNull set_WindowTop(VirtualMachine vm, HassiumObject[] args)
        {
            Console.WindowTop = (int)HassiumInt.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        private HassiumInt get_WindowWidth(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowWidth);
        }
        private HassiumNull set_WindowWidth(VirtualMachine vm, HassiumObject[] args)
        {
            Console.WindowWidth = (int)HassiumInt.Create(args[0]).Value;
            return HassiumObject.Null;
        }

        private ConsoleColor stringToConsoleColor(string colorString)
        {
            ConsoleColor color;
            switch (colorString.ToLower())
            {
                case "black":
                    color = ConsoleColor.Black;
                    break;
                case "blue":
                    color = ConsoleColor.Blue;
                    break;
                case "cyan":
                    color = ConsoleColor.Cyan;
                    break;
                case "darkblue":
                    color = ConsoleColor.DarkBlue;
                    break;
                case "darkcyan":
                    color = ConsoleColor.DarkCyan;
                    break;
                case "darkgray":
                    color = ConsoleColor.DarkGray;
                    break;
                case "darkgreen":
                    color = ConsoleColor.DarkGreen;
                    break;
                case "darkmagenta":
                    color = ConsoleColor.DarkMagenta;
                    break;
                case "darkred":
                    color = ConsoleColor.DarkRed;
                    break;
                case "darkyellow":
                    color = ConsoleColor.DarkYellow;
                    break;
                case "gray":
                    color = ConsoleColor.Gray;
                    break;
                case "green":
                    color = ConsoleColor.Green;
                    break;
                case "magenta":
                    color = ConsoleColor.Magenta;
                    break;
                case "red":
                    color = ConsoleColor.Red;
                    break;
                case "white":
                    color = ConsoleColor.White;
                    break;
                case "yellow":
                    color = ConsoleColor.Yellow;
                    break;
                default:
                    throw new InternalException("Unknown ConsoleColor: " + colorString);
            }
            return color;
        }
    }
}

