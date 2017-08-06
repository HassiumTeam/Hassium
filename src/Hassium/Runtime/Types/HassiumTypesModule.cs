namespace Hassium.Runtime.Types
{
    public class HassiumTypesModule : InternalModule
    {
        public HassiumTypesModule() : base("types")
        {
            AddAttribute("ArgumentLengthException", new HassiumArgumentLengthException());
            AddAttribute("AttributeNotFoundException", new HassiumAttributeNotFoundException());
            AddAttribute("bool", HassiumBool.TypeDefinition);
            AddAttribute("char", HassiumChar.TypeDefinition);
            AddAttribute("closure", HassiumClosure.TypeDefinition);
            AddAttribute("ConversionFailedException", new HassiumConversionFailedException());
            AddAttribute("dictionary", HassiumDictionary.TypeDefinition);
            AddAttribute("false", new HassiumBool(false));
            AddAttribute("float", HassiumFloat.TypeDefinition);
            AddAttribute("func", HassiumFunction.TypeDefinition);
            AddAttribute("IndexOutOfRangeException", new HassiumIndexOutOfRangeException());
            AddAttribute("int", HassiumInt.TypeDefinition);
            AddAttribute("list", HassiumList.TypeDefinition);
            AddAttribute("null", Null);
            AddAttribute("number", Number);
            AddAttribute("object", HassiumObject.TypeDefinition);
            AddAttribute("PrivateAttributeException", new HassiumPrivateAttributeException());
            AddAttribute("property", HassiumProperty.TypeDefinition);
            AddAttribute("string", HassiumString.TypeDefinition);
            AddAttribute("true", new HassiumBool(true));
            AddAttribute("tuple", HassiumTuple.TypeDefinition);
            AddAttribute("typedef", HassiumTypeDefinition.TypeDefinition);
            AddAttribute("VariableNotFoundException", new HassiumVariableNotFoundException());
        }
    }
}
