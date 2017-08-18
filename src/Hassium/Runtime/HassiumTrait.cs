using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class HassiumTrait : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("trait");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { TOSTRING, new HassiumFunction(tostring, 0) },
            { "traits", new HassiumProperty(get_traits) }
        };

        public string Name { get; private set; }
        public HassiumDictionary Traits { get; private set; }

        public HassiumTrait(string name)
        {
            Name = name;
            Traits = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
            AddType(TypeDefinition);
            Attributes = HassiumMethod.CloneDictionary(Attribs);
        }

        public HassiumBool Is(VirtualMachine vm, SourceLocation location, HassiumObject left)
        {
            foreach (var trait in Traits.Dictionary)
            {
                string name = trait.Key.ToString(vm, trait.Key, location).String;
                var val = trait.Value is HassiumMethod ? trait.Value.Invoke(vm, location) : trait.Value is HassiumTypeDefinition ? trait.Value : trait.Value.Type();

                if (left.Attributes.ContainsKey(name))
                {
                    if (!val.Types.Contains(left.Attributes[name].Type()))
                        return False;
                }
                else
                    return False;
            }

            return True;
        }

        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString((self as HassiumTrait).Name);
        }

        [FunctionAttribute("traits { get; }")]
        public static HassiumDictionary get_traits(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumTrait).Traits;
        }
    }
}
