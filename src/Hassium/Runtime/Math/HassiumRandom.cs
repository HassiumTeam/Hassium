using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Math
{
    public class HassiumRandom : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new RandomTypeDef();

        public Random Random { get; set; }

        public HassiumRandom()
        {
            AddType(TypeDefinition);
        }

        [DocStr(
            "@desc A class representing a Random object for computing pseudo-random numbers.",
            "@returns Random."
            )]
        public class RandomTypeDef : HassiumTypeDefinition
        {
            public RandomTypeDef() : base("Random")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { INVOKE, new HassiumFunction(_new, 0, 1) },
                    { "randbytes", new HassiumFunction(randbytes, 1) },
                    { "randfloat", new HassiumFunction(randfloat, 1) },
                    { "randint", new HassiumFunction(randint, 0, 1, 2) },
                };
            }

            [DocStr(
                "@desc Constructs a new Random object using the optionally specified seed.",
                "@optional seed The int seed for the random object.",
                "@returns The new Random object."
                )]
            [FunctionAttribute("func new () : Random", "func new (seed : int) : Random")]
            public static HassiumObject _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumRandom rand = new HassiumRandom();

                rand.Random = args.Length == 0 ? new Random() : new Random((int)args[0].ToInt(vm, args[0], location).Int);

                return rand;
            }

            [DocStr(
                "@desc Returns a new list with the specified count, filled with random bytes.",
                "@param count The amount of random bytes to get.",
                "@returns A new list of random bytes."
                )]
            [FunctionAttribute("func randbytes (count : int) : list")]
            public static HassiumList randbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Random = (self as HassiumRandom).Random;
                HassiumList bytes = new HassiumList(new HassiumObject[0]);

                int count = (int)args[0].ToInt(vm, args[0], location).Int;
                for (int i = 0; i < count; i++)
                {
                    byte[] buf = new byte[1];
                    Random.NextBytes(buf);
                    HassiumList.add(vm, bytes, location, new HassiumChar((char)buf[0]));
                }

                return bytes;
            }

            [DocStr(
                "@desc Returns a random float.",
                "@returns The random float."
                )]
            [FunctionAttribute("func randfloat () : float")]
            public static HassiumFloat randfloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Random = (self as HassiumRandom).Random;
                return new HassiumFloat(Random.NextDouble());
            }

            [DocStr(
                "@desc Calculates a random int using either no parameters, the specified upper bound, or the specified lower and upper bound.",
                "@optional low The inclusive lower bound.",
                "@optional up The non-inclusive upper bound.",
                "@returns The random int."
                )]
            [FunctionAttribute("func randint () : int", "func randint (up : int) : int", "func randint (low : int, up : int) : int")]
            public static HassiumObject randint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Random = (self as HassiumRandom).Random;
                switch (args.Length)
                {
                    case 0:
                        return new HassiumInt(Random.Next());
                    case 1:
                        return new HassiumInt(Random.Next((int)args[0].ToInt(vm, args[0], location).Int));
                    case 2:
                        return new HassiumInt(Random.Next((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int));
                }
                return Null;
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
