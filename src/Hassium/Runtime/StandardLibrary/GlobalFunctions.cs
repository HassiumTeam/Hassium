using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary
{
    public static class GlobalFunctions
    {
        public static Dictionary<string, HassiumObject> FunctionList = new Dictionary<string, HassiumObject>()
        {
            { "exit",           new HassiumFunction(exit, 1) },
            { "fillList",       new HassiumFunction(fillList, new int[] { 1, 2, 3 }) },
            { "filter",         new HassiumFunction(filter, 2) },
            { "format",         new HassiumFunction(format, -1) },
            { "getAttribute",   new HassiumFunction(getAttribute, 2) },
            { "globals",        new HassiumProperty(get_Globals) },
            { "hasAttribute",   new HassiumFunction(hasAttribute, 2) },
            { "input",          new HassiumFunction(input, 0) },
            { "inputChar",      new HassiumFunction(inputChar, 0) },
            { "map",            new HassiumFunction(map, new int[] { 2, 3 }) },
            { "print",          new HassiumFunction(print, -1) },
            { "println",        new HassiumFunction(println, -1) },
            { "range",          new HassiumFunction(range, 2) },
            { "setAttribute",   new HassiumFunction(setAttribute, 3) },
            { "sleep",          new HassiumFunction(sleep, 1) },
            { "type",           new HassiumFunction(type, 1) },
            { "types",          new HassiumFunction(types, -1) }
        };
        private static HassiumNull exit(VirtualMachine vm, HassiumObject[] args)
        {
            Environment.Exit((int)HassiumInt.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        private static HassiumList fillList(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = HassiumList.Create(args[0]);
            HassiumObject filler = (args.Length == 3) ? args[2] : new HassiumInt(0);
            int max = (int)HassiumInt.Create(args[1]).Value;
            for (int i = 0; i < max; i++)
                list.Value.Add(filler.Clone() as HassiumObject);

            return list;
        }
        private static HassiumList filter(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = args[0].Iter(vm) as HassiumList;
            HassiumList result = new HassiumList(new HassiumObject[0]);

            foreach (HassiumObject obj in list.Value)
                if (HassiumBool.Create(args[1].Invoke(vm, new HassiumObject[] { obj })).Value)
                    result.Value.Add(obj);
            
            return result;
        }
        private static HassiumString format(VirtualMachine vm, HassiumObject[] args)
        {
            string[] objs = new string[args.Length - 1];
            for (int i = 1; i < args.Length; i++)
                objs[i - 1] = args[i].ToString(vm);
            return new HassiumString(string.Format(HassiumString.Create(args[0]).ToString(), objs));
        }
        private static HassiumList get_Globals(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            foreach (KeyValuePair<string, HassiumObject> entry in vm.Globals)
                list.Value.Add(new HassiumKeyValuePair(new HassiumString(entry.Key), entry.Value));

            return list;
        }
        private static HassiumObject getAttribute(VirtualMachine vm, HassiumObject[] args)
        {
            return args[0].Attributes[args[1].ToString(vm)];
        }
        private static HassiumBool hasAttribute(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(args[0].Attributes.ContainsKey(args[1].ToString(vm)));
        }
        private static HassiumString input(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Console.ReadLine());
        }
        private static HassiumChar inputChar(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumChar(Convert.ToChar(Console.Read()));
        }
        private static HassiumList map(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = args[0].Iter(vm) as HassiumList;
            HassiumList result = new HassiumList(new HassiumObject[0]);

            list.EnumerableReset(vm);
            if (args.Length == 2)
                while (!HassiumBool.Create(list.EnumerableFull(vm)).Value)
                    result.Value.Add(args[1].Invoke(vm, new HassiumObject[] { list.EnumerableNext(vm) }));
            else
            {
                while (!HassiumBool.Create(list.EnumerableFull(vm)).Value)
                {
                    HassiumObject obj = list.EnumerableNext(vm);
                    if (HassiumBool.Create(args[2].Invoke(vm, new HassiumObject[] { obj })).Value)
                        result.Value.Add(args[1].Invoke(vm, new HassiumObject[] { obj }));
                }
            }
            
            list.EnumerableReset(vm);

            return result;
        }
        private static HassiumObject print(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                Console.Write(obj.ToString(vm));
            return HassiumObject.Null;
        }
        private static HassiumObject println(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                Console.WriteLine(obj.ToString(vm));
            return HassiumObject.Null;
        }
        private static HassiumObject range(VirtualMachine vm, HassiumObject[] args)
        {
            int max = (int)HassiumInt.Create(args[1]).Value;
            HassiumList list = new HassiumList(new HassiumObject[0]);
            for (int i = (int)HassiumInt.Create(args[0]).Value; i < max; i++)
                list.Value.Add(new HassiumInt(i));

            return list;
        }
        private static HassiumObject setAttribute(VirtualMachine vm, HassiumObject[] args)
        {
            string attribute = args[1].ToString(vm);
            if (!args[0].Attributes.ContainsKey(attribute))
                args[0].Attributes.Add(attribute, args[2]);
            else
                args[0].Attributes[attribute] = args[2];
            return args[2];
        }
        private static HassiumNull sleep(VirtualMachine vm, HassiumObject[] args)
        {
            Thread.Sleep((int)HassiumInt.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        private static HassiumObject type(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(args[0].Type().ToString(vm));
        }
        private static HassiumObject types(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject[] elements = new HassiumObject[args[0].Types.Count];
            for (int i = 0; i < elements.Length; i++)
                elements[i] = new HassiumString(args[0].Types[i].ToString(vm));
            return new HassiumList(elements);
        }
    }
}