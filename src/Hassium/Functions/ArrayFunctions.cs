using System;
using System.Collections.Generic;

namespace Hassium
{
    public class ArrayFunctions : ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
            result.Add("resizearr", new InternalFunction(ArrayFunctions.ResizeArr));
            result.Add("getarr", new InternalFunction(ArrayFunctions.GetArr));
            result.Add("setarr", new InternalFunction(ArrayFunctions.SetArr));
            result.Add("arrlen", new InternalFunction(ArrayFunctions.ArrLen));
            result.Add("concatarr", new InternalFunction(ArrayFunctions.ConcatArr));
            result.Add("newarr", new InternalFunction(ArrayFunctions.NewArr));

            return result;
        }

        public static object ResizeArr(object[] args)
        {
            object arr = args[0];
            object[] objarr = (object[])arr;

            object[] newobj = new object[objarr.Length + Convert.ToInt32(args[1]) - 1];

            for (int x = 0; x < objarr.Length; x++)
                newobj[x] = objarr[x];

            return newobj;
        }

        public static object GetArr(object[] args)
        {
            object arr = args[0];
            object[] objarr = (object[])arr;

            return objarr[Convert.ToInt32(args[1])];
        }

        public static object SetArr(object[] args)
        {
            object arr = args[0];
            object[] objarr = (object[])arr;

            objarr[Convert.ToInt32(args[2])] = args[1];

            return objarr;
        }

        public static object ArrLen(object[] args)
        {
            object arr = args[0];
            object[] objarr = (object[])arr;

            return Convert.ToDouble(objarr.Length);
        }

        public static object ConcatArr(object[] args)
        {
            string result = "";
            object arr = args[0];
            object[] objarr = (object[])arr;

            foreach (object entry in objarr)
            {
                result += entry + " ";
            }

            return result;
        }

        public static object NewArr(object[] args)
        {
            return new object[Convert.ToInt32(args[0])];
        }
    }
}