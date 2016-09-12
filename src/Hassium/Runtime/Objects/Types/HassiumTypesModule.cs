using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumTypesModule : InternalModule
    {
        public HassiumTypesModule() : base("types")
        {
            AddAttribute("BitArray",        new HassiumBitArray());
            AddAttribute("bool",            HassiumBool.TypeDefinition);
            AddAttribute("char",            HassiumChar.TypeDefinition);
            AddAttribute("closure",         HassiumClosure.TypeDefinition);
            AddAttribute("dictionary",      HassiumDictionary.TypeDefinition);
            AddAttribute("Event",           new HassiumEvent());
            AddAttribute("false",           new HassiumBool(false));
            AddAttribute("float",           HassiumFloat.TypeDefinition);
            AddAttribute("func",            HassiumFunction.TypeDefinition);
            AddAttribute("keyValuePair",    HassiumKeyValuePair.TypeDefinition);
            AddAttribute("int",             HassiumInt.TypeDefinition);
            AddAttribute("list",            HassiumList.TypeDefinition);
            AddAttribute("null",            HassiumObject.Null);
            AddAttribute("object",          HassiumObject.TypeDefinition);
            AddAttribute("property",        HassiumProperty.TypeDefinition);
            AddAttribute("string",          HassiumString.TypeDefinition);
            AddAttribute("thread",          HassiumThread.TypeDefinition);
            AddAttribute("trait",           HassiumTrait.TypeDefinition);
            AddAttribute("true",            new HassiumBool(true));
            AddAttribute("TypeConverter",   new HassiumTypeConverter());
            AddAttribute("TypeDefinition",  HassiumTypeDefinition.TypeDefinition);
        }
    }
}

