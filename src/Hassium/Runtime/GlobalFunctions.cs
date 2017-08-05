using System;
using System.Collections.Generic; 

using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime
{
    public class GlobalFunctions
    {
        public static Dictionary<string, HassiumObject> Functions = new Dictionary<string, HassiumObject>()
        {
            { "clone",         new HassiumFunction(clone,           1) },
            { "format",        new HassiumFunction(format,         -1) },
            { "getattrib",     new HassiumFunction(getattrib,       2) },
            { "getattribs",    new HassiumFunction(getattribs,      1) },
            { "hasattrib",     new HassiumFunction(hasattrib,       2) },
            { "input",         new HassiumFunction(input,           0) },
            { "map",           new HassiumFunction(map,             2) },
            { "print",         new HassiumFunction(print,          -1) },
            { "printf",        new HassiumFunction(printf,         -1) },
            { "println",       new HassiumFunction(println,        -1) },
            { "range",         new HassiumFunction(range,        1, 2) },
            { "setattrib",     new HassiumFunction(setattrib,       3) },
            { "sleep",         new HassiumFunction(sleep,           1) },
            { "type",          new HassiumFunction(type,            1) },
            { "types",         new HassiumFunction(types,           1) }
        };

        [FunctionAttribute("func clone (obj : object) : object")]
        public static HassiumObject clone(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return args[0].Clone() as HassiumObject;
        }

        [FunctionAttribute("func format (fmt : string, params obj) : string")]
        public static HassiumString format(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            if (args.Length <= 0)
                return new HassiumString(string.Empty);
            if (args.Length == 1)
                return args[0].ToString(vm, location);

            string[] fargs = new string[args.Length];
            for (int i = 1; i < args.Length; i++)
                fargs[i] = args[i].ToString(vm, location).String;
            return new HassiumString(string.Format(args[0].ToString(vm, location).String, fargs));
        }

        [FunctionAttribute("func getattribute (obj : object, attrib : string) : object")]
        public static HassiumObject getattrib(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return args[0].Attributes[args[1].ToString(vm, location).String];
        }

        [FunctionAttribute("func getattribs (obj : object) : dict")]
        public static HassiumDictionary getattribs(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumDictionary dict = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());

            foreach (var attrib in args[0].Attributes)
                dict.Dictionary.Add(new HassiumString(attrib.Key), attrib.Value);

            return dict;
        }

        [FunctionAttribute("func hasattrib (obj : object, attrib : string) : bool")]
        public static HassiumBool hasattrib(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(args[0].Attributes.ContainsKey(args[1].ToString(vm, location).String));
        }

        [FunctionAttribute("func input () : string")]
        public static HassiumString input(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Console.ReadLine());
        }

        [FunctionAttribute("func map (l : list, f : func) : list")]
        public static HassiumList map(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var list = args[0].ToList(vm, location).Values;
            HassiumList result = new HassiumList(new HassiumObject[0]);

            for (int i = 0; i < list.Count; i++)
                result.add(vm, location, args[1].Invoke(vm, location, list[i]));

            return result;
        }

        [FunctionAttribute("func print (params obj) : null")]
        public static HassiumNull print(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            foreach (var arg in args)
                Console.Write(arg.ToString(vm, location).String);
            return HassiumObject.Null;
        }

        [FunctionAttribute("func printf (strf : string, params obj) : null")]
        public static HassiumNull printf(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Console.Write(format(vm, location, args[0], args[1]).String);
            return HassiumObject.Null;
        }

        [FunctionAttribute("func println (params obj) : null")]
        public static HassiumNull println(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            foreach (var arg in args)
                Console.WriteLine(arg.ToString(vm, location).String);
            return HassiumObject.Null;
        }

        [FunctionAttribute("func range (upper : int) : list", "func range (lower : int, upper : int) : list")]
        public static HassiumList range(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            int lower = args.Length == 1 ? 0 : (int)args[1].ToInt(vm, location).Int;
            int upper = args.Length == 1 ? (int)args[0].ToInt(vm, location).Int : (int)args[1].ToInt(vm, location).Int;

            HassiumList list = new HassiumList(new HassiumObject[0]);

            while (lower < upper)
                list.add(vm, location, new HassiumInt(lower++));

            return list;
        }


        [FunctionAttribute("func setattrib (obj : object, attrib : string, val : object) : null")]
        public static HassiumNull setattrib(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            args[0].Attributes.Add(args[1].ToString(vm, location).String, args[2]);
            return HassiumObject.Null;
        }

        [FunctionAttribute("func sleep (milliseconds : int) : null")]
        public static HassiumNull sleep(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            System.Threading.Thread.Sleep((int)args[0].ToInt(vm, location).Int);
            return HassiumObject.Null;
        }

        [FunctionAttribute("func type (obj : object) : typedef")]
        public static HassiumTypeDefinition type(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return args[0].Type();
        }

        [FunctionAttribute("func types (obj : object) : list")]
        public static HassiumList types(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumList(args[0].Types);
        }
    }
}
