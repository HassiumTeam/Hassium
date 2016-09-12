using System;

namespace Hassium.Runtime.Objects.Reflection
{
    public class HassiumReflectionModule: InternalModule
    {
        public HassiumReflectionModule() : base("Reflection")
        {
            AddAttribute("HassiumInspector", new HassiumHassiumInspector());
        }
    }
}

