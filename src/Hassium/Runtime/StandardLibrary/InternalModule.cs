using System;
using System.Collections.Generic;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary
{
    public class InternalModule: HassiumObject
    {
        public string Name { get; private set; }
        public InternalModule(string name)
        {
            Name = name;
        }

        public static List<InternalModule> InternalModules = new List<InternalModule>()
        {
            new Collections.HassiumCollectionsModule(),
            new IO.HassiumIOModule(),
            new Math.HassiumMathModule(),
            new Net.HassiumNetModule(),
            new Reflection.HassiumReflectionModule(),
            new Text.HassiumTextModule(),
            new Util.HassiumUtilModule()
        };
    }
}

