namespace Hassium.Runtime.Types
{
    public class HassiumTypesModule : InternalModule
    {
        public HassiumTypesModule() : base("types")
        {
            AddAttribute("ArgLengthException", new HassiumArgLengthException());
            AddAttribute("AttribNotFoundException", new HassiumAttribNotFoundException());
            AddAttribute("bool", HassiumBool.TypeDefinition);
            AddAttribute("BigInt", HassiumBigInt.TypeDefinition);
            AddAttribute("char", HassiumChar.TypeDefinition);
            //AddAttribute("closure", HassiumClosure.TypeDefinition);
            AddAttribute("ConversionFailedException", new HassiumConversionFailedException());
            AddAttribute("dictionary", HassiumDictionary.TypeDefinition);
            AddAttribute("false", new HassiumBool(false));
            AddAttribute("float", HassiumFloat.TypeDefinition);
            AddAttribute("func", HassiumFunction.TypeDefinition);
            AddAttribute("globals", new GlobalFunctions());
            AddAttribute("IndexOutOfRangeException", HassiumIndexOutOfRangeException.TypeDefinition);
            AddAttribute("int", HassiumInt.TypeDefinition);
            AddAttribute("KeyNotFoundException", HassiumKeyNotFoundException.TypeDefinition);
            AddAttribute("list", HassiumList.TypeDefinition);
            AddAttribute("module", HassiumModule.TypeDefinition);
            AddAttribute("null", Null);
            AddAttribute("number", Number);
            AddAttribute("object", HassiumObject.TypeDefinition);
            AddAttribute("PrivateAttribException", new HassiumPrivateAttribException());
            AddAttribute("property", HassiumProperty.TypeDefinition);
            AddAttribute("string", HassiumString.TypeDefinition);
            AddAttribute("Thread", HassiumThread.TypeDefinition);
            AddAttribute("true", new HassiumBool(true));
            AddAttribute("tuple", HassiumTuple.TypeDefinition);
            AddAttribute("typedef", HassiumTypeDefinition.TypeDefinition);
            AddAttribute("VariableNotFoundException", new HassiumVariableNotFoundException());
        }
    }
}
