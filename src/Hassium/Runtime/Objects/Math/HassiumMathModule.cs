using System;

namespace Hassium.Runtime.Objects.Math
{
    public class HassiumMathModule: InternalModule
    {
        public HassiumMathModule() : base("Math")
        {
            AddAttribute("Math",    new HassiumMath());
            AddAttribute("Random",  new HassiumRandom());
        }
    }
}

