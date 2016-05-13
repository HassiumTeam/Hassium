using System;
using System.Text;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumTuple: HassiumObject
    {
        public new HassiumObject[] Value { get; private set; }
        public HassiumTuple(HassiumObject[] elements)
        {
            Value = elements;

            Attributes.Add("length", new HassiumProperty(get_Length));
            Attributes.Add("split", new HassiumFunction(split, 2));
            Attributes.Add(HassiumObject.INDEX_FUNCTION,    new HassiumFunction(__index__, 1));
            Attributes.Add(HassiumObject.ENUMERABLE_FULL,   new HassiumFunction(__enumerablefull__, 0));
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION, new HassiumFunction(__tostring__, 0));
            Attributes.Add(HassiumObject.ENUMERABLE_NEXT,   new HassiumFunction(__enumerablenext__, 0));
            Attributes.Add(HassiumObject.ENUMERABLE_RESET,  new HassiumFunction(__enumerablereset__, 0));
            AddType("tuple");
        }

        private HassiumInt get_Length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.Length);
        }
        private HassiumTuple split(VirtualMachine vm, HassiumObject[] args)
        {
            int min = (int)HassiumInt.Create(args[0]).Value;
            int max = (int)HassiumInt.Create(args[1]).Value;

            HassiumObject[] elements = new HassiumObject[max - min + 1];

            for (int i = 0; i <= max - min; i++)
                elements[i] = Value[i + min];

            return new HassiumTuple(elements);
        }

        private HassiumObject __index__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumObject obj = args[0];
            if (obj is HassiumDouble)
                return Value[((HassiumDouble)obj).ValueInt];
            else if (obj is HassiumInt)
                return Value[(int)((HassiumInt)obj).Value];
            throw new InternalException("Cannot index list with " + obj.GetType().Name);
        }
        private HassiumString __tostring__ (VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (HassiumObject obj in Value)
                sb.Append(obj.ToString(vm));

            return new HassiumString(sb.ToString());
        }

        public int EnumerableIndex = 0;
        private HassiumObject __enumerablefull__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(EnumerableIndex >= Value.Length);
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

