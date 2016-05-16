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
            Attributes.Add(HassiumObject.EQUALS_FUNCTION,       new HassiumFunction(__equals__, 1));
            Attributes.Add(HassiumObject.NOT_EQUAL_FUNCTION,    new HassiumFunction(__notequals__, 1));
            Attributes.Add(HassiumObject.INDEX_FUNCTION,        new HassiumFunction(__index__, 1));
            Attributes.Add(HassiumObject.ITER_FUNCTION,         new HassiumFunction(__iter__, 0));
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION,     new HassiumFunction(__tostring__, 0));
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
            throw new InternalException("Cannot index list with " + obj.GetType().Name);
        }
        private HassiumString __tostring__ (VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (HassiumObject obj in Value)
                sb.Append(obj.ToString(vm));

            return new HassiumString(sb.ToString());
        }

        private HassiumList __iter__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumList(Value);
        }
    }
}

