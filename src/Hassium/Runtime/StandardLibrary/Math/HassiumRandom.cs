using System;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Math
{
    public class HassiumRandom: HassiumObject
    {
        public new Random Value { get; set; }
        public HassiumRandom()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, new int[] { 0, 1 }));
        }

        private HassiumRandom _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumRandom hassiumRandom = new HassiumRandom();

            hassiumRandom.Value = args.Length == 1 ?    new Random((int)HassiumInt.Create(args[0]).Value) : new Random();
            hassiumRandom.Attributes.Add("nextDouble",  new HassiumFunction(hassiumRandom.nextDouble, 0));
            hassiumRandom.Attributes.Add("nextInt",     new HassiumFunction(hassiumRandom.nextInt, new int[] { 0, 1, 2 }));
            hassiumRandom.AddType("Random");

            return hassiumRandom;
        }

        public HassiumDouble nextDouble(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(Value.NextDouble());
        }
        public HassiumInt nextInt(VirtualMachine vm, HassiumObject[] args)
        {
            int value = 0;
            switch (args.Length)
            {
                case 0:
                    value = Value.Next();
                    break;
                case 1:
                    value = Value.Next((int)HassiumInt.Create(args[0]).Value);
                    break;
                case 2:
                    value = Value.Next((int)HassiumInt.Create(args[0]).Value, (int)HassiumInt.Create(args[1]).Value);
                    break;
            }

            return new HassiumInt(value);
        }
    }
}

