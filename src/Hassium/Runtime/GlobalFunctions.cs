using System;
using System.Collections.Generic; 

using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime
{
    public class GlobalFunctions : HassiumObject
    {
        public static Dictionary<string, HassiumObject> Functions = new Dictionary<string, HassiumObject>()
        {
            { "clone",           new HassiumFunction(clone,           1) },
            { "eval",            new HassiumFunction(eval,            1) },
            { "format",          new HassiumFunction(format,         -1) },
            { "getattrib",       new HassiumFunction(getattrib,       2) },
            { "getattribs",      new HassiumFunction(getattribs,      1) },
            { "getdocauthor",    new HassiumFunction(getdocauthor,    1) },
            { "getdocdesc",      new HassiumFunction(getdocdesc,      1) },
            { "getdocoptparams", new HassiumFunction(getdocoptparams, 1) },
            { "getdocreqparams", new HassiumFunction(getdocreqparams, 1) },
            { "getdocreturns",   new HassiumFunction(getdocreturns,   1) },
            { "getparamlengths", new HassiumFunction(getparamlengths, 1) },
            { "getsourcerep",    new HassiumFunction(getsourcerep,    1) },
            { "getsourcereps",   new HassiumFunction(getsourcereps,   1) },
            { "hasattrib",       new HassiumFunction(hasattrib,       2) },
            { "help",            new HassiumFunction(help,            1) },
            { "input",           new HassiumFunction(input,           0) },
            { "map",             new HassiumFunction(map,             2) },
            { "print",           new HassiumFunction(print,          -1) },
            { "printf",          new HassiumFunction(printf,         -1) },
            { "println",         new HassiumFunction(println,        -1) },
            { "range",           new HassiumFunction(range,        1, 2) },
            { "setattrib",       new HassiumFunction(setattrib,       3) },
            { "sleep",           new HassiumFunction(sleep,           1) },
            { "type",            new HassiumFunction(type,            1) },
            { "types",           new HassiumFunction(types,           1) }
        };

        public GlobalFunctions()
        {
            BoundAttributes = Functions;
        }

        [DocStr(
            "@desc Creates a clone of the given object.",
            "@param obj The object to clone.",
            "@returns The cloned object."
            )]
        [FunctionAttribute("func clone (obj : object) : object")]
        public static HassiumObject clone(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return args[0].Clone() as HassiumObject;
        }

        [DocStr(
            "@desc Compiles the given string of Hassium source and returns a module.",
            "@param src The string Hassium source.",
            "@returns The compiled Hassium module."
            )]
        [FunctionAttribute("func eval (src : string) : module")]
        public static HassiumModule eval(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return Hassium.Compiler.Emit.HassiumCompiler.CompileModuleFromString("eval", args[0].ToString(vm, args[0], location).String);
        }

        [DocStr(
            "@desc C# formats the given format string with a list of arguments.",
            "@param fmt The format string.",
            "@optional params obj The list of format arguments.",
            "@returns The resulting formatted string."
            )]
        [FunctionAttribute("func format (fmt : string, params obj) : string")]
        public static HassiumString format(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (args.Length <= 0)
                return new HassiumString(string.Empty);
            if (args.Length == 1)
                return args[0].ToString(vm, args[0], location);

            object[] fargs = new object[args.Length];
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].Types.Contains(HassiumObject.Number))
                    fargs[i - 1] = args[i].ToInt(vm, args[i], location).Int;
                else
                    fargs[i - 1] = args[i].ToString(vm, args[i], location).String;
            }
            return new HassiumString(string.Format(args[0].ToString(vm, args[0], location).String, fargs));
        }

        [DocStr(
            "@desc Gets the specified attribute from the given object by name.",
            "@param obj The object containing the attributes.",
            "@param attrib The name of the desired attribute",
            "@returns The value of the attribute."
            )]
        [FunctionAttribute("func getattrib (obj : object, attrib : string) : object")]
        public static HassiumObject getattrib(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return args[0].GetAttribute(vm, args[1].ToString(vm, args[1], location).String);
        }

        [DocStr(
            "@desc Gets a dict containing the attributes in { string : object } format of the given object.",
            "@param obj The object to get attributes from.",
            "@returns A dictionary with the attributes."
            )]
        [FunctionAttribute("func getattribs (obj : object) : dict")]
        public static HassiumDictionary getattribs(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumDictionary dict = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());

            foreach (var attrib in args[0].GetAttributes())
                dict.Dictionary.Add(new HassiumString(attrib.Key), attrib.Value);

            return dict;
        }

        [DocStr(
            "@desc Gets the @author parameter of documentation for a function.",
            "@param obj The function to get documentation for.",
            "@returns The documentation author."
            )]
        [FunctionAttribute("func getdocauthor (obj : object) : string")]
        public static HassiumString getdocauthor(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (args[0] is HassiumFunction)
            {
                var a = (args[0] as HassiumFunction).Target.Method.GetCustomAttributes(typeof(DocStrAttribute), false);
                if (a.Length > 0)
                    return new HassiumString((a[0] as DocStrAttribute).Author);
            }
            else if (args[0] is HassiumProperty)
            {
                var a = ((args[0] as HassiumProperty).Get as HassiumFunction).Target.Method.GetCustomAttributes(typeof(DocStrAttribute), false);
                if (a.Length > 0)
                    return new HassiumString((a[0] as DocStrAttribute).Author);
            }
            else if (args[0] is HassiumMethod)
            {
                var meth = (args[0] as HassiumMethod);
                if (meth.DocStr != null)
                    return new HassiumString(meth.DocStr.Author);
            }
            else if (args[0] is HassiumClass)
            {
                var clazz = (args[0] as HassiumClass);
                if (clazz.DocStr != null)
                    return new HassiumString(clazz.DocStr.Author);
            }
            else if (args[0] is HassiumTypeDefinition)
            {
                var a = args[0].GetType().GetCustomAttributes(true);
                if (a.Length > 0)
                    return new HassiumString((a[0] as DocStrAttribute).Author);
            }
            return new HassiumString(string.Empty);
        }

        [DocStr(
            "@desc Gets the @desc parameter of documentation for a function.",
            "@param obj The function to get documentation for.",
            "@returns The documentation description."
            )]
        [FunctionAttribute("func getdocdesc (obj : object) : string")]
        public static HassiumString getdocdesc(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (args[0] is HassiumFunction)
            {
                var a = (args[0] as HassiumFunction).Target.Method.GetCustomAttributes(typeof(DocStrAttribute), false);
                if (a.Length > 0)
                    return new HassiumString((a[0] as DocStrAttribute).Description);
            }
            else if (args[0] is HassiumProperty)
            {
                var a = ((args[0] as HassiumProperty).Get as HassiumFunction).Target.Method.GetCustomAttributes(typeof(DocStrAttribute), false);
                if (a.Length > 0)
                    return new HassiumString((a[0] as DocStrAttribute).Description);
            }
            else if (args[0] is HassiumMethod)
            {
                var meth = (args[0] as HassiumMethod);
                if (meth.DocStr != null)
                    return new HassiumString(meth.DocStr.Description);
            }
            else if (args[0] is HassiumClass)
            {
                var clazz = (args[0] as HassiumClass);
                if (clazz.DocStr != null)
                    return new HassiumString(clazz.DocStr.Description);
            }
            else if (args[0] is HassiumTypeDefinition)
            {
                var a = args[0].GetType().GetCustomAttributes(true);
                if (a.Length > 0)
                    return new HassiumString((a[0] as DocStrAttribute).Description);
            }
            return new HassiumString(string.Empty);
        }

        [DocStr(
            "@desc Gets the @optional parameters of documentation for a function.",
            "@param obj The function to get documentation for.",
            "@returns A list of the documentation optional parameters."
            )]
        [FunctionAttribute("func getdocoptparams (obj : object) list")]
        public static HassiumList getdocoptparams(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var list = new HassiumList(new HassiumObject[0]);

            if (args[0] is HassiumFunction)
            {
                var a = (args[0] as HassiumFunction).Target.Method.GetCustomAttributes(typeof(DocStrAttribute), false);
                if (a.Length > 0)
                    foreach (var param in (a[0] as DocStrAttribute).OptionalParams)
                        HassiumList.add(vm, list, location, new HassiumString(param));
            }
            else if (args[0] is HassiumMethod)
            {
                var meth = (args[0] as HassiumMethod);
                if (meth.DocStr != null)
                    foreach (var param in meth.DocStr.OptionalParams)
                        HassiumList.add(vm, list, location, new HassiumString(param));
            }

            return list;
        }

        [DocStr(
            "@desc Gets the @param parameters of documentation for a function.",
            "@param obj The function to get documentation for.",
            "@returns A list of the documentation parameters."
            )]
        [FunctionAttribute("func getdocreqparams (obj : object) : list")]
        public static HassiumList getdocreqparams(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var list = new HassiumList(new HassiumObject[0]);

            if (args[0] is HassiumFunction)
            {
                var a = (args[0] as HassiumFunction).Target.Method.GetCustomAttributes(typeof(DocStrAttribute), false);
                if (a.Length > 0)
                    foreach (var param in (a[0] as DocStrAttribute).RequiredParams)
                        HassiumList.add(vm, list, location, new HassiumString(param));
            }
            else if (args[0] is HassiumMethod)
            {
                var meth = (args[0] as HassiumMethod);
                if (meth.DocStr != null)
                    foreach (var param in meth.DocStr.RequiredParams)
                        HassiumList.add(vm, list, location, new HassiumString(param));
            }

            return list;
        }

        [DocStr(
            "@desc Gets the @returns parameter of documentation for a function.",
            "@param obj The function to get documentation for.",
            "@returns The documentation returns."
            )]
        [FunctionAttribute("func getdocreturns (obj : object) : list")]
        public static HassiumString getdocreturns(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (args[0] is HassiumFunction)
            {
                var a = (args[0] as HassiumFunction).Target.Method.GetCustomAttributes(typeof(DocStrAttribute), false);
                if (a.Length > 0)
                    return new HassiumString((a[0] as DocStrAttribute).Returns);
            }
            else if (args[0] is HassiumProperty)
            {
                var a = ((args[0] as HassiumProperty).Get as HassiumFunction).Target.Method.GetCustomAttributes(typeof(DocStrAttribute), false);
                if (a.Length > 0)
                    return new HassiumString((a[0] as DocStrAttribute).Returns);
            }
            else if (args[0] is HassiumMethod)
            {
                var meth = (args[0] as HassiumMethod);
                if (meth.DocStr != null)
                    return new HassiumString(meth.DocStr.Returns);
            }

            return new HassiumString(string.Empty);
        }

        [DocStr(
            "@desc Gets a list of possible parameter lengths for the given function.",
            "@param obj The function to get parameter lengths for.",
            "@returns The parameter lengths."
            )]
        [FunctionAttribute("func getparamlengths (obj : object) : list")]
        public static HassiumList getparamlengths(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            if (args[0] is HassiumFunction)
                foreach (int len in (args[0] as HassiumFunction).ParameterLengths)
                    HassiumList.add(vm, list, location, new HassiumInt(len));
            else if (args[0] is HassiumMethod)
                HassiumList.add(vm, list, location, new HassiumInt((args[0] as HassiumMethod).Parameters.Count));
            else if (args[0] is HassiumMultiFunc)
                foreach (var method in (args[0] as HassiumMultiFunc).Methods)
                    HassiumList.add(vm, list, location, new HassiumInt(method.Parameters.Count));

            return list;
        }

        [DocStr(
            "@desc Gets the string source representation for the given function.",
            "@param obj The function to get source representation for.",
            "@returns The source representation."
            )]
        [FunctionAttribute("func getsourcerep (obj : object) : string")]
        public static HassiumString getsourcerep(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (args[0] is HassiumFunction)
            {
                var a = (args[0] as HassiumFunction).Target.Method.GetCustomAttributes(typeof(FunctionAttribute), false);
                if (a.Length > 0)
                {
                    var reps = (a[0] as FunctionAttribute).SourceRepresentations;
                    if (reps.Count > 0)
                        return new HassiumString(reps[0]);
                }
            }
            else if (args[0] is HassiumMethod)
                return new HassiumString((args[0] as HassiumMethod).SourceRepresentation);
            else if (args[0] is HassiumMultiFunc)
                return new HassiumString((args[0] as HassiumMultiFunc).Methods[0].SourceRepresentation);
            else if (args[0] is HassiumProperty)
            {
                var property = (args[0] as HassiumProperty);
                return new HassiumString((property.Get as HassiumFunction).GetTopSourceRep());
            }
            return new HassiumString(string.Empty);
        }

        [DocStr(
            "@desc Gets a list of the possible source representations for the given function.",
            "@param obj The function to get source representations for.",
            "@returns The source representations."
            )]
        [FunctionAttribute("func getsourcereps (obj : object) : list")]
        public static HassiumList getsourcereps(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            if (args[0] is HassiumFunction)
            {
                var a = (args[0] as HassiumFunction).Target.Method.GetCustomAttributes(typeof(FunctionAttribute), false);
                if (a.Length > 0)
                {
                    var reps = (a[0] as FunctionAttribute).SourceRepresentations;
                    foreach (var rep in reps)
                        HassiumList.add(vm, list, location, new HassiumString(rep));
                }
            }
            else if (args[0] is HassiumMethod)
                HassiumList.add(vm, list, location, new HassiumString((args[0] as HassiumMethod).SourceRepresentation));
            else if (args[0] is HassiumMultiFunc)
                HassiumList.add(vm, list, location, new HassiumString((args[0] as HassiumMultiFunc).Methods[0].SourceRepresentation));
            else if (args[0] is HassiumProperty)
            {
                var property = (args[0] as HassiumProperty);
                HassiumList.add(vm, list, location, new HassiumString((property.Get as HassiumFunction).GetTopSourceRep()));
                if (property.Set != null)
                    HassiumList.add(vm, list, location, new HassiumString((property.Set as HassiumFunction).GetTopSourceRep()));
            }
            return list;
        }

        [DocStr(
            "@desc Returns a bool indicating if the given object contains the specified attribute.",
            "@param obj The object whose attributes to check.",
            "@param attrib The attribute name to chech.",
            "@returns true if obj contains attrib, otherwise false."
            )]
        [FunctionAttribute("func hasattrib (obj : object, attrib : string) : bool")]
        public static HassiumBool hasattrib(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(args[0].ContainsAttribute(args[1].ToString(vm, args[1], location).String));
        }

        [DocStr(
            "@desc Prints to stdout a helpdoc for the given function using the sourcerep and docs.",
            "@param obj The function to get help for.",
            "@returns null."
            )]
        [FunctionAttribute("func help (obj : object) : null")]
        public static HassiumNull help(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Console.WriteLine(getsourcereps(vm, self, location, args).ToString(vm, null, location).String.Replace("[", string.Empty).Replace("]", string.Empty).Trim().Replace("., ", "." + Environment.NewLine).Trim());
            Console.WriteLine();
            Console.WriteLine((getdocdesc(vm, self, location, args) as HassiumString).String);
            Console.WriteLine(getdocreqparams(vm, self, location, args).ToString(vm, null, location).String.Replace("[", string.Empty).Replace("]", string.Empty).Trim().Replace("., ", "." + Environment.NewLine).Trim());
            Console.WriteLine(getdocoptparams(vm, self, location, args).ToString(vm, null, location).String.Replace("[", string.Empty).Replace("]", string.Empty).Trim().Replace("., ", "." + Environment.NewLine).Trim());
            Console.WriteLine(getdocreturns(vm, self, location, args).ToString(vm, null, location).String);

            return HassiumObject.Null;
        }

        [DocStr(
            "@desc Reads a line from stdin and returns it.",
            "@returns The string line."
            )]
        [FunctionAttribute("func input () : string")]
        public static HassiumString input(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Console.ReadLine());
        }

        [DocStr(
            "@desc Iterates over the given list, adding to a new list the result of invoking F() with each list element.",
            "@param l The list of input values.",
            "@param f The function to operate with.",
            "@returns The new list of results."
            )]
        [FunctionAttribute("func map (l : list, f : func) : list")]
        public static HassiumList map(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var list = args[0].ToList(vm, args[0], location).Values;
            HassiumList result = new HassiumList(new HassiumObject[0]);

            for (int i = 0; i < list.Count; i++)
                HassiumList.add(vm, result, location, args[1].Invoke(vm, location, list[i]));

            return result;
        }

        [DocStr(
            "@desc Writes the string value of the given objects to stdout.",
            "@optional params obj List of objects to print.",
            "@returns null."
            )]
        [FunctionAttribute("func print (params obj) : null")]
        public static HassiumNull print(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            foreach (var arg in args)
                Console.Write(arg.ToString(vm, arg, location).String);
            return HassiumObject.Null;
        }

        [DocStr(
            "@desc Writes the string value of the result of formatting the given format string with the given format args.",
            "@param strf The format string.",
            "@optional params obj The list of format arguments.",
            "@returns null."
            )]
        [FunctionAttribute("func printf (strf : string, params obj) : null")]
        public static HassiumNull printf(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Console.Write(format(vm, self, location, args).String);
            return HassiumObject.Null;
        }

        [DocStr(
            "@desc Writes the string value of the given objects to stdout, each followed by a newline.",
            "@optional params obj List of objects to print.",
            "@returns null."
            )]
        [FunctionAttribute("func println (params obj) : null")]
        public static HassiumNull println(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            foreach (var arg in args)
                Console.WriteLine(arg.ToString(vm, self, location).String);
            return HassiumObject.Null;
        }

        [DocStr(
            "@desc Returns a list of every number between 0 or a specified lower bound, and the specified upper bound.",
            "@param upper The upper bound (non-inclusive).",
            "@optional lower The lower bound (inclusive).",
            "@returns The new list of values."
            )]
        [FunctionAttribute("func range (upper : int) : list", "func range (lower : int, upper : int) : list")]
        public static HassiumList range(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            int lower = args.Length == 1 ? 0 : (int)args[0].ToInt(vm, args[0], location).Int;
            int upper = args.Length == 1 ? (int)args[0].ToInt(vm, args[0], location).Int : (int)args[1].ToInt(vm, args[1], location).Int;

            HassiumList list = new HassiumList(new HassiumObject[0]);

            while (lower < upper)
                HassiumList.add(vm, list, location, new HassiumInt(lower++));

            return list;
        }

        [DocStr(
            "@desc Sets the value of specified attribute to the specified value in the given object.",
            "@param obj The object whose attributes to modify.",
            "@param attrib The name of the attribute to set.",
            "@param val The value of the attribute to set.",
            "@returns null."
            )]
        [FunctionAttribute("func setattrib (obj : object, attrib : string, val : object) : null")]
        public static HassiumNull setattrib(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            args[0].AddAttribute(args[1].ToString(vm, args[1], location).String, args[2]);
            return HassiumObject.Null;
        }

        [DocStr(
            "@desc Stops the current thread for the specified number of milliseconds.",
            "@param milliseconds The amount of milliseconds to sleep for.",
            "@returns null."
            )]
        [FunctionAttribute("func sleep (milliseconds : int) : null")]
        public static HassiumNull sleep(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            System.Threading.Thread.Sleep((int)args[0].ToInt(vm, args[0], location).Int);
            return HassiumObject.Null;
        }

        [DocStr(
            "@desc Gets the typedef for the given object.",
            "@param obj The object whose type to get.",
            "@returns The typedef of the object."
            )]
        [FunctionAttribute("func type (obj : object) : typedef")]
        public static HassiumTypeDefinition type(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (args[0] is HassiumTypeDefinition)
                return HassiumTypeDefinition.TypeDefinition;
            return args[0].Type();
        }

        [DocStr(
            "@desc Gets a list of typedefs for the given object.",
            "@param obj The object whose types to get.",
            "@returns The list of typedefs."
            )]
        [FunctionAttribute("func types (obj : object) : list")]
        public static HassiumList types(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (args[0] is HassiumTypeDefinition)
                return new HassiumList(new HassiumObject[] { HassiumObject.TypeDefinition });
            return new HassiumList(args[0].Types);
        }
    }
}
