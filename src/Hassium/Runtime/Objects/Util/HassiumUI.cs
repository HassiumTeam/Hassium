using System;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Util
{
    public class HassiumUI: HassiumObject
    {
        public HassiumUI()
        {
            AddAttribute("backgroundColor",   new HassiumProperty(get_backgroundColor, set_backgroundColor));
            AddAttribute("beep",              beep,  new int[] { 0, 2 });
            AddAttribute("capsLock",          new HassiumProperty(get_capsLock));
            AddAttribute("clear",             clear,                  0);
            AddAttribute("cursorLeft",        new HassiumProperty(get_cursorLeft, set_cursorLeft));
            AddAttribute("cursorSize",        new HassiumProperty(get_cursorSize, set_cursorSize));
            AddAttribute("cursorTop",         new HassiumProperty(get_cursorTop, set_cursorTop));
            AddAttribute("cursorVisible",     new HassiumProperty(get_cursorVisible, set_cursorVisible));
            AddAttribute("foregroundColor",   new HassiumProperty(get_foregroundColor, set_foregroundColor));
            AddAttribute("title",             new HassiumProperty(get_title, set_title));
            AddAttribute("windowHeight",      new HassiumProperty(get_windowHeight, set_windowHeight));
            AddAttribute("windowLeft",        new HassiumProperty(get_windowLeft, set_windowLeft));
            AddAttribute("windowTop",         new HassiumProperty(get_windowTop, set_windowTop));
            AddAttribute("windowWidth",       new HassiumProperty(get_windowWidth, set_windowWidth));
        }

        private HassiumString get_backgroundColor(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Console.BackgroundColor.ToString());
        }
        private HassiumNull set_backgroundColor(VirtualMachine vm, HassiumObject[] args)
        {
            Console.BackgroundColor = stringToConsoleColor(vm, args[0].ToString(vm).String);
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
                    Console.Beep((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int);
                    break;
            }
            return HassiumObject.Null;
        }
        private HassiumBool get_capsLock(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Console.CapsLock);
        }
        private HassiumNull clear(VirtualMachine vm, HassiumObject[] args)
        {
            Console.Clear();
            return HassiumObject.Null;
        }
        private HassiumInt get_cursorLeft(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorLeft);
        }
        private HassiumNull set_cursorLeft(VirtualMachine vm, HassiumObject[] args)
        {
            Console.CursorLeft = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }
        private HassiumInt get_cursorSize(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorSize);
        }
        private HassiumNull set_cursorSize(VirtualMachine vm, HassiumObject[] args)
        {
            Console.CursorSize = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }
        private HassiumInt get_cursorTop(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorTop);
        }
        private HassiumNull set_cursorTop(VirtualMachine vm, HassiumObject[] args)
        {
            Console.CursorTop = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }
        private HassiumBool get_cursorVisible(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Console.CursorVisible);
        }
        private HassiumNull set_cursorVisible(VirtualMachine vm, HassiumObject[] args)
        {
            Console.CursorVisible = args[0].ToBool(vm).Bool;
            return HassiumObject.Null;
        }
        private HassiumString get_foregroundColor(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Console.ForegroundColor.ToString());
        }
        private HassiumNull set_foregroundColor(VirtualMachine vm, HassiumObject[] args)
        {
            Console.ForegroundColor = stringToConsoleColor(vm, args[0].ToString(vm).String);
            return HassiumObject.Null;
        }
        private HassiumNull setCursorPosition(VirtualMachine vm, HassiumObject[] args)
        {
            Console.SetCursorPosition((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        private HassiumNull setWindowPosition(VirtualMachine vm, HassiumObject[] args)
        {
            Console.SetWindowPosition((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        private HassiumNull setWindowSize(VirtualMachine vm, HassiumObject[] args)
        {
            Console.SetWindowSize((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        private HassiumString get_title(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Console.Title);
        }
        private HassiumNull set_title(VirtualMachine vm, HassiumObject[] args)
        {
            Console.Title = args[0].ToString(vm).String;
            return HassiumObject.Null;
        }
        private HassiumInt get_windowHeight(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowHeight);
        }
        private HassiumNull set_windowHeight(VirtualMachine vm, HassiumObject[] args)
        {
            Console.WindowHeight = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }
        private HassiumInt get_windowLeft(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowLeft);
        }
        private HassiumNull set_windowLeft(VirtualMachine vm, HassiumObject[] args)
        {
            Console.WindowLeft = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }
        private HassiumInt get_windowTop(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowTop);
        }
        private HassiumNull set_windowTop(VirtualMachine vm, HassiumObject[] args)
        {
            Console.WindowTop = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }
        private HassiumInt get_windowWidth(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowWidth);
        }
        private HassiumNull set_windowWidth(VirtualMachine vm, HassiumObject[] args)
        {
            Console.WindowWidth = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }

        private ConsoleColor stringToConsoleColor(VirtualMachine vm, string colorString)
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
                    throw new InternalException(vm, "Unknown ConsoleColor: " + colorString);
            }
            return color;
        }
    }
}
