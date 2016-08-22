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
            { "format",     new HassiumFunction(format,    -1) },
            { "input",      new HassiumFunction(input,      0) },
            { "map",        new HassiumFunction(map,        2) },
            { "print",      new HassiumFunction(print,     -1) },
            { "println",    new HassiumFunction(println,   -1) },
            { "range",      new HassiumFunction(range, new int[] { 1, 2 }) },
            { "sleep",      new HassiumFunction(sleep,      1) },
            { "type",       new HassiumFunction(type,       1) },
            { "types",      new HassiumFunction(types,      1) }
        };

        public static HassiumString format(VirtualMachine vm, params HassiumObject[] args)
        {
            string[] elements = new string[args.Length - 1];
            for (int i = 1; i < elements.Length; i++)
                elements[i - 1] = args[i].ToString(vm).String;
            return new HassiumString(string.Format(args[0].ToString(vm).String, elements));
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
                if (args[1].Invoke(vm, list.List[i]).ToBool(vm).Bool)
                    result.add(vm, list.List[i]);

            return result;
        }
        public static HassiumObject print(VirtualMachine vm, params HassiumObject[] args)
        {
            foreach (var arg in args)
                Console.Write(arg.ToString(vm).String);
            return HassiumObject.Null;
        }
        public static HassiumObject println(VirtualMachine vm, params HassiumObject[] args)
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

