using System;
using System.Collections;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumBitArray: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("BitArray");

        public BitArray BitArray { get; set; }

        public HassiumBitArray()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 1);
        }

        public HassiumBitArray _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumBitArray bitArray = new HassiumBitArray();
            if (args[0] is HassiumList)
            {
                HassiumList list = args[0].ToList(vm);
                byte[] bytes = new byte[list.List.Count];
                for (int i = 0; i < list.List.Count; i++)
                    bytes[i] = (byte)list.List[i].ToChar(vm).Char;
                bitArray.BitArray = new BitArray(bytes);
            }
            else
                bitArray.BitArray = new BitArray((int)args[0].ToInt(vm).Int);
            bitArray.AddAttribute("and",    bitArray.and,       1);
            bitArray.AddAttribute("get",    bitArray.get,       1);
            bitArray.AddAttribute("not",    bitArray.not,       0);
            bitArray.AddAttribute("or",     bitArray.or,        1);
            bitArray.AddAttribute("set",    bitArray.set,       2);
            bitArray.AddAttribute("setAll", bitArray.setAll,    1);
            return bitArray;
        }

        public HassiumBitArray and(VirtualMachine vm, params HassiumObject[] args)
        {
            BitArray.And(((HassiumBitArray)args[0]).BitArray);
            return this;
        }
        public HassiumBool get(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(BitArray.Get((int)args[0].ToInt(vm).Int));
        }
        public HassiumBitArray not(VirtualMachine vm, params HassiumObject[] args)
        {
            BitArray.Not();
            return this;
        }
        public HassiumBitArray or(VirtualMachine vm, params HassiumObject[] args)
        {
            BitArray.Or(((HassiumBitArray)args[0]).BitArray);
            return this;
        }
        public HassiumObject set(VirtualMachine vm, params HassiumObject[] args)
        {
            BitArray.Set((int)args[0].ToInt(vm).Int, args[1].ToBool(vm).Bool);
            return args[1];
        }
        public HassiumBitArray setAll(VirtualMachine vm, params HassiumObject[] args)
        {
            BitArray.SetAll(args[0].ToBool(vm).Bool);
            return this;
        }
    }
}

