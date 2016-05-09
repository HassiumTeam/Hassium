using System;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Reflection
{
    public class HassiumReflectionModule : InternalModule
    {
        public HassiumReflectionModule() : base("Reflection")
        {
            Attributes.Add("Assembly", new HassiumAssembly());
        }
    }
}
