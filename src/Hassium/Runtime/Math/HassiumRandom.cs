using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;

namespace Hassium.Runtime.Math
{
    public class HassiumRandom : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Random");

        public Random Random { get; set; }

        public HassiumRandom()
        {
            AddType(TypeDefinition);

            AddAttribute(INVOKE, _new, 0, 1);
            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumRandom rand)
        {
            rand.AddAttribute("randbytes", rand.randbytes, 1);
            rand.AddAttribute("randfloat", rand.randfloat, 0);
            rand.AddAttribute("randint", rand.randint, 0, 1, 2);
        }

        [FunctionAttribute("func new () : Random", "func new (seed : int) : Random")]
        public HassiumObject _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumRandom rand = new HassiumRandom();

            rand.Random = args.Length == 0 ? new Random() : new Random((int)args[0].ToInt(vm, location).Int);
            ImportAttribs(rand);

            return rand;
        }

        [FunctionAttribute("func randbytes (count : int) : list")]
        public HassiumList randbytes(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList bytes = new HassiumList(new HassiumObject[0]);

            int count = (int)args[0].ToInt(vm, location).Int;
            for (int i = 0; i < count; i++)
            {
                byte[] buf = new byte[1];
                Random.NextBytes(buf);
                bytes.add(vm, location, new HassiumChar((char)buf[0]));
            }

            return bytes;
        }

        [FunctionAttribute("func randfloat () : float")]
        public HassiumFloat randfloat(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Random.NextDouble());
        }

        [FunctionAttribute("func randint () : int", "func randint (up : int) : int", "func randint (low : int, up : int) : int")]
        public HassiumObject randint(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 0:
                    return new HassiumInt(Random.Next());
                case 1:
                    return new HassiumInt(Random.Next((int)args[0].ToInt(vm, location).Int));
                case 2:
                    return new HassiumInt(Random.Next((int)args[0].ToInt(vm, location).Int, (int)args[1].ToInt(vm, location).Int));
            }
            return Null;
        }
    }
}
