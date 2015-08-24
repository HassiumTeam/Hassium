using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium
{
    public class HassiumObject : IFunction
    {
        public Dictionary<string, HassiumObject> Attributes
        {
            private set;
            get;
        }

        public HassiumObject()
        {
            Attributes = new Dictionary<string, HassiumObject>();
        }

        public void SetAttribute (string name, HassiumObject value)
        {
            Attributes[name] = value;
        }

        public HassiumObject GetAttribute(string name)
        {
            return Attributes[name];
        }

        public object Invoke (object[] args)
        {
            return null;
        }
    }
}
