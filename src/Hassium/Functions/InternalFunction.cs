using System;

namespace Hassium
{
    public delegate HassiumObject HassiumFunctionDelegate (HassiumArray arguments);

    public class InternalFunction : HassiumObject
    {
        private HassiumFunctionDelegate target;

        public InternalFunction(HassiumFunctionDelegate target)
        {
            this.target = target;
        }


        public override string ToString()
        {
            return ((object) this).ToString();
        }

        public override HassiumObject Invoke(HassiumArray args)
        {
            return target(args);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class IntFunc : Attribute
    {
        public string Name { get; set; }
        public string Alias { get; set; }

        public IntFunc(string name)
        {
            Name = name;
            Alias = "";
        }

        public IntFunc(string name, string alias)
        {
            Name = name;
            Alias = alias;
        }
    }
}

