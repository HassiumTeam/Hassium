using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;


namespace Hassium.Runtime.Util
{
    public class HassiumUI : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new UITypeDef();

        public HassiumUI()
        {
            AddType(TypeDefinition);
        }

        [DocStr(
            "@desc A class containing methods for interacting with the terminal user interface (TUI).",
            "@returns UI."
            )]
        public class UITypeDef : HassiumTypeDefinition
        {
            public UITypeDef() : base("UI")
            {
                AddAttribute("backcolor", new HassiumProperty(get_backcolor, set_backcolor));
                AddAttribute("beep", beep, 0, 2);
                AddAttribute("capslock", new HassiumProperty(get_capslock));
                AddAttribute("clear", clear, 0);
                AddAttribute("cursorleft", new HassiumProperty(get_cursorleft, set_cursorleft));
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
            [DocStr(
                "@desc Gets the mutable string representing the background color of the terminal.",
                "@returns The background color string."
            )]
            [FunctionAttribute("backcolor { get; }")]
            public HassiumString get_backcolor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Console.BackgroundColor.ToString().ToLower());
            }

            [DocStr(
                "@desc Sets the mutable string representing the background color of the terminal.",
                "@returns null."
            )]
            [FunctionAttribute("backcolor { set; }")]
            public HassiumNull set_backcolor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.BackgroundColor = stringToConsoleColor(vm, args[0].ToString(vm, args[0], location).String);
                return Null;
            }

            [DocStr(
                "@desc Causes the terminal to beep, optionally specifying the millisecond length and frequency.",
                "@param freq The frequency as int.",
                "@param milliseconds The milliseconds as int.",
                "@returns null."
            )]
            [FunctionAttribute("func beep () : null", "func beep (freq : int, milliseconds : int) : null")]
            public HassiumNull beep(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                if (args.Length == 0)
                    Console.Beep();
                else
                    Console.Beep((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int);
                return Null;
            }

            [DocStr(
                "@desc Gets a readonly boolean indicating if the capslock is on.",
                "@returns true if capslock is on, otherwise false."
            )]
            [FunctionAttribute("capslock { get; }")]
            public HassiumBool get_capslock(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool(Console.CapsLock);
            }

            [DocStr(
                "@desc Clears the terminal.",
                "@returns null."
            )]
            [FunctionAttribute("func clear () : null")]
            public HassiumNull clear(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.Clear();
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable int representing the left cursor position.",
                "@returns The left cursor position on the terminal."
            )]
            [FunctionAttribute("cursorleft { get; }")]
            public HassiumInt get_cursorleft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt(Console.CursorLeft);
            }

            [DocStr(
                "@desc Sets the mutable int representing the left cursor position.",
                "@returns null."
            )]
            [FunctionAttribute("cursorleft { set; }")]
            public HassiumNull set_cursorleft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.CursorLeft = (int)args[0].ToInt(vm, args[0], location).Int;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable int representing the cursor size.",
                "@returns The cursor size as int."
            )]
            [FunctionAttribute("cursorsize { get; }")]
            public HassiumInt get_cursorsize(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt(Console.CursorSize);
            }

            [DocStr(
                "@desc Sets the mutable int representing the cursor size.",
                "@returns null."
            )]
            [FunctionAttribute("cursorsize { set; }")]
            public HassiumNull set_cursorsize(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.CursorSize = (int)args[0].ToInt(vm, args[0], location).Int;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable int representing the top cursor position.",
                "@returns The top cursor position on the terminal."
            )]
            [FunctionAttribute("cursortop { get; }")]
            public HassiumInt get_cursortop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt(Console.CursorTop);
            }

            [DocStr(
                "@desc Sets the mutable int representing the top cursor position.",
                "@returns null."
            )]
            [FunctionAttribute("cursortop { set; }")]
            public HassiumNull set_cursortop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.CursorTop = (int)args[0].ToInt(vm, args[0], location).Int;
                return Null;
            }

            [DocStr(
                "@desc Gets a mutable boolean indicating if the cursor is visible.",
                "@returns true if the cursor is visible, otherwise false."
            )]
            [FunctionAttribute("cursorvisible { get; }")]
            public HassiumBool get_cursorvisible(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool(Console.CursorVisible);
            }

            [DocStr(
                "@desc Sets the mutable boolean indicating if the cursor is visible.",
                "@returns null."
            )]
            [FunctionAttribute("cursorvisible { set; }")]
            public HassiumNull set_cursorvisible(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.CursorVisible = args[0].ToBool(vm, args[0], location).Bool;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable string representing the foreground color of the terminal.",
                "@returns The foreground color string."
            )]
            [FunctionAttribute("forecolor { get; }")]
            public HassiumString get_forecolor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Console.ForegroundColor.ToString().ToLower());
            }

            [DocStr(
                "@desc Sets the mutable string representing the foreground color of the terminal.",
                "@returns null."
            )]
            [FunctionAttribute("forecolor { set; }")]
            public HassiumNull set_forecolor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.ForegroundColor = stringToConsoleColor(vm, args[0].ToString(vm, args[0], location).String);
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable title of the terminal.",
                "@returns The title of the terminal as string."
            )]
            [FunctionAttribute("title { get; }")]
            public HassiumString get_title(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Console.Title);
            }

            [DocStr(
                "@desc Sets the mutable title of the terminal.",
                "@returns null."
            )]
            [FunctionAttribute("title { set; }")]
            public HassiumNull set_title(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.Title = args[0].ToString(vm, args[0], location).String;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable height of the terminal window.",
                "@returns The height of the terminal window as int."
            )]
            [FunctionAttribute("windowheight { get; }")]
            public HassiumInt get_windowheight(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt(Console.WindowHeight);
            }

            [DocStr(
                "@desc Sets the mutable window height of the terminal.",
                "@returns null."
            )]
            [FunctionAttribute("windowheight { set; }")]
            public HassiumNull set_windowheight(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.WindowHeight = (int)args[0].ToInt(vm, args[0], location).Int;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable int representing the left size of the terminal window.",
                "@returns The left size of the terminal window."
            )]
            [FunctionAttribute("windowleft { get; }")]
            public HassiumInt get_windowleft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt(Console.WindowLeft);
            }

            [DocStr(
                "@desc Sets the mutable int representing the left size of the terminal window.",
                "@returns null."
            )]
            [FunctionAttribute("windowleft { set; }")]
            public HassiumNull set_windowleft(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.WindowLeft = (int)args[0].ToInt(vm, args[0], location).Int;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable int representing the top size of the terminal window.",
                "@returns The top size of the terminal window."
            )]
            [FunctionAttribute("windowtop { get; }")]
            public HassiumInt get_windowtop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt(Console.WindowTop);
            }

            [DocStr(
                "@desc Sets the mutable int representing the top size of the terminal window.",
                "@returns null."
            )]
            [FunctionAttribute("windowtop { set; }")]
            public HassiumNull set_windowtop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.WindowTop = (int)args[0].ToInt(vm, args[0], location).Int;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable int representing the width of the terminal window.",
                "@returns The width of the terminal window."
            )]
            [FunctionAttribute("windowwidth { get; }")]
            public HassiumInt get_windowwidth(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt(Console.WindowWidth);
            }

            [DocStr(
                "@desc Sets the mutable int representing the width of the terminal window.",
                "@returns null."
            )]
            [FunctionAttribute("windowwidth { set; }")]
            public HassiumNull set_windowwidth(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                Console.WindowWidth = (int)args[0].ToInt(vm, args[0], location).Int;
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
                        vm.RaiseException(HassiumColorNotFoundException.ColorNotFoundExceptionTypeDef._new(vm, null, vm.CurrentSourceLocation, new HassiumString(colorString)));
                        break;
                }
                return color;
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
