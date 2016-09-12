using System;

namespace Hassium.Runtime.Objects
{
    public class HassiumEnum: HassiumObject
    {
        public HassiumEnum(string name)
        {
            AddType(new HassiumTypeDefinition(name));
        }
    }
}

