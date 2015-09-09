using System;
using System.Linq;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Conversion;

namespace Hassium.Functions
{
    public class ConsoleFunctions : ILibrary
    {
        /// <summary>
        /// Prints the specified string to the console.
        /// </summary>
        /// <param name="args">The string to print.</param>
        /// <returns>Nothing</returns>
        [IntFunc("print")]
        public static HassiumObject Print(HassiumObject[] args)
        {
            Console.WriteLine(String.Join("", args.Select(x => x.ToString())));
            return null;
        }
        /// <summary>
        /// Prints the specified string to the console (see <c>Print</c>) followed by a newline.
        /// </summary>
        /// <param name="args">The string to print.</param>
        /// <returns>Nothing</returns>
        [IntFunc("println")]
        public static HassiumObject PrintLn(HassiumObject[] args)
        {
            Console.WriteLine(String.Join("", args.Select(x => x.ToString())));
            return null;
        }
        /// <summary>
        /// Prints the specified array to the console followed by a newline.
        /// </summary>
        /// <param name="args">The array to print.</param>
        /// <returns>Nothing</returns>
        [IntFunc("printarr")]
        public static HassiumObject PrintArr(HassiumObject[] args)
        {
            Console.WriteLine(HassiumConvert.toString(new[] {args[0]}).ToString());
            return null;
        }
        /// <summary>
        /// Prompt the user to type something and return the input.
        /// </summary>
        /// <param name="args">No parameters.</param>
        /// <returns>Nothing.</returns>
        [IntFunc("input")]
        public static HassiumObject Input(HassiumObject[] args)
        {
            return Console.ReadLine();
        }
        /// <summary>
        /// Clears the console.
        /// </summary>
        /// <param name="args">No parameters.</param>
        /// <returns>Nothing.</returns>
        [IntFunc("cls")]
        public static HassiumObject Cls(HassiumObject[] args)
        {
            Console.Clear();
            return null;
        }
        /// <summary>
        /// Waits for the user to press any key.
        /// </summary>
        /// <param name="args">No parameters.</param>
        /// <returns>Nothing.</returns>
        [IntFunc("pause")]
        public static HassiumObject Pause(HassiumObject[] args)
        {
            Console.ReadKey(true);
            return null;
        }
    }
}
