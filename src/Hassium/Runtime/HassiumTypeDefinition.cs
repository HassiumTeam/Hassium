using System;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime
{
    public class HassiumTypeDefinition: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("TypeDefinition");

        public string TypeString { get; private set; }
        public HassiumTypeDefinition(string type)
        {
            TypeString = type;
            Attributes.Add(HassiumObject.TOSTRING_FUNCTION, new HassiumFunction(toString, 0));
            AddType(HassiumTypeDefinition.TypeDefinition);
        }

        public override HassiumTypeDefinition Type()
        {
            return this;
        }

        private HassiumString toString(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(TypeString);
        }
    }
}

