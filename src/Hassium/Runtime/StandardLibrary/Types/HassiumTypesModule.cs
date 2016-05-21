using System;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumTypesModule: InternalModule
    {
        public static HassiumTypesModule Instance = new HassiumTypesModule();
        public HassiumTypesModule() : base ("types")
        {
            Attributes.Add("bool",              HassiumBool.TypeDefinition);
            Attributes.Add("char",              HassiumChar.TypeDefinition);
            Attributes.Add("lambda",            HassiumClosure.TypeDefinition);
            Attributes.Add("double",            HassiumDouble.TypeDefinition);
            Attributes.Add("dictionary",        HassiumDictionary.TypeDefinition);
            Attributes.Add("event",             HassiumEvent.TypeDefinition);
            Attributes.Add("ExceptionHandler",  HassiumExceptionHandler.TypeDefinition);
            Attributes.Add("func",              HassiumFunction.TypeDefinition);
            Attributes.Add("int",               HassiumInt.TypeDefinition);
            Attributes.Add("KeyValuePair",      HassiumKeyValuePair.TypeDefinition);
            Attributes.Add("list",              HassiumList.TypeDefinition);
            Attributes.Add("object",            HassiumObject.DefaultTypeDefinition);
            Attributes.Add("property",          HassiumProperty.TypeDefinition);
            Attributes.Add("string",            HassiumString.TypeDefinition);
            Attributes.Add("Thread",            HassiumThread.TypeDefinition);
            Attributes.Add("tuple",             HassiumTuple.TypeDefinition);
        }
    }
}

