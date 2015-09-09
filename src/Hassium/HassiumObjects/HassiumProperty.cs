using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Functions;

namespace Hassium.HassiumObjects
{
    public class HassiumProperty : HassiumObject
    {
        public HassiumFunctionDelegate SetValue;
        public HassiumFunctionDelegate GetValue;
        public string Name;

        public HassiumProperty(string name, HassiumFunctionDelegate get, HassiumFunctionDelegate set)
        {
            Name = name;
            GetValue = get;
            SetValue = set;
        }

        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            return GetValue(args);
        }
    }
}
