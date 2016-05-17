using System;
using System.Collections.Generic;

using Hassium.Parser;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary
{
    public class HassiumTrait: HassiumObject
    {
        public HassiumTypeDefinition TypeDefinition { get; set; }
        public List<TraitNode.Trait> Traits { get; private set; }
        public HassiumTrait(List<TraitNode.Trait> traits)
        {
            Traits = traits;
        }

        public bool MatchesTrait(VirtualMachine vm, HassiumObject obj)
        {
            foreach (var trait in Traits)
            {
                HassiumTypeDefinition type = vm.Globals[trait.Type] as HassiumTypeDefinition;
                if (!obj.Attributes.ContainsKey(trait.Name))
                    return false;
                if (!(obj.Attributes[trait.Name].Type() == type))
                    return false;
            }
            return true;
        }
    }
}