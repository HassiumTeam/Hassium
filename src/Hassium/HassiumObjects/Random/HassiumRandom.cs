using System;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Random
{
    public class HassiumRandom: HassiumObject
    {
        public System.Random Value { get; private set; }

        public HassiumRandom(System.Random value)
        {
            this.Value = value;
            this.Attributes.Add("next", new InternalFunction(next));
            this.Attributes.Add("nextDouble", new InternalFunction(nextDouble));
            this.Attributes.Add("toString", new InternalFunction(toString));
        }

        private HassiumObject next(HassiumObject[] args)
        {
            if (args.Length == 0)
                return new HassiumNumber(this.Value.Next());
            else if (args.Length == 1)
                return new HassiumNumber(this.Value.Next(((HassiumNumber)args[0]).ValueInt));
            else
                return new HassiumNumber(this.Value.Next(((HassiumNumber)args[0]).ValueInt, ((HassiumNumber)args[1]).ValueInt));
        }

        private HassiumObject nextDouble(HassiumObject[] args)
        {
            return new HassiumNumber(this.Value.NextDouble());
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return this.Value.ToString();
        }
    }
}

