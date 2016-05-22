using System;
using System.Collections.Generic;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.CodeGen
{
    public class HassiumModule: HassiumObject
    {
        public string Name { get; private set; }
        public List<HassiumObject> ConstantPool;
        public Dictionary<int, HassiumObject> Globals;
        public List<string> Imports;

        public HassiumModule(string name)
        {
            Name = name;
            ConstantPool = new List<HassiumObject>();
            Globals = new Dictionary<int, HassiumObject>();
            Imports = new List<string>();
        }
    }
}

