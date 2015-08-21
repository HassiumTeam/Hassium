using System;
using System.Collections.Generic;
using System.IO;

namespace Hassium
{
    public class MiscFunctions : ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
            result.Add("free", new InternalFunction(MiscFunctions.Free));
            result.Add("exit", new InternalFunction(MiscFunctions.Exit));
            result.Add("type", new InternalFunction(MiscFunctions.Type));
            result.Add("throw", new InternalFunction(MiscFunctions.Throw));
            result.Add("import", new InternalFunction(MiscFunctions.Import));

            return result;
        }
        public static object Free(object[] args)
        {
            Interpreter.FreeVar(args[0].ToString());
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

        public static object Import(object[] args)
        {
            string path = string.Join("", args);
            if (File.Exists(path))
                foreach (Dictionary<string, InternalFunction> entries in Interpreter.GetFunctions(path))
                    foreach (KeyValuePair<string, InternalFunction> entry in entries)
                        Interpreter.SetVar(entry.Key, entry.Value);
            return null;
        }
    }
}
