using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumList: HassiumObject
    {
        public new List<HassiumObject> Value { get; private set; }

        public static HassiumList Create(HassiumObject obj)
        {
            if (!(obj is HassiumList))
                throw new InternalException(string.Format("Cannot convert from {0} to HassiumList!", obj.GetType().Name));
            return (HassiumList)obj;
        }

        public HassiumList(HassiumObject[] initial)
        {
            Value = new List<HassiumObject>();
            foreach (HassiumObject obj in initial)
                Value.Add(obj);
            Attributes.Add("add",           new HassiumFunction(_add, -1));
            Attributes.Add("contains",      new HassiumFunction(contains, -1));
            Attributes.Add("containsKey",   new HassiumFunction(containsKey, 1));
            Attributes.Add("containsValue", new HassiumFunction(containsValue, 1));
            Attributes.Add("copy",          new HassiumFunction(copy, 1));
            Attributes.Add("getString",     new HassiumFunction(getString, 0));
            Attributes.Add("indexOf",       new HassiumFunction(indexOf, 1));
            Attributes.Add("lastIndexOf",   new HassiumFunction(lastIndexOf, 1));
            Attributes.Add("length",        new HassiumProperty(get_Length));
            Attributes.Add("remove",        new HassiumFunction(remove, -1));
            Attributes.Add("reverse",       new HassiumFunction(reverse, 0));
            Attributes.Add("split",         new HassiumFunction(split, new int[] { 1, 2 }));
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION,     new HassiumFunction(__tostring__, 0));
            Attributes.Add(HassiumObject.INDEX_FUNCTION,        new HassiumFunction(__index__, 1));
            Attributes.Add(HassiumObject.STORE_INDEX_FUNCTION,  new HassiumFunction(__storeindex__, 2));
            Attributes.Add(HassiumObject.ENUMERABLE_FULL,       new HassiumFunction(__enumerablefull__, 0));
            Attributes.Add(HassiumObject.ENUMERABLE_NEXT,       new HassiumFunction(__enumerablenext__, 0));
            Attributes.Add(HassiumObject.ENUMERABLE_RESET,      new HassiumFunction(__enumerablereset__, 0));
            Types.Add(GetType().Name);
        }

        private HassiumObject _add(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                Value.Add(obj);
            return HassiumObject.Null;
        }
        private HassiumBool contains(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                if (!Value.Any(x => x.Equals(vm, args[0]).Value))
                    return new HassiumBool(false);
            return new HassiumBool(true);
        }
        private HassiumBool containsKey(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in Value)
            {
                if (obj is HassiumKeyValuePair)
                if (args[0].Equals(vm, ((HassiumKeyValuePair)obj).Key).Value)
                    return new HassiumBool(true);
            }
            return new HassiumBool(false);
        }
        private HassiumBool containsValue(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in Value)
            {
                if (obj is HassiumKeyValuePair)
                if (args[0].Equals(vm, ((HassiumKeyValuePair)obj).Value).Value)
                    return new HassiumBool(true);
            }
            return new HassiumBool(false);
        }
        private HassiumNull copy(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = HassiumList.Create(args[0]);
            foreach (HassiumObject obj in Value)
                list.Add(vm, obj);

            return HassiumObject.Null;
        }
        private HassiumString getString(VirtualMachine vm, HassiumObject[] args)
        {
            byte[] bytes = new byte[Value.Count];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)HassiumChar.Create(Value[i]).Value;
            return new HassiumString(ASCIIEncoding.ASCII.GetString(bytes));
        }
        private HassiumDouble indexOf(VirtualMachine vm, HassiumObject[] args)
        {
            for (int i = 0; i < Value.Count; i++)
                if (Value[i].Equals(vm, args[0]).Value)
                    return new HassiumDouble(i);
            return new HassiumDouble(-1);
        }
        private HassiumDouble lastIndexOf(VirtualMachine vm, HassiumObject[] args)
        {
            for (int i = Value.Count - 1; i >= 0; i--)
                if (Value[i].Equals(vm, args[0]).Value)
                    return new HassiumDouble(i);
            return new HassiumDouble(-1);
        }
        private HassiumDouble get_Length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(Value.Count);
        }
        private HassiumNull remove(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                Value.Remove(obj);
            return HassiumObject.Null;
        }
        private HassiumList reverse(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject[] elements = new HassiumObject[Value.Count];
            for (int i = 0; i < elements.Length; i++)
                elements[i] = Value[Value.Count - (i + 1)];
            return new HassiumList(elements);
        }
        private HassiumList split(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            int max = args.Length == 2 ? (int)HassiumInt.Create(args[1]).Value : list.Value.Count;
            for (int i = (int)HassiumInt.Create(args[0]).Value; i < max; i++)
                list.Add(vm, Value[i]);
            return list;
        }
        private HassiumString __tostring__(VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (HassiumObject obj in Value)
                sb.Append(obj.ToString(vm) + " ");
            return new HassiumString(sb.ToString());
        }

        private HassiumObject __index__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            foreach (HassiumObject entry in Value)
                if (entry is HassiumKeyValuePair)
                {
                    var pair = entry as HassiumKeyValuePair;
                    try
                    {
                        if (pair.Key.Equals(vm, obj).Value)
                            return pair.Value;
                    }
                    catch
                    {
                    }
                }
            if (obj is HassiumDouble)
                return Value[((HassiumDouble)obj).ValueInt];
            else if (obj is HassiumInt)
                return Value[(int)((HassiumInt)obj).Value];
            throw new InternalException("Cannot index list with " + obj.GetType().Name);
        }
        private HassiumObject __storeindex__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject index = args[0];
            foreach (HassiumObject entry in Value)
                if (entry is HassiumKeyValuePair)
                {
                    var pair = entry as HassiumKeyValuePair;
                    try
                    {
                        if (pair.Key.Equals(vm, index).Value)
                        {
                            pair.Value = args[1];
                            return args[1];
                        }
                    }
                    catch
                    {
                    }
                }

            if (index is HassiumInt || index is HassiumDouble)
            {
                int indexer = Convert.ToInt32(index.ToString(vm));
                if (indexer < Value.Count)
                    Value[indexer] = args[1];
                else
                    Value.Add(new HassiumKeyValuePair(index, args[1]));
            }
            else
                Value.Add(new HassiumKeyValuePair(index, args[1]));
            return args[1];
        }
        public int EnumerableIndex = 0;
        private HassiumObject __enumerablefull__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(EnumerableIndex >= Value.Count);
        }
        private HassiumObject __enumerablenext__ (VirtualMachine vm, HassiumObject[] args)
        {
            return Value[EnumerableIndex++];
        }
        private HassiumObject __enumerablereset__ (VirtualMachine vm, HassiumObject[] args)
        {
            EnumerableIndex = 0;
            return HassiumObject.Null;
        }
    }
}