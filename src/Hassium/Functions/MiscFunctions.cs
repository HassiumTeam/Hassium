using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            result.Add("runtimecall", new InternalFunction(MiscFunctions.RuntimeCall));

            return result;
        }
        public static object Free(object[] args)
        {
            Interpreter.Globals.Remove(args[0].ToString());
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

        public static object RuntimeCall(object[] args)
        {
            string fullpath = args[0].ToString();
            string typename = fullpath.Substring(0, fullpath.LastIndexOf('.'));
            string membername = fullpath.Split('.').Last();
            object[] margs = args.Skip(1).ToArray();
            Type t = System.Type.GetType(typename);
            if(t == null) throw new ArgumentException("The type '" + typename + "' doesn't exist.");
            object instance = null;
            try
            {
                instance = Activator.CreateInstance(t);
            }
            catch (Exception)
            {
            }
            var test = t.GetMember(membername).First();
            if(test.MemberType == MemberTypes.Field)
            {
                return t.GetField(membername).GetValue(null);
            }
            else if (test.MemberType == MemberTypes.Method || test.MemberType == MemberTypes.Constructor)
            {
                object result = t.InvokeMember(
                    membername,
                    BindingFlags.InvokeMethod,
                    null,
                    instance,
                    margs
                    );
                return result;
            }
            return null;
        }

        public static object Import(object[] args)
        {
            string path = string.Join("", args);
            if (File.Exists(path))
                foreach (Dictionary<string, InternalFunction> entries in Interpreter.GetFunctions(path))
                    foreach (KeyValuePair<string, InternalFunction> entry in entries)
                        Interpreter.Globals.Add(entry.Key, entry.Value);
            return null;
        }
    }
}
