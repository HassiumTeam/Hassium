using Hassium.Functions;

namespace Hassium.HassiumObjects.Random
{
    public class HassiumRandom : HassiumObject
    {
        public System.Random Value { get; private set; }

        public HassiumRandom(System.Random value)
        {
            Value = value;
            Attributes.Add("next", new InternalFunction(next, new[] {0, 1, 2}));
            Attributes.Add("nextDouble", new InternalFunction(nextDouble, 0));
            Attributes.Add("toString", new InternalFunction(toString, 0));
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