using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumList: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("list");

        public new List<HassiumObject> Value { get; private set; }

        public static HassiumList Create(HassiumObject obj)
        {
            if (!(obj is HassiumList))
                throw new InternalException(string.Format("Cannot convert from {0} to list!", obj.Type()));
            return (HassiumList)obj;
        }

        public HassiumList(HassiumObject[] initial)
        {
            Value = new List<HassiumObject>();
            foreach (HassiumObject obj in initial)
                Value.Add(obj);
            Attributes.Add("add",           new HassiumFunction(_add, -1));
            Attributes.Add("contains",      new HassiumFunction(contains, -1));
            Attributes.Add("copy",          new HassiumFunction(copy, 1));
            Attributes.Add("Event",         new HassiumEvent());
            Attributes.Add("getString",     new HassiumFunction(getString, 0));
            Attributes.Add("indexOf",       new HassiumFunction(indexOf, 1));
            Attributes.Add("lastIndexOf",   new HassiumFunction(lastIndexOf, 1));
            Attributes.Add("length",        new HassiumProperty(get_Length));
            Attributes.Add("remove",        new HassiumFunction(remove, -1));
            Attributes.Add("reverse",       new HassiumFunction(reverse, 0));
            Attributes.Add("slice",         new HassiumFunction(slice, new int[] { 1, 2 }));
            Attributes.Add("Thread",        new HassiumThread());
            Attributes.Add(HassiumObject.CONTAINS,              new HassiumFunction(contains, 1));
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION,     new HassiumFunction(__tostring__, 0));
            Attributes.Add(HassiumObject.EQUALS_FUNCTION,       new HassiumFunction(__equals__, 1));
            Attributes.Add(HassiumObject.NOT_EQUAL_FUNCTION,    new HassiumFunction(__notequals__, 1));
            Attributes.Add(HassiumObject.INDEX_FUNCTION,        new HassiumFunction(__index__, 1));
            Attributes.Add(HassiumObject.STORE_INDEX_FUNCTION,  new HassiumFunction(__storeindex__, 2));
            Attributes.Add(HassiumObject.ITER_FUNCTION,         new HassiumFunction(__iter__, 0));
            Attributes.Add(HassiumObject.SLICE_FUNCTION,        new HassiumFunction(slice, 2));
            Attributes.Add(HassiumObject.SKIP_FUNCTION,         new HassiumFunction(skip, 1));
            Attributes.Add(HassiumObject.ADD_FUNCTION,          new HassiumFunction(__add__, 1));
            Attributes.Add(HassiumObject.ENUMERABLE_FULL,       new HassiumFunction(__enumerablefull__, 0));
            Attributes.Add(HassiumObject.ENUMERABLE_NEXT,       new HassiumFunction(__enumerablenext__, 0));
            Attributes.Add(HassiumObject.ENUMERABLE_RESET,      new HassiumFunction(__enumerablereset__, 0));
            AddType(HassiumList.TypeDefinition);
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
                if (!Value.Any(x => x.Equals(vm, obj).Value))
                    return new HassiumBool(false);
            return new HassiumBool(true);
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
        private HassiumInt get_Length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.Count);
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
        private HassiumList skip(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            int step = (int)HassiumInt.Create(args[0]).Value;
            if (step == -1)
                return reverse(vm, args);
            for (int i = 0; (i + step) < Value.Count; i += step)
                list.Value.Add(Value[i]);

            return list;
        }
        private HassiumList slice(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            int max = args.Length == 2 ? (int)HassiumInt.Create(args[1]).Value : list.Value.Count;
            if (max == -1)
                max = list.Value.Count - 2;
            for (int i = (int)HassiumInt.Create(args[0]).Value; i < max; i++)
                list.Add(vm, Value[i]);
            return list;
        }
        private HassiumString __tostring__ (VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (HassiumObject obj in Value)
                sb.Append(obj.ToString(vm) + " ");
            return new HassiumString(sb.ToString());
        }

        private HassiumBool __equals__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = HassiumList.Create(args[0].Iter(vm));
            for (int i = 0; i < list.Value.Count; i++)
                if (!list.Value[i].Equals(vm, Value[i]).Value)
                    return new HassiumBool(false);
            return new HassiumBool(true);
        }
        private HassiumBool __notequals__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(!__equals__(vm, args).Value);
        } 
        private HassiumObject __index__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumDouble)
                return Value[((HassiumDouble)obj).ValueInt];
            else if (obj is HassiumInt)
                return Value[(int)((HassiumInt)obj).Value];
            throw new InternalException("Cannot index list with " + obj.Type().ToString(vm));
        }
        private HassiumObject __storeindex__ (VirtualMachine vm, HassiumObject[] args)
        {
            int index;
            if (args[0] is HassiumInt)
                index = (int)((HassiumInt)args[0]).Value;
            else if (args[0] is HassiumDouble)
                index = ((HassiumDouble)args[0]).ValueInt;
            else
                throw new InternalException("Cannot index list with " + args[0].Type().ToString(vm));
            Value[index] = args[1];
            return args[1];
        }
        private HassiumList __add__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList copy = this.Clone() as HassiumList;
            copy.Value.Add(args[0]);

            return copy;
        }

        public int EnumerableIndex = 0;
        private HassiumObject __iter__ (VirtualMachine vm, HassiumObject[] args)
        {
            return this;
        }
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