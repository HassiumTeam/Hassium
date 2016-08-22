using System;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects
{
    public class HassiumTypeDefinition: HassiumObject
    {
        public string TypeName { get; private set; }

        public HassiumTypeDefinition(string type)
        {
            TypeName = type;
            AddType(this);
        }

        public override string ToString()
        {
            return TypeName;
        }
        public override HassiumString ToString(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(TypeName);
        }
    }
}

