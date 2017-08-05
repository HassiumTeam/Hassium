using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class HassiumModule : HassiumObject
    {
        public Dictionary<int, HassiumObject> ObjectPool { get; private set; }
        public Dictionary<int, string> ConstantPool { get; private set; }
        public Dictionary<int, HassiumObject> Globals { get; private set; }

        public HassiumModule()
        {
            ObjectPool = new Dictionary<int, HassiumObject>();
            ConstantPool = new Dictionary<int, string>();
            Globals = new Dictionary<int, HassiumObject>();

            AddType(new HassiumTypeDefinition("Module"));
        }
    }
}
