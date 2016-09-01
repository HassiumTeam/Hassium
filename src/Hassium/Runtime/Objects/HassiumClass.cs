using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Objects
{
    public class HassiumClass: HassiumObject
    {
        public string Name { get; set; }
        public List<string> Inherits { get; set; }
        public new HassiumTypeDefinition TypeDefinition { get; set; }

        public override HassiumObject Invoke(VirtualMachine vm, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey("new"))
                return Attributes["new"].Invoke(vm, args);
            else if (Attributes.ContainsKey(HassiumObject.INVOKE))
                return Attributes[HassiumObject.INVOKE].Invoke(vm, args);
            throw new InternalException(vm, InternalException.OPERATOR_ERROR, "()", TypeDefinition);
        }

        public new void AddAttribute(string name, HassiumObject obj)
        {
            obj.Parent = this;
            Attributes.Add(name, obj);
        }
    }
}

