using System;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Math
{
    public class HassiumRandom: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Random");

        public Random Random { get; set; }

        public HassiumRandom()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 0, 1);
        }

        public HassiumRandom _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumRandom rand = new HassiumRandom();

            rand.Random = args.Length == 0 ? new Random() : new Random((int)args[0].ToInt(vm).Int);
            rand.AddAttribute("nextBytes",  rand.nextBytes,     1);
            rand.AddAttribute("nextFloat",  rand.nextFloat,     0);
            rand.AddAttribute("nextInt",    rand.nextInt, 0, 1, 2);

            return rand;
        }

        public HassiumList nextBytes(VirtualMachine vm, params HassiumObject[] args)
        {
            byte[] bytes = new byte[args[0].ToInt(vm).Int];
            Random.NextBytes(bytes);
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (byte b in bytes)
                list.add(vm, new HassiumInt(b));
            return list;
        }
        public HassiumFloat nextFloat(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat(Random.NextDouble());
        }
        public HassiumInt nextInt(VirtualMachine vm, params HassiumObject[] args)
        {
            int val = 0;
            switch (args.Length)
            {
                case 0:
                    val = Random.Next();
                    break;
                case 1:
                    val = Random.Next((int)args[0].ToInt(vm).Int);
                    break;
                case 2:
                    val = Random.Next((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int);
                    break;
            }
            return new HassiumInt(val);
        }
    }
}

