using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumTuple: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("tuple");

        public HassiumObject[] Elements { get; private set; }

        public HassiumTuple(HassiumObject[] elements)
        {
            Elements = elements;
            AddType(TypeDefinition);
            AddAttribute("contains",    contains,      1);
            AddAttribute("length",      new HassiumProperty(get_length));
            AddAttribute("split",       split,      1, 2);
            AddAttribute(HassiumObject.TOLIST,      ToList,     0);
            AddAttribute(HassiumObject.TOSTRING,    ToString,   0);
            AddAttribute(HassiumObject.TOTUPLE,     ToTuple,    0);
        }

        public HassiumBool contains(VirtualMachine vm, params HassiumObject[] args)
        {
            foreach (var element in Elements)
                if (element.EqualTo(vm, args[0]).ToBool(vm).Bool)
                    return new HassiumBool(true);
            return new HassiumBool(false);
        }
        public HassiumInt get_length(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Elements.Length);
        }
        public HassiumTuple split(VirtualMachine vm, params HassiumObject[] args)
        {
            int start = (int)args[0].ToInt(vm).Int;
            int end = args.Length == 2 ? (int)args[1].ToInt(vm).Int : Elements.Length;

            int length = end - start;
            HassiumObject[] elements = new HassiumObject[length];
            for (int i = 0; i < length; i++)
                elements[i] = Elements[start + i];
            return new HassiumTuple(elements);
        }
        
        public override HassiumObject EqualTo(VirtualMachine vm, params HassiumObject[] args)
        {
            var tuple = args[0].ToTuple(vm);
            if (tuple.Elements.Length != Elements.Length)
                return new HassiumBool(false);
            for (int i = 0; i < tuple.Elements.Length; i++)
                if (!tuple.Elements[i].EqualTo(vm, Elements[i]).ToBool(vm).Bool)
                    return new HassiumBool(false);
            return new HassiumBool(true);
        }
        public override HassiumObject Index(VirtualMachine vm, params HassiumObject[] args)
        {
            return Elements[args[0].ToInt(vm).Int];
        }
        public override HassiumObject Iter(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumList(Elements);
        }

        public override HassiumList ToList(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumList(Elements);
        }
        public override HassiumString ToString(VirtualMachine vm, params HassiumObject[] args)
        {
            return ToList(vm, args).ToString(vm, args);
        }
        public override HassiumTuple ToTuple(VirtualMachine vm, params HassiumObject[] args)
        {
            return this;
        }
    }
}

