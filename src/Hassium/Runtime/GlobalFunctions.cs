using System;
using System.Collections.Generic;
using System.Threading;

using Hassium.Runtime.Objects;
using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime
{
    public class GlobalFunctions
    {
        public static Dictionary<string, HassiumObject> Functions = new Dictionary<string, HassiumObject>()
        {
            { "format",         new HassiumFunction(format,        -1) },
            { "getAttribute",   new HassiumFunction(getAttribute,   2) },
            { "getAttributes",  new HassiumFunction(getAttributes,  1) },
            { "hasAttribute",   new HassiumFunction(hasAttribute,   2) },
            { "input",          new HassiumFunction(input,          0) },
            { "map",            new HassiumFunction(map,            2) },
            { "print",          new HassiumFunction(print,         -1) },
            { "printf",         new HassiumFunction(printf,        -1) },
            { "println",        new HassiumFunction(println,       -1) },
            { "range",          new HassiumFunction(range, new int[] { 1, 2 }) },
            { "readChar",       new HassiumFunction(readChar,       0)      },
            { "readKey",        new HassiumFunction(readKey, new int[] { 0, 1 }) },
            { "setAttribute",   new HassiumFunction(setAttribute,   3) },
            { "sleep",          new HassiumFunction(sleep,          1) },
            { "type",           new HassiumFunction(type,           1) },
            { "types",          new HassiumFunction(types,          1) }
        };

        public static HassiumString format(VirtualMachine vm, params HassiumObject[] args)
        {
            string[] elements = new string[args.Length - 1];
            for (int i = 0; i < elements.Length; i++)
                elements[i] = args[i + 1].ToString(vm).String;
            return new HassiumString(string.Format(args[0].ToString(vm).String, elements));
        }
        public static HassiumObject getAttribute(VirtualMachine vm, params HassiumObject[] args)
        {
            return args[0].Attributes[args[1].ToString(vm).String];
        }
        public static HassiumDictionary getAttributes(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumDictionary dict = new HassiumDictionary(new List<HassiumKeyValuePair>());
            foreach (var pair in args[0].Attributes)
                dict.add(vm, new HassiumString(pair.Key), pair.Value);
            return dict;
        }
        public static HassiumBool hasAttribute(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(args[0].Attributes.ContainsKey(args[1].ToString(vm).String));
        }
        public static HassiumString input(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Console.ReadLine());
        }
        public static HassiumList map(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList list = args[0].ToList(vm) as HassiumList;
            HassiumList result = new HassiumList(new HassiumObject[0]);

            for (int i = 0; i < list.List.Count; i++)
                result.add(vm, args[1].Invoke(vm, list.List[i]));

            return result;
        }
        public static HassiumNull print(VirtualMachine vm, params HassiumObject[] args)
        {
            foreach (var arg in args)
                Console.Write(arg.ToString(vm).String);
            return HassiumObject.Null;
        }
        public static HassiumNull printf(VirtualMachine vm, params HassiumObject[] args)
        {
            Console.Write(format(vm, args).ToString(vm).String);
            return HassiumObject.Null;
        }
        public static HassiumNull println(VirtualMachine vm, params HassiumObject[] args)
        {
            foreach (var arg in args)
                Console.WriteLine(arg.ToString(vm).String);
            return HassiumObject.Null;
        }
        public static HassiumList range(VirtualMachine vm, params HassiumObject[] args)
        {
            int start, end;
            switch (args.Length)
            {
                case 1:
                    start = 0;
                    end = (int)args[0].ToInt(vm).Int;
                    break;
                default:
                    start = (int)args[0].ToInt(vm).Int;
                    end = (int)args[1].ToInt(vm).Int;
                    break;
            }
            HassiumList result = new HassiumList(new HassiumObject[0]);
            while (start < end)
                result.add(vm, new HassiumInt(start++));
            return result;
        }
        public static HassiumChar readChar(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar((char)Console.Read());
        }
        public static HassiumChar readKey(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar((char)Console.ReadKey(args.Length == 1 ? args[0].ToBool(vm).Bool : false).KeyChar);
        }
        public static HassiumObject removeAttribute(VirtualMachine vm, params HassiumObject[] args)
        {
            args[0].Attributes.Remove(args[1].ToString(vm).String);
            return args[0];
        }
        public static HassiumObject setAttribute(VirtualMachine vm, params HassiumObject[] args)
        {
            string attrib = args[1].ToString(vm).String;
            if (args[0].Attributes.ContainsKey(attrib))
                args[0].Attributes.Remove(attrib);
            args[0].Attributes.Add(attrib, args[2]);
            return args[0];
        }
        public static HassiumNull sleep(VirtualMachine vm, params HassiumObject[] args)
        {
            Thread.Sleep((int)args[0].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        public static HassiumTypeDefinition type(VirtualMachine vm, params HassiumObject[] args)
        {
            return args[0].Type();
        }
        public static HassiumList types(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumList(args[0].Types.ToArray());
        }
    }
}

