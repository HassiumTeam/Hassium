using System;

namespace Hassium
{
    public partial class BuiltInFunctions
    {
        public static object Free(object[] args)
        {
            Interpreter.variables.Remove(args[0].ToString());
            return null;
        }

        public static object Exit(object[] args)
        {
            if (args.Length > 0)
            {
                Environment.Exit(Convert.ToInt32(args[0]));
            }
            else
            {
                Environment.Exit(0);
            }

            return null;
        }

        public static object Type(object[] args)
        {
            return args[0].GetType().ToString().Substring(args[0].GetType().ToString().LastIndexOf(".") + 1);
        }

        public static object Throw(object[] args)
        {
            throw new Exception(String.Join("", args));
        }
    }
}

