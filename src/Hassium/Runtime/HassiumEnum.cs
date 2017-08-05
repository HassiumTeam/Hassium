﻿namespace Hassium.Runtime
{
    public class HassiumEnum : HassiumObject
    {
        public string Name { get; private set; }
        public new HassiumTypeDefinition TypeDefinition { get; private set; }

        public HassiumEnum(string name)
        {
            Name = name;
            TypeDefinition = new HassiumTypeDefinition(name);
        }

        public new void AddAttribute(string name, HassiumObject obj)
        {
            Attributes.Add(name, obj);
            obj.Types.Add(TypeDefinition);
        }
    }
}
