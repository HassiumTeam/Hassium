using System.Reflection;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Reflection
{
    public class HassiumModule: HassiumObject
    {
        public Module Value { get; private set; }

        public HassiumModule(Module module)
        {
            this.Value = module;
        }

        private HassiumObject assembly(HassiumObject[] args)
        {
            return new HassiumAssembly(this.Value.Assembly);
        }

        private HassiumObject name(HassiumObject[] args)
        {
            return new HassiumString(this.Value.Name);
        }

        private HassiumObject scopeName(HassiumObject[] args)
        {
            return new HassiumString(this.Value.ScopeName);
        }

        private HassiumObject fullyQualifiedName(HassiumObject[] args)
        {
            return new HassiumString(this.Value.FullyQualifiedName);
        }
    }
}

