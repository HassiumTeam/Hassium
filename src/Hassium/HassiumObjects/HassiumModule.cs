using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.HassiumObjects
{
    public class HassiumModule : HassiumObject
    {
        public string Name { get; private set; }
        public HassiumModule(string name)
        {
            Name = name;
        }
    }
}
