using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime
{
    public class HassiumTypeDefinition : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("typedef");

        public string TypeName { get; private set; }

        public HassiumTypeDefinition(string type)
        {
            AddType(TypeDefinition);
            TypeName = type;
        }

        public override string ToString()
        {
            return TypeName;
        }
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(TypeName);
        }
    }
}
