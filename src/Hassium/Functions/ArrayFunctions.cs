using System;

namespace Hassium
{
    public class ArrayFunctions : ILibrary
    {
        public static object ExpandArr(object[] args)
        {
            object[] result = new object[Convert.ToInt32(args[1])];
            Array.Copy((Array)(args[0]), result, result.Length);
            return result;
        }

        public static object GetArr(object[] args)
        {
            Array result = (Array)(args[0]);
            return result[Convert.ToInt32(args[1])];
        }
    }
}

