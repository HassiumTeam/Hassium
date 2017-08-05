using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime
{
    public class HassiumTrait : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("trait");

        public string Name { get; private set; }
        public HassiumDictionary Traits { get; private set; }

        public HassiumTrait(string name)
        {
            Name = name;
            Traits = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
            AddType(TypeDefinition);

            AddAttribute("traits", new HassiumProperty(get_traits));
            AddAttribute(TOSTRING, ToString, 0);
        }

        public HassiumBool Is(VirtualMachine vm, SourceLocation location, HassiumObject left)
        {
            foreach (var trait in Traits.Dictionary)
            {
                string name = trait.Key.ToString(vm, location).String;
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
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Name);
        }

        [FunctionAttribute("traits { get; }")]
        public HassiumDictionary get_traits(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Traits;
        }
    }
}
