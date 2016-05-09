using System;

namespace Hassium.Runtime.StandardLibrary.Math
{
    public class HassiumMathModule : InternalModule
    {
        public HassiumMathModule() : base("Math")
        {
            Attributes.Add("Math", new HassiumMath());
            Attributes.Add("Random", new HassiumRandom());
        }
    }
}

