using System.Collections.Generic;

using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime
{
    public class HassiumClass : HassiumObject
    {
        public string Name { get; set; }
        public new HassiumTypeDefinition TypeDefinition { get; private set; }
        public List<HassiumMethod> Inherits { get; private set; }

        public HassiumClass(string name)
        {
            Name = name;
            TypeDefinition = new HassiumTypeDefinition(name);
            AddType(TypeDefinition);
            Inherits = new List<HassiumMethod>();
        }

        public new void AddAttribute(string name, HassiumObject obj)
        {
            obj.Parent = this;
            if (!Attributes.ContainsKey(name))
                Attributes.Add(name, obj);
        }

        public override HassiumObject Invoke(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            if (Attributes.ContainsKey("new"))
                return Attributes["new"].Invoke(vm, location, args).AddType(TypeDefinition);
            else if (Attributes.ContainsKey(INVOKE))
                return Attributes[INVOKE].Invoke(vm, location, args).AddType(TypeDefinition);
            else
            {
                foreach (var inherit in Inherits)
                {
                    foreach (var attrib in HassiumMethod.CloneDictionary(vm.ExecuteMethod(inherit).Attributes))
                    {
                        if (!Attributes.ContainsKey(attrib.Key))
                        {
                            attrib.Value.Parent = this;
                            Attributes.Add(attrib.Key, attrib.Value);
                        }
                    }
                }
                if (Attributes.ContainsKey("new"))
                    return Invoke(vm, location, args).AddType(TypeDefinition);
                vm.RaiseException(HassiumAttributeNotFoundException._new(vm, location, this, new HassiumString(INVOKE)));
                return Null;
            }
        }
    }
}
