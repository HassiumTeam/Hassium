using System;
using System.Collections.Generic;

using Hassium.Compiler.Parser.Ast;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumTrait: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("trait");

        public List<Trait> Traits { get; private set; }

        public HassiumTrait(List<Trait> traits)
        {
            Traits = traits;
            AddType(TypeDefinition);
        }

        public HassiumBool Is(VirtualMachine vm, HassiumObject obj)
        {
            foreach (var trait in Traits)
            {
                if (!obj.Attributes.ContainsKey(trait.Name))
                    return new HassiumBool(false);
                if (obj.Attributes[trait.Name].Type() != vm.Globals[trait.Type].Type())
                    return new HassiumBool(false);
            }
            return new HassiumBool(true);
        }
    }
}

