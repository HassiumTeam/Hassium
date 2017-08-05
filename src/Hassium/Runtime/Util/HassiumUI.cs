using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;


namespace Hassium.Runtime.Util
{
    public class HassiumUI : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("UI");

        public HassiumUI()
        {
            AddType(TypeDefinition);

            AddAttribute("backcolor", new HassiumProperty(get_backcolor, set_backcolor));
            AddAttribute("beep", beep, 0, 2);
            AddAttribute("capslock", new HassiumProperty(get_capslock));
            AddAttribute("clear", clear, 0);
            AddAttribute("cursorlefT", new HassiumProperty(get_cursorleft, set_cursorleft));
            AddAttribute("cursorsize", new HassiumProperty(get_cursorsize, set_cursorsize));
            AddAttribute("cursortop", new HassiumProperty(get_cursortop, set_cursortop));
            AddAttribute("cursorvisible", new HassiumProperty(get_cursorvisible, set_cursorvisible));
            AddAttribute("forecolor", new HassiumProperty(get_forecolor, set_forecolor));
            AddAttribute("title", new HassiumProperty(get_title, set_title));
            AddAttribute("windowheight", new HassiumProperty(get_windowheight, set_windowheight));
            AddAttribute("windowleft", new HassiumProperty(get_windowleft, set_windowleft));
            AddAttribute("windowtop", new HassiumProperty(get_windowtop, set_windowtop));
            AddAttribute("windowwidth", new HassiumProperty(get_windowwidth, set_windowwidth));
        }

        [FunctionAttribute("backcolor { get; }")]
        public HassiumString get_backcolor(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Console.BackgroundColor.ToString().ToLower());
        }

        [FunctionAttribute("backcolor { set; }")]
        public HassiumNull set_backcolor(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.BackgroundColor = stringToConsoleColor(vm, args[0].ToString(vm, location).String);
            return Null;
        }

        [FunctionAttribute("func beep () : null", "func beep (freq : int, milliseconds : int) : null")]
        public HassiumNull beep(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            if (args.Length == 0)
                Console.Beep();
            else
                Console.Beep((int)args[0].ToInt(vm, location).Int, (int)args[1].ToInt(vm, location).Int);
            return Null;
        }

        [FunctionAttribute("capslock { get; }")]
        public HassiumBool get_capslock(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Console.CapsLock);
        }

        [FunctionAttribute("func clear () : null")]
        public HassiumNull clear(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.Clear();
            return Null;
        }

        [FunctionAttribute("cursorleft { get; }")]
        public HassiumInt get_cursorleft(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorLeft);
        }

        [FunctionAttribute("cursorleft { set; }")]
        public HassiumNull set_cursorleft(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.CursorLeft = (int)args[0].ToInt(vm, location).Int;
            return Null;
        }

        [FunctionAttribute("cursorsize { get; }")]
        public HassiumInt get_cursorsize(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorSize);
        }

        [FunctionAttribute("cursorsize { set; }")]
        public HassiumNull set_cursorsize(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.CursorSize = (int)args[0].ToInt(vm, location).Int;
            return Null;
        }

        [FunctionAttribute("cursortop { get; }")]
        public HassiumInt get_cursortop(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Console.CursorTop);
        }

        [FunctionAttribute("cursortop { set; }")]
        public HassiumNull set_cursortop(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.CursorTop = (int)args[0].ToInt(vm, location).Int;
            return Null;
        }

        [FunctionAttribute("cursorvisible { get; }")]
        public HassiumBool get_cursorvisible(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(Console.CursorVisible);
        }

        [FunctionAttribute("cursorvisible { set; }")]
        public HassiumNull set_cursorvisible(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.CursorVisible = args[0].ToBool(vm, location).Bool;
            return Null;
        }

        [FunctionAttribute("forecolor { get; }")]
        public HassiumString get_forecolor(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Console.ForegroundColor.ToString().ToLower());
        }

        [FunctionAttribute("forecolor { set; }")]
        public HassiumNull set_forecolor(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.ForegroundColor = stringToConsoleColor(vm, args[0].ToString(vm, location).String);
            return Null;
        }

        [FunctionAttribute("title { get; }")]
        public HassiumString get_title(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Console.Title);
        }

        [FunctionAttribute("title { set; }")]
        public HassiumNull set_title(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.Title = args[0].ToString(vm, location).String;
            return Null;
        }

        [FunctionAttribute("windowheight { get; }")]
        public HassiumInt get_windowheight(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowHeight);
        }

        [FunctionAttribute("windowheight { set; }")]
        public HassiumNull set_windowheight(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.WindowHeight = (int)args[0].ToInt(vm, location).Int;
            return Null;
        }

        [FunctionAttribute("windowleft { get; }")]
        public HassiumInt get_windowleft(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowLeft);
        }

        [FunctionAttribute("windowleft { set; }")]
        public HassiumNull set_windowleft(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.WindowLeft = (int)args[0].ToInt(vm, location).Int;
            return Null;
        }

        [FunctionAttribute("windowtop { get; }")]
        public HassiumInt get_windowtop(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowTop);
        }

        [FunctionAttribute("windowtop { set; }")]
        public HassiumNull set_windowtop(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.WindowTop = (int)args[0].ToInt(vm, location).Int;
            return Null;
        }

        [FunctionAttribute("windowwidth { get; }")]
        public HassiumInt get_windowwidth(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Console.WindowWidth);
        }

        [FunctionAttribute("windowwidth { set; }")]
        public HassiumNull set_windowwidth(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.WindowWidth = (int)args[0].ToInt(vm, location).Int;
            return Null;
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
                    color = ConsoleColor.Black;
                    vm.RaiseException(HassiumColorNotFoundException._new(vm, vm.CurrentSourceLocation, new HassiumString(colorString)));
                    break;
            }
            return color;
        }
    }
}
