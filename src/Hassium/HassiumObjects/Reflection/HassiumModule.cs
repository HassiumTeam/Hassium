using System.Reflection;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Reflection
{
    public class HassiumModule: HassiumObject
    {
        public Module Value { get; private set; }

        public HassiumModule(Module module)
        {
            Value = module;
            Attributes.Add("assembly", new InternalFunction(x => new HassiumAssembly(Value.Assembly), true));
            Attributes.Add("name", new InternalFunction(x => new HassiumString(Value.Name), true));
            Attributes.Add("scopeName", new InternalFunction(x => new HassiumString(Value.ScopeName), true));
            Attributes.Add("fullyQualifiedName", new InternalFunction(x => new HassiumString(Value.FullyQualifiedName), true));
        }
    }
}

