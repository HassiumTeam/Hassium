using Hassium.Compiler;

namespace Hassium.Runtime.Types
{
    public class HassiumTuple : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("tuple");

        public HassiumObject[] Values { get; private set; }

        public HassiumTuple(params HassiumObject[] val)
        {
            AddType(TypeDefinition);
            Values = val;

            AddAttribute(INDEX, Index, 1);
            AddAttribute(ITER, Iter, 0);
        }

        [FunctionAttribute("func __index__ (index : int) : object")]
        public override HassiumObject Index(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return Values[args[0].ToInt(vm, location).Int];
        }

        [FunctionAttribute("func __iter__ () : list")]
        public override HassiumObject Iter(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumList(Values);
        }
    }
}
