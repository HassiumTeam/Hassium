using System;

namespace Hassium.Runtime.Objects
{
    public class HassiumProperty: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("property");
        public bool IsReadOnly { get { return Set == null; } }
        public HassiumObject Get { get; private set; }
        public HassiumObject Set { get; private set; }

        public HassiumProperty(HassiumObject get_, HassiumObject set_ = null)
        {
            Get = get_;
            Set = set_;
        }
        public HassiumProperty(HassiumFunctionDelegate get_, HassiumFunctionDelegate set_ = null)
        {
            Get = new HassiumFunction(get_, 0);
            Set = set_ != null ? new HassiumFunction(set_, 1) : null;
        }
    }
}