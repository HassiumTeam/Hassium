using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Random
{
    public class HassiumRandom: HassiumObject
    {
        public System.Random Value { get; private set; }

        public HassiumRandom(System.Random value)
        {
            Value = value;
            Attributes.Add("next", new InternalFunction(next));
            Attributes.Add("nextDouble", new InternalFunction(nextDouble));
            Attributes.Add("toString", new InternalFunction(toString));
        }

        private HassiumObject next(HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 0:
                    return Value.Next();
                case 1:
                    return Value.Next(args[0].HInt().Value);
                default:
                    return Value.Next(args[0].HInt().Value, args[1].HInt().Value);
            }
        }

        private HassiumObject nextDouble(HassiumObject[] args)
        {
            return Value.NextDouble();
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return Value.ToString();
        }
    }
}

