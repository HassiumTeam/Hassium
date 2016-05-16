using System;
using System.Collections.Generic;
using System.Text;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Collections
{
    public class HassiumStack: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Stack");
        public List<HassiumObject> Stack { get; set; }
        public HassiumStack()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 0));
            AddType(HassiumStack.TypeDefinition);
        }

        private HassiumStack _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumStack hassiumStack = new HassiumStack();

            hassiumStack.Stack = new List<HassiumObject>();
            hassiumStack.Attributes.Add("peek",     new HassiumFunction(hassiumStack.peek, 0));
            hassiumStack.Attributes.Add("pop",      new HassiumFunction(hassiumStack.pop, 0));
            hassiumStack.Attributes.Add("push",     new HassiumFunction(hassiumStack.push, -1));
            hassiumStack.Attributes.Add("toList",   new HassiumFunction(hassiumStack.toList, 0));
            hassiumStack.Attributes.Add(HassiumObject.TOSTRING_FUNCTION,    new HassiumFunction(hassiumStack.toString, 0));
            hassiumStack.Attributes.Add(HassiumObject.INDEX_FUNCTION,       new HassiumFunction(hassiumStack.__index__, 1));
            hassiumStack.Attributes.Add(HassiumObject.STORE_INDEX_FUNCTION, new HassiumFunction(hassiumStack.__storeindex__, 2));
            hassiumStack.Attributes.Add(HassiumObject.ENUMERABLE_FULL,      new HassiumFunction(hassiumStack.__enumerablefull__, 0));
            hassiumStack.Attributes.Add(HassiumObject.ENUMERABLE_NEXT,      new HassiumFunction(hassiumStack.__enumerablenext__, 0));
            hassiumStack.Attributes.Add(HassiumObject.ENUMERABLE_RESET,     new HassiumFunction(hassiumStack.__enumerablereset__, 0));

            return hassiumStack;
        }

        public HassiumObject peek(VirtualMachine vm, HassiumObject[] args)
        {
            return Stack[Stack.Count - 1];
        }
        public HassiumObject pop(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject ret = Stack[Stack.Count - 1];
            Stack.Remove(Stack[Stack.Count - 1]);
            return ret;
        }
        public HassiumNull push(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                Stack.Add(obj);
            return HassiumObject.Null;
        }
        public HassiumList toList(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumList(Stack.ToArray());
        }
        public HassiumString toString(VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (HassiumObject obj in Stack)
                sb.Append(obj.ToString(vm) + " ");

            return new HassiumString(sb.ToString());
        }

        public HassiumObject __index__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumDouble)
                return Stack[((HassiumDouble)obj).ValueInt];
            else if (obj is HassiumInt)
                return Stack[(int)((HassiumInt)obj).Value];
            throw new InternalException("Cannot index stack with " + obj);
        }
        public HassiumObject __storeindex__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject index = args[0];
            if (index is HassiumDouble)
                Stack[((HassiumDouble)index).ValueInt] = args[1];
            else if (index is HassiumInt)
                Stack[(int)((HassiumInt)index).Value] = args[1];
            else
                throw new InternalException("Cannot index stack with " + index);
            return args[1];
        }
        public int EnumerableIndex = 0;
        private HassiumObject __enumerablefull__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(EnumerableIndex >= Stack.Count);
        }
        private HassiumObject __enumerablenext__ (VirtualMachine vm, HassiumObject[] args)
        {
            return Stack[EnumerableIndex++];
        }
        private HassiumObject __enumerablereset__ (VirtualMachine vm, HassiumObject[] args)
        {
            EnumerableIndex = 0;
            return HassiumObject.Null;
        }
    }
}