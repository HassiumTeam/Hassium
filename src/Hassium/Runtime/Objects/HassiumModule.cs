using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Objects
{
    public class HassiumModule: HassiumObject
    {
        public Dictionary<int, HassiumObject> ObjectPool { get; private set; }
        public Dictionary<int, string> ConstantPool { get; private set; }
        public Dictionary<int, HassiumObject> Globals { get; private set; }
        public Dictionary<int, HassiumMethod> InitialVariables { get; private set; }

        public List<string> Imports { get; private set; }

        public HassiumModule()
        {
            ObjectPool = new Dictionary<int, HassiumObject>();
            ConstantPool = new Dictionary<int, string>();
            Globals = new Dictionary<int, HassiumObject>();
            InitialVariables = new Dictionary<int, HassiumMethod>();

            Imports = new List<string>();
        }

        public override HassiumObject Invoke(VirtualMachine vm, params HassiumObject[] args)
        {
            new VirtualMachine().Execute(this, new string[0]);
            return HassiumObject.Null;
        }
    }
}

