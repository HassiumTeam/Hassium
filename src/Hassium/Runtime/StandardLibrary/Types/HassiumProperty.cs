using System;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumProperty: HassiumObject
    {
        public HassiumFunctionDelegate GetValue;
        public HassiumFunctionDelegate SetValue;

        public HassiumProperty(HassiumFunctionDelegate getValue, HassiumFunctionDelegate setValue = null)
        {
            GetValue = getValue;
            SetValue = setValue;
            AddType("property");
        }

        public new HassiumObject Invoke(VirtualMachine vm, HassiumObject[] args)
        {
            return GetValue.Invoke(vm, args);
        }
        public HassiumObject Set(VirtualMachine vm, HassiumObject[] args)
        {
            return SetValue.Invoke(vm, args);
        }
    }
}

