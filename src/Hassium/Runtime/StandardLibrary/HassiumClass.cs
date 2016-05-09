using System;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary
{
    public class HassiumClass: HassiumObject
    {
        public override HassiumObject Invoke(VirtualMachine vm, HassiumObject[] args)
        {
            if (!Attributes.ContainsKey("new"))
                throw new InternalException("Class has no suitible constructor!");
            return Attributes["new"].Invoke(vm, args);
        }
    }
}

