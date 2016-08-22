using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumNull: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("null");

        public HassiumNull()
        {
            AddType(TypeDefinition);
        }
    }
}

