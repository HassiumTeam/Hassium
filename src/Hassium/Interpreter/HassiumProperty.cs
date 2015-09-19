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
        public bool ReadOnly;

        public HassiumProperty(string name, HassiumFunctionDelegate get, HassiumFunctionDelegate set, bool ro = false)
        {
            Name = name;
            GetValue = get;
            SetValue = set;
            ReadOnly = ro;
        }

        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            return GetValue(args);
        }
    }
}