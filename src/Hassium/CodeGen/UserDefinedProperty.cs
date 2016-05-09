using System;

using Hassium.Parser;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.CodeGen
{
    public class UserDefinedProperty: HassiumObject
    {
        public string Name { get; private set; }
        public MethodBuilder GetMethod { get; set; }
        public MethodBuilder SetMethod { get; set; }

        public UserDefinedProperty(string name)
        {
            Name = name;
        }
    }
}

