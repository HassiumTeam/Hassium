using System;
using Hassium.Functions;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumConsole : HassiumObject
    {
        public HassiumConsole()
        {
            Attributes.Add("beep", new InternalFunction(Beep, new[] {0, 2}));
            Attributes.Add("backgroundColor",
                new HassiumProperty("backgroundColor", x => GetBackground(new HassiumObject[] {}), SetBackground));
            Attributes.Add("foregroundColor",
                new HassiumProperty("foregroundColor", x => GetForeground(new HassiumObject[] {}), SetForeground));
            Attributes.Add("title", new HassiumProperty("title", x => Console.Title, x =>
            {
                Console.Title = x[0].ToString();
                return null;
            }));
            Attributes.Add("capsLock", new InternalFunction(x => Console.CapsLock, 0, true));
            Attributes.Add("getKey", new InternalFunction(x => Console.ReadKey(true).KeyChar.ToString(), 0));
            Attributes.Add("cursorPos", new InternalFunction(GetCursorPos, 0, true));
        }

        /// <summary>
        /// Sets the console foreground color.
        /// </summary>
        /// <param name="args">
        /// The color to apply. Can be :
        /// <list type="bullet">
        ///    <item>
        ///        <term><c>black</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>blue</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkBlue</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>green</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkGreen</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>red</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkRed</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>magenta</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkMagenta</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>yellow</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkYellow</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>gray</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkGray</c></term>
        ///    </item>
        ///     <item>
        ///        <term><c>cyan</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkCyan</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>white</c></term>
        ///    </item>
        /// </list>
        /// </param>
        /// <returns>Nothing</returns>
        public HassiumObject SetForeground(HassiumObject[] args)
        {
            Console.ForegroundColor = parseColor(args[0].ToString());
            return null;
        }

        /// <summary>
        /// Sets the console background color.
        /// </summary>
        /// <param name="args">
        /// The color to apply. Can be :
        /// <list type="bullet">
        ///    <item>
        ///        <term><c>black</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>blue</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkBlue</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>green</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkGreen</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>red</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkRed</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>magenta</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkMagenta</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>yellow</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkYellow</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>gray</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkGray</c></term>
        ///    </item>
        ///     <item>
        ///        <term><c>cyan</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkCyan</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>white</c></term>
        ///    </item>
        /// </list>
        /// </param>
        /// <returns>Nothing</returns>
        public HassiumObject SetBackground(HassiumObject[] args)
        {
            Console.BackgroundColor = parseColor(args[0].ToString());
            return null;
        }

        /// <summary>
        /// Gets the console foreground color.
        /// </summary>
        /// <param name="args">No parameters</param>
        /// <returns>
        /// The current foreground color. Can be :
        /// <list type="bullet">
        ///    <item>
        ///        <term><c>black</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>blue</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkBlue</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>green</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkGreen</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>red</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkRed</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>magenta</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkMagenta</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>yellow</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkYellow</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>gray</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkGray</c></term>
        ///    </item>
        ///     <item>
        ///        <term><c>cyan</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkCyan</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>white</c></term>
        ///    </item>
        /// </list></returns>
        public HassiumObject GetForeground(HassiumObject[] args)
        {
            return Console.ForegroundColor.ToString();
        }

        /// <summary>
        /// Gets the console background color.
        /// </summary>
        /// <param name="args">No parameters</param>
        /// <returns>
        /// The current background color. Can be :
        /// <list type="bullet">
        ///    <item>
        ///        <term><c>black</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>blue</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkBlue</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>green</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkGreen</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>red</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkRed</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>magenta</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkMagenta</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>yellow</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkYellow</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>gray</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkGray</c></term>
        ///    </item>
        ///     <item>
        ///        <term><c>cyan</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>darkCyan</c></term>
        ///    </item>
        ///    <item>
        ///        <term><c>white</c></term>
        ///    </item>
        /// </list></returns>
        public HassiumObject GetBackground(HassiumObject[] args)
        {
            return Console.BackgroundColor.ToString();
        }

        public HassiumObject GetCursorPos(HassiumObject[] args)
        {
            var ret = new HassiumObject();
            ret.SetAttribute("left",
                new HassiumProperty("left", x => Console.CursorLeft, x => Console.CursorLeft = x[1].HInt().Value));
            ret.SetAttribute("top",
                new HassiumProperty("top", x => Console.CursorTop, x => Console.CursorTop = x[1].HInt().Value));
            ret.SetAttribute("toString",
                new InternalFunction(x => "{" + Console.CursorLeft + ", " + Console.CursorTop + "}", 0));
            return ret;
        }

        public HassiumObject Beep(HassiumObject[] args)
        {
            if (args.Length <= 1)
                Console.Beep();
            else
                Console.Beep(args[0].HDouble().ValueInt, args[1].HDouble().ValueInt);

            return null;
        }

        public ConsoleColor parseColor(string color)
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